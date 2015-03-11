// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.IO;
using Hik.Communication.Scs.Communication.Messages;
using IcarusCommons.Network;
using IcarusCommons.Network.Interface;
using IcarusCommons.Utils;
using IcarusGameServer.Network.Protocols;

namespace IcarusGameServer.Network.Packets.Processors
{
    public abstract class ARecvPacket : ABinRPacket
    {
        public new Connection Connection
        {
            get { return (Connection) base.Connection; }
            set { base.Connection = value; }
        }

        public override void Process(IConnection connection, IScsMessage message)
        {
            TaskProcessor.AddTask(() => Process(connection, ((GameMessage) message).Data));
        }

        public void Process(IConnection connection, byte[] datas)
        {
            if (connection == null)
                return;

            Connection = (Connection) connection;

            try
            {
                using (Reader = new BinaryReader(new MemoryStream(datas)))
                    Read();

                Process();
            }
            catch (Exception ex)
            {
                Log.WarnException("ARecvPacket", ex);
            }
        }
    }
}