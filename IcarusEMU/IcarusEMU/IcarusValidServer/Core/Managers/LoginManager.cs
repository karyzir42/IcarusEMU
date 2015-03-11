// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.Collections.Generic;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Service;
using IcarusCommons.Configurations;
using IcarusCommons.Models.Account;
using IcarusCommons.Models.Bridge;
using IcarusCommons.Utils;

namespace IcarusValidServer.Core.Managers
{
    public static class LoginManager
    {
        public static bool IsConnected = false;

        public static void Init()
        {
            var lservice =
                ScsServiceBuilder.CreateService(new ScsTcpEndPoint(CIniLoader.ReadValue("NETWORK", "LOGIN_IP"),
                    Convert.ToInt32(CIniLoader.ReadValue("NETWORK", "LOGIN_BRIDGE_PORT"))));

            lservice.AddService<ILoginContract, LoginBridge>(new LoginBridge());
            lservice.Start();
            lservice.ClientConnected += lservice_ClientConnected;
            lservice.ClientDisconnected += lservice_ClientDisconnected;

            Log.Info("[{2}] started at {0}:{1}", CIniLoader.ReadValue("NETWORK", "LOGIN_IP"),
                Convert.ToInt32(CIniLoader.ReadValue("NETWORK", "LOGIN_BRIDGE_PORT")), "LOGIN MANAGER");
        }

        private static void lservice_ClientDisconnected(object sender, ServiceClientEventArgs e)
        {
            Log.Info("LOGIN SERVER DISCONNECTED");
            IsConnected = false;
        }

        private static void lservice_ClientConnected(object sender, ServiceClientEventArgs e)
        {
            Log.Info("LOGIN SERVER CONNECTED");
            IsConnected = true;
        }

        public class LoginBridge : ScsService, ILoginContract
        {
            public AccountData CheckedAccountData(string token)
            {
                return LauncherManager.CheckedAccountData(token); //Возвращяем данные логин серверу после валидации
            }

            public List<LobbyServerData> GetLobbyServers()
            {
                return LobbyManager.GetLobbyServers();
            }
        }

        [ScsService]
        public interface ILoginContract
        {
            AccountData CheckedAccountData(string token);
            List<LobbyServerData> GetLobbyServers();
        }
    }
}