// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.Runtime.InteropServices;
using IcarusCommons;
using IcarusCommons.Configurations;
using IcarusCommons.Network;
using IcarusCommons.Utils;
using IcarusGameServer.Managers;
using IcarusGameServer.Network;
using IcarusGameServer.Services;

namespace IcarusGameServer
{
    internal class GameServer : Instance
    {
        public static TcpServer<Connection> TcpServer { get; set; }

        private static void Main()
        {
            Global = new GameServer();
        }

        #region Imports

        private struct Rect
        {
#pragma warning disable 649
            public int left, top, right, bottom;
#pragma warning restore 649
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hWnd, out Rect rc);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, bool repaint);

        #endregion

        public override void Initilize()
        {
            Console.Title = "ICARUS GAME SERVER";
            Console.ForegroundColor = ConsoleColor.Green;

            IsWorking = true;

            #region Console options

            IntPtr hWin = GetConsoleWindow();
            Rect rc;
            GetWindowRect(hWin, out rc);
            MoveWindow(hWin, 0, rc.bottom - rc.top, rc.right - rc.left, rc.bottom - rc.top, false);

            #endregion

            Console.WriteLine("*************************\n" +
                              " ICARUS GAME SERVER      \n" +
                              " Author:karyzir          \n" +
                              "*************************\n");

            TaskProcessor.Init();
            PacketHandler.Init();
            GameManager.Init();

            InstanceService.InitilizeInstance();

            TcpServer = new TcpServer<Connection>
                (CIniLoader.ReadValue("NETWORK", "GAMESERVER_IP"), int.Parse(CIniLoader.ReadValue("NETWORK", "GAMESERVER_PORT")), 1000);

            Log.Info("ICARUS ONLINE GAME SERVER STARTED AT {0}:{1}", CIniLoader.ReadValue("NETWORK", "GAMESERVER_IP"), CIniLoader.ReadValue("NETWORK", "GAMESERVER_PORT"));

            Start();
        }
    }
}