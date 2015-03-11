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
    internal class SM_SKILL_PANEL : ASendPacket
    {
        protected List<Skill> Skills;

        public SM_SKILL_PANEL(List<Skill> skills)
        {
            Skills = skills;
        }

        public override void Write(BinaryWriter writer)
        {
            //WriteB(writer, "010006000100000000000000" +
            //               "FFFFFFFF 4E000500 FFFFFFFF000000000100000000000140" +
            //               "FFFFFFFF 28000500 FFFFFFFF000000000100000000000233" +
            //               "FFFFFFFF 2E000500 FFFFFFFF000000000100000000000300" +
            //               "FFFFFFFF 64000500 FFFFFFFF000000000200000000000600" +
            //               "FFFFFFFF 03000000 FFFFFFFF0000000001000000020000FF" +
            //               "FFFFFFFF 4E000500 FFFFFFFF00000000");

            //WriteH(writer, 1);
            //WriteH(writer, (short) Skills.Count);
            //foreach (var skill in Skills)
            //{
            //    WriteD(writer, 1);
            //    WriteD(writer, 0);
            //    WriteD(writer, 0xffffffff);
            //    WriteD(writer, skill.SkillId);
            //    WriteD(writer, 0xffffffff);
            //    WriteD(writer, 0);
            //}

            WriteH(writer, 1);
            WriteH(writer, 3);

            WriteD(writer, 1);
            WriteD(writer, 0);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0x0002007f);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0);

            WriteD(writer, 1);
            WriteD(writer, 0x40010000);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0x0002003b);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0);

            WriteD(writer, 1);
            WriteD(writer, 0x3c020000);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0x0002006b);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0);
        }
    }
}