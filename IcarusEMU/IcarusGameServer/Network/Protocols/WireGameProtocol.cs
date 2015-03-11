// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Communication.Protocols;

namespace IcarusGameServer.Network.Protocols
{
    internal class WireGameProtocol : IScsWireProtocol
    {
        protected MemoryStream Stream = new MemoryStream();

        public byte[] GetBytes(IScsMessage message)
        {
            return ((GameMessage) message).Data;
        }

        public IEnumerable<IScsMessage> CreateMessages(byte[] receivedBytes)
        {
            Stream.Write(receivedBytes, 0, receivedBytes.Length);
            var messages = new List<IScsMessage>();
            while (ReadMessage(messages))
            {
            }

            return messages;
        }

        public void Reset()
        {
        }

        private bool ReadMessage(List<IScsMessage> messages)
        {
            Stream.Position = 0;
            if (Stream.Position == Stream.Length || Stream.Length < 2)
                return false;

            var lenArray = new byte[2];

            Stream.Read(lenArray, 0, 2);

            ushort len = BitConverter.ToUInt16(lenArray, 0);

            if (len > Stream.Length)
            {
                Stream.Position = Stream.Length;
                return false;
            }

            var datas = new byte[len - 2];

            Stream.Read(datas, 0, datas.Length);

            if (datas[2] == 1 || datas[2] == 2) //if message is crypted, decrypt
                XorDecr(ref datas);

            messages.Add(new GameMessage
            {
                OpCode = BitConverter.ToUInt16(datas, 0),
                IsCrypted = datas[2],
                Data = datas.Skip(4).ToArray(),
                FullLen = len
            });
            TrimStream();

            return true;
        }

        private void TrimStream()
        {
            if (Stream.Position == Stream.Length)
            {
                Stream.Dispose();
                Stream = new MemoryStream();
                return;
            }

            byte[] remaining = new byte[Stream.Length - Stream.Position];
            Stream.Read(remaining, 0, remaining.Length);
            Stream = new MemoryStream();
            Stream.Write(remaining, 0, remaining.Length);
        }

        public static void XorDecr(ref byte[] data)
        {
            int step = 0;
            byte[] xorKey = {0xc1, 0xa1, 0xb2, 0xc4, 0x4b, 0x3f, 0x1b, 0x41};

            for (int i = 4; i < data.Length; i++)
            {
                data[i] ^= xorKey[step];
                step++;

                if (step == xorKey.Length)
                    step = 0;
            }
        }
    }

    internal class GameMessage : ScsMessage
    {
        public ushort FullLen;
        public ushort OpCode;
        public byte IsCrypted;
        public byte[] Data;
    }
}