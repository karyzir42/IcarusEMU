// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Server;
using IcarusCommons.Network.Interface;
using IcarusCommons.Utils;

namespace IcarusCommons.Network
{
    public class TcpServer<TConnection> where TConnection : IConnection, new()
    {
        #region Fields

        /*
         * Resharper warning isn't interest for our case. 
         * The reason for this is that a static field in a generic type will not be shared among instances of different close constructed types. 
         * This means that for a generic class C<T> which has a static field X, the values of C<int>.X and C<string>.X have completely different, independent values.
         */
        public List<TConnection> Connections { get; private set; }
        public Dictionary<string, long> ConnectionsTime { get; private set; }

        protected string BindAddress;
        protected int BindPort;
        protected int MaxConnections;

        public IScsServer Server;

        #endregion

        public TcpServer(string bindAddress, int bindPort, int maxConnections)
        {
            Connections = new List<TConnection>();
            ConnectionsTime = new Dictionary<string, long>();

            BindAddress = bindAddress;
            BindPort = bindPort;
            MaxConnections = maxConnections;

            Log.Info("Start TcpServer at {0}:{1}...", BindAddress, BindPort);
            Server = ScsServerFactory.CreateServer(new ScsTcpEndPoint(BindAddress, BindPort));
            Server.Start();

            Server.ClientConnected += OnConnected;
            Server.ClientDisconnected += OnDisconnected;
        }

        /// <summary>
        ///     This method must call, when new client was connected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConnected(object sender, ServerClientEventArgs e)
        {
            Log.Info("New client connected!");
            if (
                !Prehandlecheck(
                    Regex.Match(e.Client.RemoteEndPoint.ToString(), "([0-9]+).([0-9]+).([0-9]+).([0-9]+)").Value))
                return;

            var con = new TConnection {Client = e.Client};
            con.InitilizeConnection();

            Connections.Add(con);
        }

        /// <summary>
        ///     This method must call, when client was disconnected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDisconnected(object sender, ServerClientEventArgs e)
        {
            Log.Info("Client disconnected!");

            foreach (TConnection connection in Connections.Where(connection => connection.Client.Equals(e.Client)))
            {
                Connections.Remove(connection);
                break;
            }

            GC.Collect();
        }

        ~TcpServer()
        {
            Connections.ForEach(c => c.CloseConnection(true));
            Connections.Clear();
            Connections = null;
        }


        /// <summary>
        ///     (Windows only!) This method will automaticaly add new firewall rule, if last reconnection delay is less than
        ///     ReconnectionDelay
        /// </summary>
        /// <param name="ip">Client IP</param>
        /// <returns></returns>
        private bool Prehandlecheck(string ip)
        {
            if (ConnectionsTime.ContainsKey(ip))
            {
                ConnectionsTime[ip] = Extensions.RoundedUtc;
            }
            else
                ConnectionsTime.Add(ip, Extensions.RoundedUtc);

            return true;
        }
    }
}