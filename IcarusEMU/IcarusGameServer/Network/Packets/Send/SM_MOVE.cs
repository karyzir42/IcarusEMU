// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusCommons.Models.Player;
using IcarusCommons.Structures.Global;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Send
{
    internal class SM_MOVE : ASendPacket
    {
        public Position OldPosition;
        public Position NewPosition;
        protected Player Player;
        protected byte MovementStatus;

        public SM_MOVE(Player player, Position old, Position newPos, byte movementStatus = 1)
        {
            OldPosition = old;
            NewPosition = newPos;
            MovementStatus = movementStatus;
            Player = player;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteC(writer, (byte) NewPosition.MovementType); // 0 - идти / 1 - бежать
            WriteC(writer, MovementStatus); // 0 - стоять / 1 - двигаться
            WriteC(writer, 0);
            WriteC(writer, 0x88);
            WriteD(writer, Player.Id);

            WriteFn(writer, OldPosition.X);
            WriteFn(writer, OldPosition.Y);
            WriteFn(writer, OldPosition.Z);

            WriteFn(writer, NewPosition.X);
            WriteFn(writer, NewPosition.Y);
            WriteFn(writer, NewPosition.Z);
        }

        //01 01 00 Type and status
        //63 A9 30 02 Player Id
        //80 - unk
        //FD E1 2C 44 x
        //72 2A 34 44 y
        //F2 2F DA 41 z

        //33 5C 2E 44 x1
        //AB D8 31 44 y1
        //F2 2F DA 41 z1

        //DF 30 11 3F unk heading??
        //00 00 00 00 D   

        //01 01 00 63 A9 30 02 80 69 2B 2D 44  
        //46 81 34 44 B0 E0 D9 41 4E 47 2E 44 0E FD 31 44   
        //B0 E0 D9 41 11 85 D4 3E 00 00 00 00     

        //01 01 00 34 A9 30 02 80 17 E4 2A 44  
        //18 25 35 44 CD CC D6 41 35 5B 2D 44 27 ED 33 44  
        //CD CC D6 41 AA 4D 8E 3F 00 00 00 00               
    }
}