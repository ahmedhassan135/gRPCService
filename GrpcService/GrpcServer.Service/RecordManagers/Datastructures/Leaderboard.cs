using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcServer.Service.RecordManagers.Datastructures
{
    internal class Leaderboard<T>
    {
        private SortedSet<KeyValuePair<long, long>> set;

        internal SortedSet<KeyValuePair<long, long>> TopNumbers { get => set; }

        internal int Count => set.Count;

        internal Leaderboard(IComparer<KeyValuePair<long, long>> comparer)
        {
            set = new SortedSet<KeyValuePair<long, long>>(comparer);
        }

        internal void Enqueue(KeyValuePair<long, long> item)
        {
            if(set.Contains(item))
                set.Remove(item);

            set.Add(item);
        }

        internal KeyValuePair<long, long> Dequeue()
        {
            var first = set.Min;
            set.Remove(first);
            return first;
        }

        internal IEnumerator<KeyValuePair<long, long>> GetEnumerator()
        {
            return set.GetEnumerator();
        }

    }

    public class KeyValuePairComparer : IComparer<KeyValuePair<long, long>>
    {
        public int Compare(KeyValuePair<long, long> x, KeyValuePair<long, long> y)
        {
            return y.Key.CompareTo(x.Key);
        }
    }
}
