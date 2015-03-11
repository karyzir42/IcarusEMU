// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusLobbyServer.Network.Packets.Processors;
using IcarusLobbyServer.Network.Packets.Send;
using IcarusLobbyServer.Services;

namespace IcarusLobbyServer.Network.Packets.Recv
{
    internal class CM_STARTGAME : ARecvPacket
    {
        protected int CharacterId;

        public override void Read()
        {
            CharacterId = ReadD();
        }

        public override void Process()
        {
            if (CharacterService.UsePinCode)
                new SM_GETPIN(CharacterId, Connection.AccountData.PinCode).Send(Connection);
            else
            {
                new SM_PIN(CharacterId, true).Send(Connection);
                CharacterService.StartGameProceed(Connection, CharacterId);
            }
        }
    }
}