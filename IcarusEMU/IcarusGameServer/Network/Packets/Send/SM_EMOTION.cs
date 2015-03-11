// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusCommons.Models.Player;
using IcarusCommons.Utils;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Send
{
    internal class SM_EMOTION : ASendPacket
    {
        protected string Emotion;
        protected Player Player;
        protected byte[] EmotionBuffer = new byte[70];

        public SM_EMOTION(Player player, string emotion)
        {
            Emotion = emotion;
            Player = player;
        }

        public override void Write(BinaryWriter writer)
        {
            for (int i = 0; i < Extensions.GetBytes(Emotion).Length; i++)
                EmotionBuffer[i] = Extensions.GetBytes(Emotion)[i];

            WriteD(writer, Player.Id);
            if (Player.BattleModel == null)
                WriteD(writer, 0xffffffff);
            else
                WriteD(writer, Player.BattleModel.TargetId);
            WriteSs(writer, Emotion);
        }
    }
}