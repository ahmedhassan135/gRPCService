using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcServer.Service.Core
{
    internal class PrimeNumberManager
    {
        private readonly ConcurrentDictionary<long, long> _primeNumbersEncountered;
        private List<KeyValuePair<long, long>> _leaderBoard;
        
        internal PrimeNumberManager() 
        {
            _primeNumbersEncountered = new ConcurrentDictionary<long, long>();
            _leaderBoard = new List<KeyValuePair<long, long>>();
        }

        internal void AddOrUpdatePrimeNumber(long number)
        {
            if (!_primeNumbersEncountered.ContainsKey(number))
            {
                _primeNumbersEncountered.TryAdd(number, 1);
            }
            else
            {
                _primeNumbersEncountered[number] = _primeNumbersEncountered[number] + 1;
            }


        }

        internal long GetPrimeNumberEncounteredAmount(long number) 
        {
            return _primeNumbersEncountered[number];
        }

        internal ConcurrentDictionary<long, long> GetPrimeNumbersEncountered()      //TODO: remove this it is only for debugging
        {
            return _primeNumbersEncountered;
        }

       


    }
}
