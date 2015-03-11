// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.Collections.Generic;
using Hik.Communication.Scs.Communication.Messages;
using IcarusCommons.Utils;
using IcarusGameServer.Network.Packets.Processors;
using IcarusGameServer.Network.Packets.Recv;
using IcarusGameServer.Network.Packets.Send;
using IcarusGameServer.Network.Protocols;

namespace IcarusGameServer.Network
{
    internal class PacketHandler
    {
        public static Dictionary<ushort, Type> RecvPackets = new Dictionary<ushort, Type>
        {
            {0x0165, typeof (CM_PING)},
            {0x010e, typeof (CM_CONNECT)},
            {0x320e, typeof (CM_GETAREA)},
            {0xfd0e, typeof (CM_UNK2)},
            {0x3c0e, typeof (CM_UNK4)},
            {0x460e, typeof (CM_UNK5)},
            {0x8d0e, typeof (CM_UNK6)},
            {0x900e, typeof (CM_UNK7)},
            {0xa00e, typeof (CM_UNK11)},
            {0x1406, typeof (CM_UNK25)},
            {0x0127, typeof (CM_CHARACTER_STYLE)},
            {0x340e, typeof (CM_INVENTORY)},
            {0x1e13, typeof (CM_INVENTORY_MOVE)},
            {0x360e, typeof (CM_SKILL_LIST)},
            {0x4a0e, typeof (CM_SKILL_PANEL)},
            {0x0111, typeof (CM_CHAT)},
            {0x0610, typeof (CM_MOVE)},
            {0x920e, typeof (CM_EXIT)},
            {0xfa0e, typeof (CM_SELECTCHARACTERS)},
            {0xaa18, typeof (CM_EMOTION)},
            {0x0b18, typeof (CM_USE_SKILL)},
            {0x640f, typeof (CM_TARGET)},
            {0x0110, typeof (CM_MOUSESET)},
            {0x0c10, typeof (CM_FLY)},
            {0x0f13, typeof (CM_REMOVE_ITEM)},
            {0x0410, typeof (CM_MOVE_STOP)},
            {0x0a10, typeof (CM_UPDATE_POSITION)},
            {0x0810, typeof (CM_JUMP)},
        };

        public static Dictionary<Type, ushort> SendPackets = new Dictionary<Type, ushort>
        {
            {typeof (SM_PONG), 0x0165},
            {typeof (SM_CONNECT), 0x020e},
            {typeof (SM_SETAREA), 0x330e},
            {typeof (SM_ENTER_WORLD), 0x010f},
            {typeof (SM_UNK1), 0x440e},
            {typeof (SM_UNK3), 0x8c0f},
            {typeof (SM_UNK4), 0xfe0e},
            {typeof (SM_UNK7), 0x3d0e},
            {typeof (SM_UNK8), 0x470e},
            {typeof (SM_UNK9), 0x8e0e},
            {typeof (SM_UNK10), 0x910e},
            {typeof (SM_UNK17), 0xa10e},
            {typeof (SM_UNK20), 0x3f0e},
            {typeof (SM_UNK21), 0x030f},
            {typeof (SM_UNK40), 0x9913},
            {typeof (SM_UNK1506), 0x1506},
            {typeof (SM_UNK1606), 0x1606},
            {typeof (SM_CHARACTER_STYLE), 0x0227},
            {typeof (SM_INVENTORY_RESPONSE), 0x350e},
            {typeof (SM_SKILL_PANEL), 0x4c0e},
            {typeof (SM_SKILL_LIST), 0x380e},
            {typeof (SM_CHAT_MESSAGE), 0x0311},
            {typeof (SM_ANIMATION), 0x1f18},
            {typeof (SM_GAMEOBJECT_SPAWN), 0x1b10},
            {typeof (SM_GAMEOBJECT_MOVE), 0x0310},
            {typeof (SM_DISCONNECT), 0xfb0e},
            {typeof (SM_CHANGE_CHARACTER), 0x700e},
            {typeof (SM_EXIT), 0x6f0e},
            {typeof (SM_EMOTION), 0xab18},
            {typeof (SM_USE_SKILL), 0x0c18},
            {typeof (SM_UNK_25), 0x2018},
            {typeof (SM_MOVE), 0x0710},
            {typeof (SM_MOUSE_MOVE), 0x0210},
            {typeof (SM_PLAYER_INFO), 0x1710},
            {typeof (SM_FLY), 0x0d10},
            {typeof (SM_REMOVE_ITEM_RESP), 0x1013},
            {typeof (SM_REMOVE_ITEM), 0x0213},
        };

        public static void Init()
        {
            Log.Info("[Server packets:{0} readed]", SendPackets.Count);
            Log.Info("[Client packets:{0} readed]", RecvPackets.Count);
        }

        public static void HandleIncomingPacket(Connection connection, IScsMessage message)
        {
            var msg = message as GameMessage;
            if (msg == null)
                return;

            if (RecvPackets.ContainsKey(msg.OpCode))
            {
                string opCodeLittleEndianHex = BitConverter.GetBytes(msg.OpCode).ToHex();
                if (msg.OpCode != 0x0165 && msg.OpCode == 0x0610)
                    Log.Info("Received packet:{0}[{2}] with data:\n{1}", RecvPackets[msg.OpCode].Name,
                        msg.Data.FormatHex(), opCodeLittleEndianHex);

                ((ARecvPacket) Activator.CreateInstance(RecvPackets[msg.OpCode])).Process(connection, msg.Data);
            }
            else
            {
                string opCodeLittleEndianHex = BitConverter.GetBytes(msg.OpCode).ToHex();
                Log.Info("Unknown packet opCode: 0x{0}{1} [{2}]\n",
                    opCodeLittleEndianHex.Substring(2),
                    opCodeLittleEndianHex.Substring(0, 2),
                    msg.Data.Length);

                Log.Info("Data:\n{0}\n", msg.Data.FormatHex());
            }
        }

        public static void BroadcastMessage(ASendPacket packet)
        {
            var connections = GameServer.TcpServer.Connections;
            foreach (var connection in connections)
                packet.Send(connection);
        }
    }
}