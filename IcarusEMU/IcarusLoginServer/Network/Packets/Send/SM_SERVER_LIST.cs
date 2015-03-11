// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using IcarusCommons.Models.Bridge;
using IcarusCommons.Utils;
using IcarusLoginServer.Network.Packets.Processors;

namespace IcarusLoginServer.Network.Packets.Send
{
    internal class SM_SERVER_LIST : ASendPacket
    {
        protected List<LobbyServerData> ServerData { get; set; }

        public SM_SERVER_LIST(List<LobbyServerData> serverList)
        {
            ServerData = serverList;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, 0);
            WriteD(writer, 5);

            int nnum = ServerData.Count(lobbyServerData => lobbyServerData.Id != -1);

            WriteD(writer, nnum);

            var serverName = new byte[42];

            foreach (var lobbyServerData in ServerData)
            {
                int maxPlayers = lobbyServerData.MaxPlayers;
                int players = lobbyServerData.PlayersOnline;

                for (int i = 0; i < Extensions.GetBytes(lobbyServerData.Name).Length; i++)
                    serverName[i] = Extensions.GetBytes(lobbyServerData.Name)[i];

                WriteD(writer, lobbyServerData.Id);
                WriteB(writer, serverName);
                WriteH(writer, 1);
                WriteD(writer, 1);

                if ((maxPlayers/4*1) > players)
                {
                    WriteD(writer, 0);
                }
                else if ((maxPlayers/4*2) < players)
                {
                    WriteD(writer, 1);
                }
                else if ((maxPlayers/4*3) < players)
                {
                    WriteD(writer, 2);
                }
                else
                {
                    WriteD(writer, 3);
                }

                WriteB(writer, new byte[0]);
                WriteB(writer, new byte[2]);
                WriteH(writer, 0);
            }
        }
    }
}