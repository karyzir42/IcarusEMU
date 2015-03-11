// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.Collections.Generic;
using Hik.Communication.Scs.Communication.Messages;
using IcarusCommons.Utils;
using IcarusLoginServer.Network.Packets.Processors;
using IcarusLoginServer.Network.Packets.Recv;
using IcarusLoginServer.Network.Packets.Send;
using IcarusLoginServer.Network.Protocols;

namespace IcarusLoginServer.Network
{
    public class PacketHandler
    {
        public static Dictionary<ushort, Type> RecvPackets = new Dictionary<ushort, Type>
        {
            {0x0165, typeof (CM_PING)},
            {0x030B, typeof (CM_AUTH)},
            {0x0A0B, typeof (CM_SERVER_LIST)},
            {0x140B, typeof (CM_SELECTSERVER)}
        };

        public static Dictionary<Type, ushort> SendPackets = new Dictionary<Type, ushort>
        {
            {typeof (SM_PONG), 0x0165},
            {typeof (SM_AUTH), 0x040B},
            {typeof (SM_SERVER_LIST), 0x0B0B},
            {typeof (SM_SELECTSERVER), 0x150B},
            {typeof (SM_UNK1), 0x0E0B}
        };

        public static void HandleIncomingPacket(Connection connection, IScsMessage message)
        {
            var msg = message as LoginMessage;
            if (msg == null)
                return;

            if (RecvPackets.ContainsKey(msg.OpCode))
            {
                string opCodeLittleEndianHex = BitConverter.GetBytes(msg.OpCode).ToHex();

                Log.Info("Received packet:{0}[{2}] with data:\n{1}", RecvPackets[msg.OpCode].Name, msg.Data.FormatHex(),
                    opCodeLittleEndianHex);

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