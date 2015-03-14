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
using IcarusLoginServer.Managers;
using IcarusLoginServer.Network;
using IcarusLoginServer.Services;

namespace IcarusLoginServer
{
    internal class LoginServer : Instance
    {
        public static TcpServer<Connection> TcpServer { get; set; }

        private static void Main()
        {
            Global = new LoginServer();
        }

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

        public override void Initilize()
        {
            Console.Title = "ICARUS LOGIN SERVER";
            Console.ForegroundColor = ConsoleColor.Green;

            IsWorking = true;

            #region Console options

            IntPtr hWin = GetConsoleWindow();
            Rect rc;
            GetWindowRect(hWin, out rc);
            MoveWindow(hWin, 0, 0, rc.right - rc.left, rc.bottom - rc.top, false);

            #endregion

            Console.WriteLine("*************************\n" +
                              " ICARUS LOGIN SERVER     \n" +
                              " Core version:{0}        \n" +
                              " Author:karyzir          \n" +
                              "*************************\n",
                1);
            TaskProcessor.Init();
            DatabaseService.Initialize();//TODO

            TcpServer = new TcpServer<Connection>(CIniLoader.ReadValue("NETWORK", "LOGIN_IP"),
                int.Parse(CIniLoader.ReadValue("NETWORK", "LOGIN_PORT")), 100);

            LoginManager.Init();

            Log.Info("ICARUS ONLINE LOGIN SERVER STARTED AT {0}:{1}", CIniLoader.ReadValue("NETWORK", "LOGIN_IP"),
                CIniLoader.ReadValue("NETWORK", "LOGIN_PORT"));

            Start();
        }
    }
}