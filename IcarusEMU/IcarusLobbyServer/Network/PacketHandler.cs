// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.Collections.Generic;
using Hik.Communication.Scs.Communication.Messages;
using IcarusCommons.Utils;
using IcarusLobbyServer.Network.Packets.Processors;
using IcarusLobbyServer.Network.Packets.Recv;
using IcarusLobbyServer.Network.Packets.Send;
using IcarusLobbyServer.Network.Protocols;

namespace IcarusLobbyServer.Network
{
    internal class PacketHandler
    {
        public static Dictionary<ushort, Type> RecvPackets = new Dictionary<ushort, Type>
        {
            {0x0105, typeof (CM_CONNECT)},
            {0x0165, typeof (CM_PING)},
            {0x1005, typeof (CM_CHARACTERLIST)},
            {0x1205, typeof (CM_CREATECHARACTER)},
            {0x0e05, typeof (CM_DELETECHARACTER)},
            {0x2005, typeof (CM_STARTGAME)},
            {0x1405, typeof (CM_CHECKCHARACTERNAME)},
            {0x0a05, typeof (CM_RESTORE_CONNECTION)},
            {0xfa05, typeof (CM_CLOSE)},
            {0x0405, typeof (CM_PIN)},
        };

        public static Dictionary<Type, ushort> SendPackets = new Dictionary<Type, ushort>
        {
            {typeof (SM_CONNECT), 0x0205},
            {typeof (SM_PONG), 0x0165},
            {typeof (SM_CHARACTERLIST), 0x1105},
            {typeof (SM_CREATECHARACTER), 0x1305},
            {typeof (SM_DELETECHARACTER), 0x1605},
            {typeof (SM_DELETECHARACTER_PARTTWO), 0x1705},
            {typeof (SM_STARTGAME), 0x2105},
            {typeof (SM_LOBBYUSERMSG), 0x1505},
            {typeof (SM_RESTORE_CONNECTION), 0x0b05},
            {typeof (SM_PIN), 0x0505},
            {typeof (SM_GETPIN), 0x0305},
        };

        public static void HandleIncomingPacket(Connection connection, IScsMessage message)
        {
            var msg = message as LobbyMessage;
            if (msg == null)
                return;

            if (RecvPackets.ContainsKey(msg.OpCode))
            {
                string opCodeLittleEndianHex = BitConverter.GetBytes(msg.OpCode).ToHex();
                if (msg.OpCode != 0x0165)
                    //Log.Info("Received packet:{0}[{2}] with data:\n{1}", RecvPackets[msg.OpCode].Name,
                    //    msg.Data.FormatHex(), opCodeLittleEndianHex);

                    ((ARecvPacket) Activator.CreateInstance(RecvPackets[msg.OpCode])).Process(connection, msg.Data);
            }
            else
            {
                string opCodeLittleEndianHex = BitConverter.GetBytes(msg.OpCode).ToHex();
                Console.WriteLine("Unknown packet opCode: 0x{0}{1} [{2}]\n",
                    opCodeLittleEndianHex.Substring(2),
                    opCodeLittleEndianHex.Substring(0, 2),
                    msg.Data.Length);

                Console.WriteLine("Data:\n{0}\n", msg.Data.FormatHex());
            }
        }
    }
}