// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusCommons.Structures.Global;
using IcarusGameServer.Network.Packets.Processors;
using IcarusGameServer.Network.Packets.Send;

namespace IcarusGameServer.Network.Packets.Recv
{
    internal class CM_MOUSESET : ARecvPacket
    {
        protected Position Pos;
        protected float ToX;
        protected float ToY;
        protected float ToZ;

        protected byte MovementType;
        protected byte MovementStatus;

        public override void Read()
        {
            MovementType = (byte) ReadC();
            MovementStatus = (byte) ReadC();

            ReadH();
            ReadD();

            Pos = new Position(ReadF(), ReadF(), ReadF());

            ToX = ReadF();
            ToY = ReadF();
            ToZ = ReadF();
        }

        public override void Process()
        {
            Connection.ActivePlayer.Position.X = ToX;
            Connection.ActivePlayer.Position.Y = ToY;
            Connection.ActivePlayer.Position.Z = ToZ;
            Connection.ActivePlayer.Position.MovementType = MovementType;
            Connection.ActivePlayer.Position.MovementStatus = MovementStatus;

            Connection.ActivePlayer.Instance.PlayerMove(Connection.ActivePlayer, Connection.ActivePlayer.Position);

            new SM_MOUSE_MOVE(Connection.ActivePlayer, Connection.ActivePlayer.Position, new Position(ToX, ToY, ToZ))
                .Send(Connection);
        }
    }
}