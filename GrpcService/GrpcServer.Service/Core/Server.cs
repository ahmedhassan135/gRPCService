using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrpcServer.Service.RecordManagers.Datastructures;

namespace GrpcServer.Service.Core
{
    internal class PrimeNumberServer
    {
        private readonly Server _server;

        internal PrimeNumberServer(PrimeNumberManager primeNumberManager, Leaderboard<KeyValuePair<long, long>> priorityQueue)
        {
            _server = new Server()
            {
                Services = { PrimeServerService.Generated.ServerService.BindService(new ServiceImpl(primeNumberManager, priorityQueue)) },
                Ports = { new ServerPort("localhost", 12000, ServerCredentials.Insecure) }              //TODO: Make ip and port configurable (Read from a config file)
            };
        }

        internal void Start()
        {
            _server.Start();
        }

        internal async Task ShutDownAsync()
        {
            await _server.ShutdownAsync();
        }
    }
}
