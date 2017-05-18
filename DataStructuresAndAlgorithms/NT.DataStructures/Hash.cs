using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NT.DataStructures
{
    public class Hash<T> : ICollection<T>
    {
        private const int DefaultInitialSize = 17;
        internal List<HashSlot<T>>[] buckets;
        private int count;
        private int lenght;

        public Hash()
        {
            Initialize(DefaultInitialSize);
        }

        public Hash(ICollection<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }

            int size = NextPrimeNumber(collection.Count * 2);
            Initialize(size);
            this.UnionWith(collection);
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        private void Initialize(int size)
        {
            this.buckets = new List<HashSlot<T>>[size];
            this.count = 0;
            this.lenght = size;
        }

        public void UnionWith(IEnumerable<T> collection)
        {
            foreach (T value in collection)
            {
                AddIfNotPresent(value);
            }
        }

        private bool AddIfNotPresent(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            if (this.count * 2 > this.buckets.Length)
            {
                IncreaseCapacity();
            }

            int hashCode = ReHash(value.GetHashCode()) & 0x7FFFFFFF;
            int bucket = hashCode % this.buckets.Length;
            var currentBucket = this.buckets[bucket];
            if (currentBucket == null)
            {
                currentBucket = new List<HashSlot<T>>();
            }
            else
            {
                for (int i = 0; i < currentBucket.Count; i++)
                {
                    var current = currentBucket[i];
                    if (current.HashCode == hashCode && current.Value.Equals(value))
                    {
                        return false;
                    }
                }
            }

            HashSlot<T> itemToAdd = new HashSlot<T>();
            itemToAdd.HashCode = hashCode;
            itemToAdd.Value = value;
            currentBucket.Add(itemToAdd);
            this.buckets[bucket] = currentBucket;
            this.count++;

            return true;
        }

        private void IncreaseCapacity()
        {
            int newSize = NextPrimeNumber(this.buckets.Length * 2);
            List<HashSlot<T>>[] newBuckets = new List<HashSlot<T>>[newSize];
            for (int i = 0; i < this.lenght; i++)
            {
                if (this.buckets[i] != null)
                {
                    var newHashCode = this.buckets[i].First().HashCode;
                    var newBucket = newHashCode % newBuckets.Length;
                    newBuckets[newBucket] = this.buckets[i];
                }
            }

            this.buckets = newBuckets;
            this.lenght = newBuckets.Length;
        }

        public void Add(T item)
        {
            AddIfNotPresent(item);
        }

        public void Clear()
        {
            Array.Clear(this.buckets, 0, this.lenght);
            this.lenght = 0;
            this.count = 0;
        }

        public bool Contains(T value)
        {
            return this.ContainsAction(value, (currentBucket, i) => { });
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException();
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (this.count < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (arrayIndex > array.Length || this.count > array.Length - arrayIndex)
            {
                throw new ArgumentOutOfRangeException();
            }
            
            int current = 0;
            int numCopied = 0;
            for (int i = 0; i < this.lenght && numCopied < this.count; i++)
            {
                if (this.buckets[i] != null)
                {
                    var tempArray = this.buckets[i].ToArray();
                    for (int k = 0; k < tempArray.Length; k++)
                    {
                        array[current + arrayIndex] = tempArray[k].Value;
                        current++;
                        numCopied++;
                    }
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new HashEnumerator<T>(this);
        }

        public bool Remove(T value)
        {
            return ContainsAction(value, (currentBucket, i) =>
            {
                currentBucket.Remove(currentBucket[i]);
                this.count--;
            });
        }

        private bool ContainsAction(T value, Action<List<HashSlot<T>>, int> action)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            int hashCode = ReHash(value.GetHashCode()) & 0x7FFFFFFF;
            int bucket = hashCode % this.buckets.Length;
            var currentBucket = this.buckets[bucket];
            if (currentBucket != null)
            {
                for (int i = 0; i < currentBucket.Count; i++)
                {
                    var current = currentBucket[i];
                    if (current.HashCode == hashCode && current.Value.Equals(value))
                    {
                        action(currentBucket, i);
                        return true;
                    }
                }
            }

            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private int NextPrimeNumber(int number)
        {
            int[] primes = {
            3, 7, 11, 17, 23, 29, 37, 47, 59, 71, 89, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919,
            1103, 1327, 1597, 1931, 2333, 2801, 3371, 4049, 4861, 5839, 7013, 8419, 10103, 12143, 14591,
            17519, 21023, 25229, 30293, 36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363, 156437,
            187751, 225307, 270371, 324449, 389357, 467237, 560689, 672827, 807403, 968897, 1162687, 1395263,
            1674319, 2009191, 2411033, 2893249, 3471899, 4166287, 4999559, 5999471, 7199369};

            if (number < primes.Last())
            {
                for (int i = 0; i < primes.Length; i++)
                {
                    if (primes[i] > number)
                    {
                        return number = primes[i];
                    }
                }
            }
            else
            {
                do
                {
                    number++;
                } while (!isPrime(number));
            }

            return number;
        }

        private bool isPrime(int number)
        {
            double sqRoot = Math.Sqrt(number);
            for (int i = number; i <= sqRoot; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        private int ReHash(int source)
        {
            unchecked
            {
                ulong c = 0xDEADBEEFDEADBEEF + (ulong)source;
                ulong d = 0xE2ADBEEFDEADBEEF ^ c;
                ulong a = d += c = c << 15 | c >> -15;
                ulong b = a += d = d << 52 | d >> -52;
                c ^= b += a = a << 26 | a >> -26;
                d ^= c += b = b << 51 | b >> -51;
                a ^= d += c = c << 28 | c >> -28;
                b ^= a += d = d << 9 | d >> -9;
                c ^= b += a = a << 47 | a >> -47;
                d ^= c += b << 54 | b >> -54;
                a ^= d += c << 32 | c >> 32;
                a += d << 25 | d >> -25;
                return (int)(a >> 1);
            }
        }
    }
}
