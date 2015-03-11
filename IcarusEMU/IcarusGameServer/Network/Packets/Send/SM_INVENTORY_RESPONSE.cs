// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusCommons.Models.Player;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Send
{
    internal class SM_INVENTORY_RESPONSE : ASendPacket
    {
        protected byte Unk;
        protected PlayerData PlayerData;


        public SM_INVENTORY_RESPONSE(byte unk, PlayerData data)
        {
            Unk = unk;
            PlayerData = data;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, 0);
            WriteC(writer, Unk);
            WriteH(writer, (short) PlayerData.Storage.Storage.Count);
            WriteC(writer, 0);

            foreach (var item in PlayerData.Storage.Storage)
            {
                WriteD(writer, PlayerData.PlayerId);
                WriteD(writer, item.UId);
                WriteD(writer, item.ItemId);
                WriteD(writer, item.Count);
                WriteD(writer, item.IsEquip);
                WriteH(writer, item.SId);
                WriteH(writer, item.Slot);
            }
        }
    }
}