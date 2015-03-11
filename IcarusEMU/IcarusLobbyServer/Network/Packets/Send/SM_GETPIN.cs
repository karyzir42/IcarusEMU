// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusLobbyServer.Network.Packets.Processors;

namespace IcarusLobbyServer.Network.Packets.Send
{
    internal class SM_GETPIN : ASendPacket
    {
        protected int CharId;
        protected byte[] PinCode;

        public SM_GETPIN(int charId, byte[] pin)
        {
            CharId = charId;
            PinCode = pin;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteC(writer, 0);

            if (PinCode[0] > 0)
                WriteC(writer, 2); //Enter pin code
            else
                WriteC(writer, 1); //Make pin code

            WriteC(writer, 0); //pin errors
            WriteC(writer, 0);
            WriteD(writer, CharId);
        }
    }
}