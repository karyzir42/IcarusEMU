// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;
using IcarusCommons.Models.Account;
using IcarusCommons.Network.Interface;
using IcarusLobbyServer.Network.Protocols;

namespace IcarusLobbyServer.Network
{
    public class Connection : IConnection
    {
        public IScsServerClient Client { get; set; }
        public bool IsValid { get; set; }

        public AccountData AccountData { get; set; }

        ~Connection()
        {
            AccountData = null;
            Client = null;
        }

        public void InitilizeConnection()
        {
            IsValid = true; //TODO:Check black list

            Client.WireProtocol = new WireLobbyProtocol();

            Client.MessageReceived += Client_MessageReceived;
            Client.Disconnected += Client_Disconnected;
        }

        private void Client_Disconnected(object sender, EventArgs e)
        {
        }

        private void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            PacketHandler.HandleIncomingPacket(this, e.Message);
        }

        public void CloseConnection(bool force)
        {
            if (force)
                Client.Disconnect();
        }

        public void HandleData(byte[] data)
        {
            Client.SendMessage(new LobbyMessage {Data = data});
        }
    }
}