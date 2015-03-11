// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace IcarusCommons.Utils
{
    public class TaskProcessor
    {
        public static List<GameTask> Tasks = new List<GameTask>();
        private static readonly object LockTasks = new object();
        private static Thread _mainTaskThread;

        ~TaskProcessor()
        {
            Tasks.Clear();
            Tasks = null;
            if (_mainTaskThread.IsAlive)
                _mainTaskThread.Abort();
        }

        public static void Init()
        {
            _mainTaskThread = new Thread(() =>
            {
                while (Instance.IsWorking)
                {
                    lock (LockTasks)
                        foreach (GameTask task in Tasks)
                        {
                            if (task.WorkInterval == 0)
                            {
                                task.Task();
                                Tasks.Remove(task);
                            }
                            else
                            {
                                if (Extensions.RoundedUtc - task.LastWorkTime > task.WorkInterval)
                                {
                                    task.Task();
                                    task.LastWorkTime = Extensions.RoundedUtc;
                                }
                            }
                        }
                    Thread.Sleep(1000);
                }
            });

            _mainTaskThread.Start();
        }

        public static void RemoveTask(Action action)
        {
            lock (LockTasks)
            {
                GameTask task = Tasks.FirstOrDefault(p => p.Task == action);

                if (task != null)
                    Tasks.Remove(task);
            }
        }

        public static void AddTask(Action action, int interval = 0)
        {
            lock (LockTasks)
            {
                if (Tasks.FirstOrDefault(p => p.Task == action) != null)
                    return;
                Tasks.Add(new GameTask(interval, action));
            }
        }
    }

    public class GameTask
    {
        public Action Task { get; private set; }

        public int LastWorkTime { get; set; }

        public int WorkInterval { get; private set; }

        public GameTask(int interval, Action action)
        {
            LastWorkTime = Extensions.RoundedUtc;
            WorkInterval = interval;
            Task = action;
        }
    }
}