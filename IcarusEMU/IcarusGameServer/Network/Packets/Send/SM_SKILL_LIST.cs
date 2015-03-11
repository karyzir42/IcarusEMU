// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.Collections.Generic;
using System.IO;
using IcarusCommons.Models.Battle;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Send
{
    internal class SM_SKILL_LIST : ASendPacket
    {
        protected List<Skill> Skills;

        public SM_SKILL_LIST(List<Skill> skills)
        {
            Skills = skills;
        }

        public override void Write(BinaryWriter writer)
        {
            //WriteD(writer, 0);
            //WriteC(writer, 0);
            //WriteC(writer, (byte) Skills.Count);
            //WriteH(writer, 0);

            //foreach (var skill in Skills)
            //{
            //    WriteD(writer, skill.SkillId);
            //    WriteD(writer, 2);
            //    WriteD(writer, 0);
            //}    

            WriteD(writer, 0);
            WriteC(writer, 0);
            WriteC(writer, 3);
            WriteH(writer, 0);

            WriteH(writer, 0x7f);
            WriteH(writer, 2);
            WriteD(writer, 1);
            WriteD(writer, 0);

            WriteH(writer, 0x3b);
            WriteH(writer, 2);
            WriteD(writer, 2);
            WriteD(writer, 0);

            WriteH(writer, 0x6b);
            WriteH(writer, 2);
            WriteD(writer, 2);
            WriteD(writer, 0);
        }
    }
}