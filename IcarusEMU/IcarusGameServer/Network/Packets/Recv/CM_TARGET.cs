// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusCommons.Models.Battle;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Recv
{
    internal class CM_TARGET : ARecvPacket
    {
        protected int TargetId;
        protected int Unk2;

        public override void Read()
        {
            TargetId = ReadD();
            Unk2 = ReadD();
        }

        public override void Process()
        {
            if (Connection.ActivePlayer.BattleModel == null)
                Connection.ActivePlayer.BattleModel = new BattleModel();

            Connection.ActivePlayer.BattleModel.TargetId = TargetId;
        }
    }
}