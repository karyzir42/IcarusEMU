// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using IcarusCommons.Structures.Global;
using IcarusCommons.Utils;
using IcarusGameServer.Network.Packets.Processors;
using IcarusGameServer.Network.Packets.Send;

namespace IcarusGameServer.Network.Packets.Recv
{
    internal class CM_MOVE : ARecvPacket
    {
        protected int MovementType;
        protected int MovementStatus;
        protected float NowX;
        protected float NowY;
        protected float NowZ;

        protected float ToX;
        protected float ToY;
        protected float ToZ;

        protected float UnkH;
        protected int UnkD;

        public override void Read()
        {
            MovementType = ReadC(); // 0 - идти / 1 - бежать
            MovementStatus = ReadC(); // 0 - стоять / 1 - двигаться

            UnkH = ReadH();
            UnkD = ReadD();

            NowX = ReadF();
            NowY = ReadF();
            NowZ = ReadF();

            ToX = ReadF();
            ToY = ReadF();
            ToZ = ReadF();
        }

        public override void Process()
        {
            Connection.ActivePlayer.PlayerData.Position = Connection.ActivePlayer.Position;
            Connection.ActivePlayer.Position.X = ToX;
            Connection.ActivePlayer.Position.Y = ToY;
            Connection.ActivePlayer.Position.Z = ToZ;
            Connection.ActivePlayer.Position.MovementType = MovementType;

            Connection.ActivePlayer.Instance.PlayerMove(Connection.ActivePlayer, Connection.ActivePlayer.Position);

            new SM_MOVE(Connection.ActivePlayer, new Position(NowX, NowY, NowZ), Connection.ActivePlayer.Position).Send(
                Connection);

            Console.Clear();
            Log.Debug("D:{0} " +
                      "H:{1} " +
                      "X:{2} " +
                      "Y:{3} " +
                      "Z:{4} ", UnkD, UnkH, NowX, NowY, NowZ);
        }
    }
}