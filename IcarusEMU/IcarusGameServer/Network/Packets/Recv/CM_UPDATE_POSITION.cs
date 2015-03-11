// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Recv
{
    internal class CM_UPDATE_POSITION : ARecvPacket
    {
        public override void Read()
        {
            Connection.ActivePlayer.Position.X = ReadF();
            Connection.ActivePlayer.Position.Y = ReadF();
            Connection.ActivePlayer.Position.Z = ReadF();
        }
    }
}