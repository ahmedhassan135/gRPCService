using System;
using GrpcServer.Service.Core;
using GrpcServer.Service.Metrics;
using GrpcServer.Service.RecordManagers.Datastructures;
using Serilog;
using Serilog.Core;

namespace GrpcService.ServerService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                InitializeLogger();         //Initially the logger is hardcoded to minimum level error. This should be configurable

                Log.Debug("Initializing Server");

                PrimeNumberManager primeNumberManager = new PrimeNumberManager();
                Leaderboard<KeyValuePair<long, long>> leaderboard = new Leaderboard<KeyValuePair<long, long>>(new KeyValuePairComparer());

                PrimeNumberServer server = new PrimeNumberServer(primeNumberManager, leaderboard);

                Log.Debug("Server Initialized Successfully");

                DisplayCounters displayCounters = new DisplayCounters();

                Log.Debug("Attempting to start server");
                displayCounters.Start(leaderboard);
                server.Start();

                Log.Debug("Server started successfully");


                Console.WriteLine("Server is listening");
                Console.WriteLine("Press any key to stop");

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Log.Error($"A fatal exception occured, exiting program {ex}");
            }

        }

        private static void InitializeLogger()
        {
            Log.Logger = new LoggerConfiguration()
                           .WriteTo.Console()
                           .WriteTo.File("all-.logs",
                                   rollingInterval: RollingInterval.Day)
                           .MinimumLevel.Error()                            //This should be read from a config file upon server start
                           .CreateLogger();
        }
    }
}