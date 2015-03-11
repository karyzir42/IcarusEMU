// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusLoginServer.Managers;
using IcarusLoginServer.Network.Packets.Processors;

namespace IcarusLoginServer.Network.Packets.Send
{
    internal class SM_SELECTSERVER : ASendPacket
    {
        protected int SelectedServer;
        protected Connection Connection;

        public SM_SELECTSERVER(Connection conection, int selectedServer)
        {
            Connection = conection;
            SelectedServer = selectedServer;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteH(writer, 0);
            WriteH(writer, 0);

            var data = LoginManager.LoginServiceClient.ServiceProxy.GetLobbyServers()[SelectedServer - 1];

            WriteB(writer, data.Ip);
            WriteD(writer, data.Port);
            WriteD(writer, Connection.AccountData.Id);
            WriteD(writer, 0x4c4c11be);
        }
    }
}