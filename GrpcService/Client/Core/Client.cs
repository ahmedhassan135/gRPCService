using Client.Common;
using Client.SocketService;
using Grpc.Core;
using System.Diagnostics;

namespace Client.Core
{
    public class Client : IClient
    {
        private string[] serverIps;
        private int port;
        private bool _showRTT;

        public Client(string[] serverIps, int port, bool showRTT)
        {
            this.serverIps = serverIps;
            this.port = port;
            _showRTT = showRTT;
        }

        public async Task<bool> SendMessage(RequestMessage message)
        {
            string hostname = ClientUtil.GetServerToRequest(message.Id, serverIps);

            Stopwatch stopwatch = Stopwatch.StartNew();

            var isPrime = await MessageService.SendMessageInternal(message, hostname, port);  

            stopwatch.Stop();

            if(_showRTT)
                Console.WriteLine($"Request for item {message.Id}: Took {stopwatch.ElapsedMilliseconds} to complete");


            return isPrime;
        }
    }
}