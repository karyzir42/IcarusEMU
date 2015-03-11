// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.Linq;
using IcarusCommons.Utils;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Recv
{
    internal class CM_INVENTORY_MOVE : ARecvPacket
    {
        protected short Sid;
        protected short Slot;
        private bool _findItem;

        public override void Read()
        {
            ReadD();
            Sid = ReadH();
            Slot = ReadH();
        }

        public override void Process()
        {
            foreach (var items in Connection.ActivePlayer.PlayerData.Storage.Storage.Where(items => items.SId == Sid))
            {
                items.Slot = Slot;
                _findItem = true;
            }
            if (!_findItem)
                Log.Info("NOT FOUND SID!! :{0}", Connection.ActivePlayer.PlayerData.Name);
        }
    }
}