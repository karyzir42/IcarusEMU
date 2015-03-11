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
using IcarusLobbyServer.Managers;
using IcarusLobbyServer.Network;

namespace IcarusLobbyServer
{
    internal class LobbyServer : Instance
    {
        public static TcpServer<Connection> TcpServer { get; set; }

        #region Imports

        private struct Rect
        {
            public int left, top, right, bottom;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hWnd, out Rect rc);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, bool repaint);

        #endregion

        private static void Main()
        {
            Global = new LobbyServer();
        }

        public override void Initilize()
        {
            Console.Title = "ICARUS LOBBY SERVER";
            Console.ForegroundColor = ConsoleColor.Green;

            IsWorking = true;

            #region Console options

            IntPtr hWin = GetConsoleWindow();
            Rect rc;
            GetWindowRect(hWin, out rc);
            MoveWindow(hWin, 0, rc.right - rc.left, rc.right - rc.left, rc.bottom - rc.top, false);

            #endregion

            Console.WriteLine("*************************\n" +
                              " ICARUS LOBBY SERVER\n" +
                              " Core version:{0}        \n" +
                              " Author:karyzir          \n" +
                              "*************************\n",
                1);

            LobbyManager.Init();

            TcpServer = new TcpServer<Connection>("127.0.0.1", 5695, 100);

            Log.Info("ICARUS ONLINE LOGIN SERVER STARTED AT {0}:{1}", CIniLoader.ReadValue("NETWORK", "LOBBYSERVER_IP"),
                CIniLoader.ReadValue("NETWORK", "LOBBYSERVER_PORT"));
            Start();
        }
    }
}