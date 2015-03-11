// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using IcarusCommons.Utils;
using IcarusLobbyServer.Network.Packets.Processors;
using IcarusLobbyServer.Network.Packets.Send;
using IcarusLobbyServer.Services;

namespace IcarusLobbyServer.Network.Packets.Recv
{
    internal class CM_PIN : ARecvPacket
    {
        protected byte[] PinBuff = new byte[4];
        protected int Action;
        protected int CharId;

        public override void Read()
        {
            ReadC();
            Action = ReadC();
            ReadH();
            CharId = ReadD();
            PinBuff = ReadB(4);
            ReadD();
        }

        public override void Process()
        {
            if (Action == 1 && Connection.AccountData.PinCode[0] <= 0) //Make pin
            {
                Connection.AccountData.PinCode = PinBuff;

                new SM_STARTGAME(CharId).Send(Connection);
                CharacterService.UpdateAccount(Connection.AccountData);

                return;
            }
            int curr = BitConverter.ToInt32(PinBuff, 0);
            int pl = BitConverter.ToInt32(Connection.AccountData.PinCode, 0);

            if (curr != pl)
            {
                Log.Info("Account is not validet! {0}", Connection.AccountData.Login);
                return;
            }
            new SM_STARTGAME(CharId).Send(Connection);
            new SM_PIN(CharId, true).Send(Connection);
        }
    }
}