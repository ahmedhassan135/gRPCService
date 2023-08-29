using Grpc.Core;
using GrpcService.ServerService.Common;
using PrimeServerService.Generated;
using GrpcServer.Service.RecordManagers.Datastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcServer.Service.Core
{
    internal class ServiceImpl : ServerService.ServerServiceBase
    {
        //_primeNumbersList is a dictionary that stores, upon initialization of server, all prime numbers between 1 and 1000
        //Upon a request the prime number will be searched for in this dictionary, and if it does not exist,
        //its not a prime number. This method will have an initial cost on server startup but upon request it will make determining
        // and returning of prime number an O(1) complexity operation.
        IDictionary<long, bool> _primeNumbersList = new Dictionary<long, bool>();
        
        PrimeNumberManager _primeNumberManager;
        Leaderboard<KeyValuePair<long, long>> _priorityQueue;
        private readonly object leaderboardLockObj = new object();


        internal ServiceImpl(PrimeNumberManager primeNumberManager, Leaderboard<KeyValuePair<long, long>> priorityQueue)
        {
            long rangeOfNumbers = 1000;                 //Make this configurable, optionally pass a config in the constructor
            PopulatePrimeNumbersList(rangeOfNumbers);
            _primeNumberManager = primeNumberManager;
            _priorityQueue = priorityQueue;
        }

        public override Task<ServerResponse> ReceiveRequest(ServerRequest request, ServerCallContext context)
        {
            ServerResponse response = new ServerResponse() { IsPrime = CheckIfPrime(request.Number) };

            if (response.IsPrime)
            {
                _primeNumberManager.AddOrUpdatePrimeNumber(request.Number);

                CheckAndInsertIntoLeaderboard(request.Number, _primeNumberManager.GetPrimeNumberEncounteredAmount(request.Number));
            }

            //Upon a successful processing of message, the counter is incremented
            Counter.Instance.IncrementCount();

            return Task.FromResult(response);
        }

        private void CheckAndInsertIntoLeaderboard(long number, long occurence)
        {
            //This process should be done in a seperate thread so as to not delay the flow or requests. (look into this)
            lock (leaderboardLockObj)
            {
                _priorityQueue.Enqueue(new KeyValuePair<long, long>(number, occurence));
                if (_priorityQueue.Count > 10)
                {
                    _priorityQueue.Dequeue();
                } 
            }
        }

        private bool CheckIfPrime(long number)
        {
            return _primeNumbersList.ContainsKey(number);
        }

        private void PopulatePrimeNumbersList(long rangeOfNumbers)
        {
            for (long i = 0; i < rangeOfNumbers; i++)
            {
                if (!_primeNumbersList.ContainsKey(i) && ServiceUtil.IsPrimeNumber(i))
                {
                    _primeNumbersList.Add(i, true);
                }
            }
        }
    }
}
