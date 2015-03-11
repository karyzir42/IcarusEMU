// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.Linq;
using IcarusCommons.Utils;
using IcarusGameServer.Network.Packets.Processors;
using IcarusGameServer.Network.Packets.Send;

namespace IcarusGameServer.Network.Packets.Recv
{
    internal class CM_REMOVE_ITEM : ARecvPacket
    {
        protected int SId;
        protected bool IsItem = false;

        public override void Read()
        {
            SId = ReadH();
            ReadH();
            ReadD();
        }

        public override void Process()
        {
            if (Connection.ActivePlayer.PlayerData.Storage.Storage.First(s => s.SId == SId) != null)
            {
                var toDel = Connection.ActivePlayer.PlayerData.Storage.Storage.First(s => s.SId == SId);
                if (toDel != null)
                {
                    IsItem = true;
                    Connection.ActivePlayer.PlayerData.Storage.Storage.Remove(toDel);
                    Log.Debug("ITEM {0} REMOVED", toDel.ItemId);
                }
            }
            new SM_REMOVE_ITEM_RESP(IsItem).Send(Connection);
            if (IsItem)
                new SM_REMOVE_ITEM(SId).Send(Connection);
        }
    }
}