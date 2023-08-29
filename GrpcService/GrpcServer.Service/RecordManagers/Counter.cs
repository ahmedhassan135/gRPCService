using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcServer.Service.Core
{
    internal class Counter
    {
        private static volatile Counter instance;
        private static object syncRoot = new object();
        private long messageCount;

        private Counter() { }

        public long MessageCount { get => messageCount; }

        public static Counter Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Counter();
                    }
                }

                return instance;
            }
        }

        public void IncrementCount()
        {
            Interlocked.Increment(ref messageCount);
        }
    }
}
