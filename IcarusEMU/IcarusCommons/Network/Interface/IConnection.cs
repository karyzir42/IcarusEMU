// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using Hik.Communication.Scs.Server;

namespace IcarusCommons.Network.Interface
{
    public interface IConnection
    {
        IScsServerClient Client { get; set; }
        bool IsValid { get; set; }

        void InitilizeConnection();
        void CloseConnection(bool force);

        void HandleData(byte[] data);
    }
}