using Client.Core;
using Client.SocketService;
using System.Diagnostics;

namespace GrpcService.Consumer
{
    class Program
    {
        /*
         * This is a sample test application. It is meant for testing and debugging purposes. 
         * It has a reference attached to the client.dll which contains the methods used for communicating
         * with the server. Note: This application due to the client.dll attached has a dependency for 
         * the libraries, Google.Proto, Grpc.core and Grpc.tools
         */
        static void Main(string[] args)
        {
            bool showRTT = false;
            var properties = new ConnectionProperties(new string[] { "localhost" }, 12000, showRTT);

            IClient client = ClientManager.GetClient(properties);  //Not displaying RTT by default since I/O is a costly operation. But setting it to true will display each time taken 

            Parallel.For(0, 10000, (x) => { var isPrime = client.SendMessage(new RequestMessage() { Id = x, Number = x % 1000 }).Result; });


            Console.WriteLine("Application has ended... press any key to exit");
            Console.ReadKey();
        }

        
    }
}