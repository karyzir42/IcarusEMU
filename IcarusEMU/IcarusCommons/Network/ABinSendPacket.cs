// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using IcarusCommons.Network.Interface;
using IcarusCommons.Utils;

namespace IcarusCommons.Network
{
    public abstract class ABinSPacket
    {
        protected byte[] Data;
        protected object WriteLock = new object();

        /// <summary>
        ///     Send packet to multiple states
        /// </summary>
        public void Send(params IConnection[] connections)
        {
            foreach (IConnection t in connections)
                Send(t);
        }

        /// <summary>
        ///     Send packet to multiple states
        /// </summary>
        public void Send(IEnumerable<IConnection> connections)
        {
            foreach (IConnection state in connections)
                Send(state);
        }

        /// <summary>
        ///     Write an integer to target binary writer.
        /// </summary>
        /// <param name="writer">Target writer.</param>
        /// <param name="val">Number.</param>
        protected void WriteD(BinaryWriter writer, int val)
        {
            writer.Write(val);
        }

        /// <summary>
        ///     Write a unsigned integer to target binary writer.
        /// </summary>
        /// <param name="writer">Target writer.</param>
        /// <param name="val">Number.</param>
        protected void WriteD(BinaryWriter writer, uint val)
        {
            writer.Write(val);
        }

        /// <summary>
        ///     Write big-endian integer number.
        /// </summary>
        /// <param name="writer">Target binary writer.</param>
        /// <param name="val">Value to write.</param>
        protected void WriteE(BinaryWriter writer, int val)
        {
            writer.Write((byte) ((val & 0xff000000) >> 24));
            writer.Write((byte) ((val & 0x00ff0000) >> 16));
            writer.Write((byte) ((val & 0x0000ff00) >> 8));
            writer.Write((byte) (val & 0x000000ff));
        }

        /// <summary>
        ///     Write big-endian integer number.
        /// </summary>
        /// <param name="writer">Target binary writer.</param>
        /// <param name="val">Value to write.</param>
        protected void WriteE(BinaryWriter writer, uint val)
        {
            writer.Write((byte) ((val & 0xff000000) >> 24));
            writer.Write((byte) ((val & 0x00ff0000) >> 16));
            writer.Write((byte) ((val & 0x0000ff00) >> 8));
            writer.Write((byte) (val & 0x000000ff));
        }

        /// <summary>
        ///     Write short to target binary writer
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="val"></param>
        protected void WriteH(BinaryWriter writer, short val)
        {
            writer.Write(val);
        }

        /// <summary>
        ///     Write unsigned short to the target binary writer.
        /// </summary>
        /// <param name="writer">Target binary writer.</param>
        /// <param name="val">Value.</param>
        protected void WriteH(BinaryWriter writer, ushort val)
        {
            writer.Write(val);
        }

        /// <summary>
        ///     Write byte to target binary writer
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="val"></param>
        protected void WriteC(BinaryWriter writer, byte val)
        {
            writer.Write(val);
        }

        /// <summary>
        ///     Write byte to target binary writer
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="eEnum"></param>
        protected void WriteC(BinaryWriter writer, Enum eEnum)
        {
            writer.Write((byte) eEnum.GetHashCode());
        }

        /// <summary>
        ///     Write double to target binary writer
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="val"></param>
        protected void WriteDf(BinaryWriter writer, double val)
        {
            writer.Write(val);
        }

        /// <summary>
        ///     Write float to target binary writer
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="val"></param>
        protected void WriteF(BinaryWriter writer, float val)
        {
            writer.Write(val);
        }

        protected void WriteFn(BinaryWriter writer, float val)
        {
            //FDE12C44
            //442CE1FD
            //691.531067

            byte[] toTransform = BitConverter.GetBytes(val);
            string hex = toTransform.ToArray().ToHex();

            WriteB(writer, hex);
        }

        /// <summary>
        ///     Write long to target binary writer.
        /// </summary>
        /// <param name="writer">Target writer.</param>
        /// <param name="val">Value.</param>
        protected void WriteQ(BinaryWriter writer, long val)
        {
            writer.Write(val);
        }

        /// <summary>
        ///     Write unsigned long to target binary writer.
        /// </summary>
        /// <param name="writer">Target writer.</param>
        /// <param name="val">Value.</param>
        protected void WriteQ(BinaryWriter writer, ulong val)
        {
            writer.Write(val);
        }

        /// <summary>
        ///     Write unicode string to target binary writer
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="text"></param>
        protected void WriteS(BinaryWriter writer, String text)
        {
            if (text == null)
            {
                writer.Write((short) 0);
            }
            else
            {
                WriteH(writer, (short) text.Length);
                writer.Write(Encoding.Unicode.GetBytes(text));
            }
        }

        /// <summary>
        ///     Write unicode string to target binary writer
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="text"></param>
        /// <param name="padding"></param>
        protected void WriteS(BinaryWriter writer, String text, int padding = 0)
        {
            if (text == null)
            {
                writer.Write((short) 0);
            }
            else
            {
                Encoding encoding = Encoding.Unicode;
                writer.Write(encoding.GetBytes(text));
                writer.Write((short) 0);
            }

            if (padding > 0)
            {
                Encoding encoding = Encoding.Unicode;
                for (int x = encoding.GetBytes(text).Length; x < (padding - 2); x++)
                    writer.Write((byte) 0);
            }
        }

        protected void WriteSs(BinaryWriter writer, String text, int padding = 0)
        {
            if (text == null)
            {
                writer.Write((short) 0);
            }
            else
            {
                Encoding encoding = Encoding.ASCII;
                writer.Write(encoding.GetBytes(text));
                writer.Write((byte) 0);
                writer.Write((short) 0);
            }

            if (padding > 0)
            {
                Encoding encoding = Encoding.ASCII;
                for (int x = encoding.GetBytes(text).Length; x < padding; x++)
                    writer.Write((byte) 0);
            }
        }

        /// <summary>
        ///     Write byte array to target binary writer
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="hex">byte array as hex string</param>
        protected void WriteB(BinaryWriter writer, string hex)
        {
            writer.Write(hex.ToBytes());
        }

        /// <summary>
        ///     Write byte array to target binary writer
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="data"></param>
        protected void WriteB(BinaryWriter writer, byte[] data)
        {
            writer.Write(data);
        }

        /// <summary>
        ///     Write Ip-address and port to the target binary writer.
        /// </summary>
        /// <param name="writer">Taget writer.</param>
        /// <param name="endPoint">Endpoint.</param>
        protected void WriteA(BinaryWriter writer, IPEndPoint endPoint)
        {
            byte[] address = endPoint.Address.GetAddressBytes();
            writer.Write(address);
            writer.Write((short) endPoint.Port);
        }

        /// <summary>
        ///     Send packet to valid IConnection member
        /// </summary>
        public abstract void Send(IConnection connection);
    }
}