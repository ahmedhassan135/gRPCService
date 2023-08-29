using GrpcServer.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcServer.Service.Metrics
{
    internal class DisplayCounters
    {
        internal void Start(RecordManagers.Datastructures.Leaderboard<KeyValuePair<long, long>> leaderboard)
        {
            new Thread(() =>
            {
                while (true)
                {
                    var items = leaderboard.TopNumbers.OrderByDescending(kvp => kvp.Value).ToList();

                    foreach (var item in items)
                    {
                        Console.WriteLine($"{item.Key}:\t\t{item.Value}");
                    }

                    Console.WriteLine("\n\nTOTAL MESSAGE COUNT:\t\t " + Counter.Instance.MessageCount);
                    Thread.Sleep(1000);
                }
            })
            {
                IsBackground = true
            }.Start();
        }

    }
}
