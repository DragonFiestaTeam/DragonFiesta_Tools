using System;
using System.Collections.Concurrent;
using System.Threading;

namespace DragonDataSniffer
{
    [TunnelModule(InitializationStage.DataStore)]
    public sealed class Worker
    {
        public static Worker Instance { get; private set; }
        public bool IsRunning { get; set; }

        private ConcurrentQueue<Action> callbacks = new ConcurrentQueue<Action>();
        private Thread main;
        private int sleep = 1;

        public Worker()
        {
            main = new Thread(Work);
            IsRunning = true;
            main.Start();
        }

        [InitializerMethod]
        public static bool Load()
        {
            try
            {
                Instance = new Worker()
                {
                    sleep = 1
                };
                return true;
            }
            catch { return false; }
        }

        public void AddCallback(Action pCallback)
        {
            callbacks.Enqueue(pCallback);
        }

        private void Work()
        {
            while (IsRunning)
            {
                while (callbacks.TryDequeue(out Action action))
                {
                    action();
                }
                Thread.Sleep(sleep);
            }
            Log.WriteLine(LogLevel.Info, "Server stopped handling callbacks.");
        }
    }
}