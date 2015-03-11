// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.IO;
using System.Text;
using Hik.Communication.Scs.Communication.Messages;
using IcarusCommons.Network.Interface;
using IcarusCommons.Utils;

namespace IcarusCommons.Network
{
    public abstract class ABinRPacket
    {
        public BinaryReader Reader;
        public IConnection Connection;

        /// <summary>
        ///     Process packet and connection
        /// </summary>
        /// <param name="connection">this connection</param>
        /// <param name="message">receivet LBMessage</param>
        public abstract void Process(IConnection connection, IScsMessage message);

        /// <summary>
        ///     Read packet datas
        /// </summary>
        public abstract void Read();

        /// <summary>
        ///     Called, when datas was readed
        /// </summary>
        public virtual void Process()
        {
        }

        /// <summary>
        ///     Read integer
        /// </summary>
        /// <returns></returns>
        protected int ReadD()
        {
            try
            {
                return Reader.ReadInt32();
            }
            catch (Exception)
            {
                Log.Warn("Missing D for: {0}", GetType());
            }
            return 0;
        }

        /// <summary>
        ///     Read byte
        /// </summary>
        /// <returns></returns>
        protected int ReadC()
        {
            try
            {
                return Reader.ReadByte() & 0xFF;
            }
            catch (Exception)
            {
                Log.Warn("Missing C for: {0}", GetType());
            }
            return 0;
        }

        /// <summary>
        ///     Read 2-byte integer number.
        /// </summary>
        /// <returns>Short.</returns>
        protected short ReadH()
        {
            try
            {
                return Reader.ReadInt16();
            }
            catch (Exception)
            {
                Log.Warn("Missing H for: {0}", GetType());
            }
            return 0;
        }

        /// <summary>
        ///     Read double float
        /// </summary>
        /// <returns></returns>
        protected double ReadDf()
        {
            try
            {
                return Reader.ReadDouble();
            }
            catch (Exception)
            {
                Log.Warn("Missing DF for: {0}", GetType());
            }
            return 0;
        }

        /// <summary>
        ///     Read float
        /// </summary>
        /// <returns></returns>
        protected float ReadF()
        {
            try
            {
                return Reader.ReadSingle();
            }
            catch (Exception)
            {
                Log.Warn("Missing F for: {0}", GetType());
            }
            return 0;
        }

        /// <summary>
        ///     Read long
        /// </summary>
        /// <returns></returns>
        protected long ReadQ()
        {
            try
            {
                return Reader.ReadInt64();
            }
            catch (Exception)
            {
                Log.Warn("Missing Q for: {0}", GetType());
            }
            return 0;
        }

        /// <summary>
        ///     Read long
        /// </summary>
        /// <returns></returns>
        protected ulong ReadQu()
        {
            try
            {
                return Reader.ReadUInt64();
            }
            catch (Exception)
            {
                Log.Warn("Missing Q for: {0}", GetType());
            }
            return 0;
        }

        /// <summary>
        ///     Read unicode string
        /// </summary>
        /// <returns></returns>
        protected String ReadS()
        {
            Encoding encoding = Encoding.Unicode;
            String result = "";
            try
            {
                short ch;
                while ((ch = Reader.ReadInt16()) != 0)
                    result += encoding.GetString(BitConverter.GetBytes(ch));
            }
            catch (Exception)
            {
                Log.Warn("Missing S for: {0}", GetType());
            }
            return result;
        }

        /// <summary>
        ///     Read unicode string with fixed length padded bytes
        /// </summary>
        /// <returns></returns>
        protected String ReadS(int padding = 0)
        {
            Encoding encoding = Encoding.Unicode;
            String result = "";
            try
            {
                short ch;
                while ((ch = Reader.ReadInt16()) != 0)
                    result += encoding.GetString(BitConverter.GetBytes(ch));

                if (padding > 0)
                {
                    for (int x = (result.Length*2); x < padding; x++)
                    {
                        byte y = Reader.ReadByte();
                    }
                }
            }
            catch (Exception)
            {
                Log.Warn("Missing Sp for: {0}", GetType());
            }
            return result;
        }

        /// <summary>
        ///     Read Unicode char/string
        /// </summary>
        /// <param name="length">padding</param>
        /// <returns></returns>
        protected String ReadSs(int padding = 0)
        {
            Encoding encoding = Encoding.Unicode;
            String result = "";
            try
            {
                char cha;

                while ((cha = Reader.ReadChar()) != 0)
                    result += encoding.GetString(BitConverter.GetBytes(cha));

                if (padding > 0)
                {
                    for (int x = result.Length; x < padding; x++)
                    {
                        byte y = Reader.ReadByte();
                    }
                }
            }
            catch (Exception)
            {
                Log.Warn("Missing Ssp for: {0}", GetType());
            }
            return result;
        }

        /// <summary>
        ///     Read byte array
        /// </summary>
        /// <param name="length">array len</param>
        /// <returns></returns>
        protected byte[] ReadB(int length)
        {
            byte[] result = new byte[length];
            try
            {
                Reader.Read(result, 0, length);
            }
            catch (Exception)
            {
                Log.Warn("Missing byte[] for: {0}", GetType());
            }
            return result;
        }

        protected byte[] ReadToEnd()
        {
            int len = (int) (Reader.BaseStream.Length - Reader.BaseStream.Position);
            return ReadB(len);
        }
    }
}