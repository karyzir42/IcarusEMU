// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.IO;
using IcarusCommons.Network;
using IcarusCommons.Network.Interface;
using IcarusCommons.Utils;

namespace IcarusGameServer.Network.Packets.Processors
{
    public abstract class ASendPacket : ABinSPacket
    {
        public override void Send(IConnection con)
        {
            if (con == null)
                return;

            ushort opCode = 0;

            if (PacketHandler.SendPackets.ContainsKey(GetType()))
                opCode = PacketHandler.SendPackets[GetType()];

            if (opCode == 0)
            {
                Log.Error("Can't find opcode for {0} packet type", GetType().Name);
                return;
            }

            if (Data == null)
            {
                try
                {
                    using (var stream = new MemoryStream())
                    {
                        using (var writer = new BinaryWriter(stream))
                        {
                            WriteH(writer, 0); //For len
                            WriteH(writer, opCode);
                            WriteH(writer, 0); //For crypto
                            Write(writer); //Some data

                            stream.Position = 0;

                            WriteH(writer, (ushort) stream.Length);
                        }

                        byte[] cdata = stream.ToArray();

                        if (GetType().Name != "SM_PONG")
                        {
                            Log.Info("Send packet '{0}'", GetType().Name);
#if DEBUG
                            Log.Debug("With data:\n{0}", cdata.FormatHex());
                        }
#endif
                        Data = cdata;
                    }
                }
                catch (Exception ex)
                {
                    Log.Warn("Can't write packet: {0}", GetType().Name);
                    Log.WarnException("ASendPacket", ex);
                    return;
                }
            }
            con.HandleData(Data);
        }

        public abstract void Write(BinaryWriter writer);
    }
}