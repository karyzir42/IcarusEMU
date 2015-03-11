// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusCommons.Models.Player;
using IcarusLobbyServer.Network.Packets.Processors;
using IcarusLobbyServer.Services;

namespace IcarusLobbyServer.Network.Packets.Recv
{
    internal class CM_CREATECHARACTER : ARecvPacket
    {
        protected static PlayerData NewPlayerData = new PlayerData();
        protected byte[] CharacterName = new byte[20];

        public override void Read()
        {
            NewPlayerData.Style = new CHAR_STYLE();

            CharacterName = ReadB(20);

            string s = GetString(CharacterName);

            NewPlayerData.Name = s.Remove(s.IndexOf("\0", System.StringComparison.Ordinal));
            NewPlayerData.Style.unk2 = ReadD();
            NewPlayerData.Style.unk3 = ReadD();
            NewPlayerData.Style.unk4 = ReadD();
            NewPlayerData.Style.unk5 = ReadD();
            NewPlayerData.Style.unk6 = ReadH();

            NewPlayerData.ClassType = (ClassEnum) ReadC();
            NewPlayerData.Sex = (SexEnum) ReadC();
            ReadH();
            NewPlayerData.Style.unk81 = 0;
            NewPlayerData.Style.unk0 = ReadH();
            NewPlayerData.Style.unk1 = ReadD();

            NewPlayerData.Style.eye = ReadC();
            NewPlayerData.Style.unk2 = ReadC();
            NewPlayerData.Style.eyebrows = ReadC();
            NewPlayerData.Style.unk3 = ReadC();
            NewPlayerData.Style.iris = ReadC();
            NewPlayerData.Style.unk4 = ReadH();
            NewPlayerData.Style.unk5 = ReadC();
            NewPlayerData.Style.tatoo = ReadC();
            NewPlayerData.Style.unk6 = ReadH();
            NewPlayerData.Style.unk7 = ReadC();
            NewPlayerData.Style.unk8 = ReadH();
            NewPlayerData.Style.unk9 = ReadD();
            NewPlayerData.Style.hair = ReadC();
            NewPlayerData.Style.unk10 = ReadH();
            NewPlayerData.Style.unk11 = ReadC();
            NewPlayerData.Style.unk12 = ReadD();
            NewPlayerData.Style.unk13 = ReadD();
            NewPlayerData.Style.unk14 = ReadD();

            NewPlayerData.Style.color_lips = ReadD();
            NewPlayerData.Style.color_eyeb = ReadD();
            NewPlayerData.Style.color_iris = ReadD();
            NewPlayerData.Style.color_eyebrows = ReadD();
            NewPlayerData.Style.color_eye = ReadD();

            NewPlayerData.Style.unk15 = ReadD();
            NewPlayerData.Style.unk16 = ReadD();
            NewPlayerData.Style.unk17 = ReadD();
            NewPlayerData.Style.unk18 = ReadD();
            NewPlayerData.Style.unk19 = ReadD();
            NewPlayerData.Style.unk20 = ReadD();
            NewPlayerData.Style.unk21 = ReadD();
            NewPlayerData.Style.unk22 = ReadD();
            NewPlayerData.Style.unk23 = ReadD();
            NewPlayerData.Style.unk24 = ReadD();
            NewPlayerData.Style.unk25 = ReadD();
            NewPlayerData.Style.unk26 = ReadD();
            NewPlayerData.Style.unk27 = ReadD();
            NewPlayerData.Style.unk28 = ReadD();
            NewPlayerData.Style.unk29 = ReadD();
            NewPlayerData.Style.unk30 = ReadD();
            NewPlayerData.Style.unk31 = ReadD();
            NewPlayerData.Style.unk32 = ReadD();
            NewPlayerData.Style.unk33 = ReadD();
            NewPlayerData.Style.unk34 = 0; //ReadD();
            NewPlayerData.Style.unk35 = 0; //ReadD();
            NewPlayerData.Style.unk36 = 0; //ReadD();
        }

        private static string GetString(byte[] bytes)
        {
            var chars = new char[bytes.Length/sizeof (char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public override void Process()
        {
            CharacterService.CharacterCreationProcess(Connection, NewPlayerData);
        }
    }
}