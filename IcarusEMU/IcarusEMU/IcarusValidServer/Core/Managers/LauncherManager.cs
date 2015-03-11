// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Service;
using IcarusCommons.Configurations;
using IcarusCommons.Models.Account;
using IcarusCommons.Models.Bridge;
using IcarusCommons.Utils;
using IcarusValidServer.Core.Services;

namespace IcarusValidServer.Core.Managers
{
    public static class LauncherManager
    {
        private static readonly Dictionary<TokenData, AccountData> Tokens = new Dictionary<TokenData, AccountData>();

        private static readonly object TokensLock = new object();

        public static void Init()
        {
            IScsServiceApplication lservice = ScsServiceBuilder.CreateService(new ScsTcpEndPoint
                (CIniLoader.ReadValue("NETWORK", "LAUNCHER_IP"),
                    Convert.ToInt32(CIniLoader.ReadValue("NETWORK", "LAUNCHER_PORT"))));

            lservice.AddService<ILauncherContract, Bridge>(new Bridge());
            lservice.Start();

            Log.Info("[{2}] started at {0}:{1}", CIniLoader.ReadValue("NETWORK", "LAUNCHER_IP"),
                Convert.ToInt32(CIniLoader.ReadValue("NETWORK", "LAUNCHER_PORT")), "LAUNCHER MANAGER");
        }

        public static void RegisterToken(AccountData data)
        {
            lock (TokensLock)
            {
                if (Tokens.ContainsValue(data))
                    Tokens.Remove(data.LoginToken); //If already added, but not validated, remove

                if (!Tokens.ContainsValue(data))
                    Tokens.Add(data.LoginToken, data);
            }
        }

        public static AccountData CheckedAccountData(string token)
        {
            var cached = Tokens.FirstOrDefault(s => s.Value.LoginToken.Key == token);

            int time = Extensions.RoundedUtc;
            if ((time - cached.Value.LoginToken.TokenCreationTime) > 10*60)
            {
                Log.Info("Token '{0}' time expired from client: {1}", token, cached.Value.Login);
                return null;
            }

            Log.Debug("SESSION ENDED:{0}", cached.Value.LoginToken.Key);
            lock (TokensLock)
                Tokens.Remove(cached.Value.LoginToken); // Remove Auth Token
            cached.Value.LoginToken = null;

            return cached.Value;
        }

        public class Bridge : ScsService, ILauncherContract
        {
            public string GetToken(string name, string password)
            {
                return AccountService.CheckAuth(name, password);
            }

            public string GetStatusAboutServers()
            {
                string connStatus = "";

                if (GameManager.GameServers.Any())
                    connStatus += "Online@";
                else if (!GameManager.GameServers.Any())
                    connStatus += "Offline@";
                if (LoginManager.IsConnected)
                    connStatus += "Online@";
                else if (!LoginManager.IsConnected)
                    connStatus += "Offline@";
                if (LobbyManager.LobbyServers.Any())
                    connStatus += "Online";
                else if (!LobbyManager.LobbyServers.Any())
                    connStatus += "Offline";

                return connStatus;
            }

            public StringCollection GetNews()
            {
                return Properties.Settings.Default.News;
            }

            public int GetTotalPlayersOnline()
            {
                return GameManager.GameServers.First(s => s.Value.Id == 1).Value.PlayersOnline;
            }
        }

        [ScsService]
        public interface ILauncherContract
        {
            string GetToken(string name, string password);
            string GetStatusAboutServers();
            StringCollection GetNews();
            int GetTotalPlayersOnline();
        }
    }
}