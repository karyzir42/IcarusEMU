// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.Threading;
using IcarusCommons.Utils;
using NLog;

namespace IcarusCommons
{
    public abstract class Instance
    {
        public static Instance Global;
        public static bool IsWorking;
        protected static Thread GeneralThread { get; set; }

        protected Instance()
        {
            Initilize();
        }

        static Instance()
        {
            LogManager.Configuration = Extensions.NLogDefaultConfiguration;
            GeneralThread = new Thread(() =>
            {
                while (true)
                {
                    if (!IsWorking)
                        break;

                    Thread.Sleep(1);
                }

                if (GeneralThread.IsAlive)
                    GeneralThread.Abort();
            });
        }

        public abstract void Initilize();

        public virtual void Start()
        {
            if (IsWorking)
                GeneralThread.Start();
        }
    }
}