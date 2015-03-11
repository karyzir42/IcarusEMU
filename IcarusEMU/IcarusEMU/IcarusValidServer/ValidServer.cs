// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using IcarusCommons;
using IcarusCommons.DAO.MongoDatabase;
using IcarusCommons.Utils;
using IcarusValidServer.Core.DAO.MongoDatabase;
using IcarusValidServer.Core.Managers;
using IcarusValidServer.Properties;

/*
 * AUTHOR:KARYZIR 
 * 2015 © all right reserved
 */

namespace IcarusValidServer
{
    internal class ValidServer : Instance
    {
        private static void Main()
        {
            Console.Title = "ICARUS VALIDATION SERVER";
            Console.ForegroundColor = ConsoleColor.Green;

            Global = new ValidServer();
        }

        public override void Initilize()
        {
            IsWorking = true;

            Console.WriteLine("*************************\n" +
                              " ICARUS VALIDATION SERVER\n" +
                              " Core version:{0}        \n" +
                              " Author:karyzir          \n" +
                              " Settings: server.ini readed\n" +
                              "*************************\n",
                Settings.Default.CoreVersion);

            TaskProcessor.Init();
            CharacterRepository.Init();
            LauncherManager.Init();
            LoginManager.Init();
            LobbyManager.Init();
            GameManager.Init();

            MongoIdFactory.SetLastId(CharacterRepository.GetLastId());

            Start();
        }
    }
}