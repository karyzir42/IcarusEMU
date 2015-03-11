// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusGameServer.Network.Packets.Processors;
using IcarusGameServer.Network.Packets.Send;

namespace IcarusGameServer.Network.Packets.Recv
{
    internal class CM_MOVE_STOP : ARecvPacket
    {
        public override void Read()
        {
            ReadD();
            Connection.ActivePlayer.Position.X = ReadF();
            Connection.ActivePlayer.Position.Y = ReadF();
            Connection.ActivePlayer.Position.Z = ReadF();
            ReadF(); //Heading??
            ReadF(); //Heading??
        }

        public override void Process()
        {
            new SM_MOVE(Connection.ActivePlayer, Connection.ActivePlayer.Position, Connection.ActivePlayer.Position, 0)
                .Send(Connection);
        }
    }
}