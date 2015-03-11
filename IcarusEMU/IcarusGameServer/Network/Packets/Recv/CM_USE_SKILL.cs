// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusGameServer.Network.Packets.Processors;
using IcarusGameServer.Network.Packets.Send;

namespace IcarusGameServer.Network.Packets.Recv
{
    internal class CM_USE_SKILL : ARecvPacket
    {
        protected int Unk1;
        protected int Unk2;
        protected int Unk3;
        protected int Unk4;
        protected int Unk5;
        protected int Unk6;
        protected int Unk7;
        protected int Unk8;
        protected int Unk9;
        protected int Unk10;
        protected int Unk11;
        protected int CharacterId;

        public override void Read()
        {
            Unk1 = ReadD();
            CharacterId = ReadD();
            Unk2 = ReadD();
            Unk3 = ReadD();
            Unk4 = ReadD();
            Unk5 = ReadD();
            Unk6 = ReadD();
            Unk7 = ReadD();
            Unk8 = ReadD();
            Unk9 = ReadD();
            Unk10 = ReadD();
            Unk11 = ReadD();
        }

        public override void Process()
        {
            new SM_UNK_25(Connection.ActivePlayer, Unk5, Unk8, Unk9, Unk10, Unk11).Send(Connection);
            new SM_USE_SKILL(Unk5).Send(Connection);
            new SM_ANIMATION(Connection.ActivePlayer, Unk8, Unk9).Send(Connection);
        }
    }
}