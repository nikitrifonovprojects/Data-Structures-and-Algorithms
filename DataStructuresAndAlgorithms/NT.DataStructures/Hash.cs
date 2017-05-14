using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NT.DataStructures
{
    public class Hash<T> : ICollection<T>
    {
        private const int DefaultInitialSize = 17;
        public List<HashSlot<T>>[] buckets;
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

            int capacity = 0;
            ICollection<T> col = collection as ICollection<T>;
            if (col != null)
            {
                capacity = col.Count;
            }

            int size = NextPrimeNumber(capacity * 2);
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
            if (this.count * 4 > this.buckets.Length)
            {
                IncreaseCapacity();
            }

            int hashCode = value.GetHashCode();
            hashCode = ReHash(hashCode);
            if (hashCode < 0)
            {
                hashCode *= -1;
            }

            int bucket = hashCode % this.buckets.Length;
            var checkIfPresent = this.buckets[bucket];

            if (checkIfPresent == null)
            {
                checkIfPresent = new List<HashSlot<T>>();
            }
            else
            {
                for (int i = 0; i < checkIfPresent.Count; i++)
                {
                    var current = checkIfPresent[i];
                    if (current.hashCode == hashCode && current.value.Equals(value))
                    {
                        return false;
                    }
                }
            }

            HashSlot<T> itemToAdd = new HashSlot<T>();
            itemToAdd.hashCode = hashCode;
            itemToAdd.value = value;
            checkIfPresent.Add(itemToAdd);
            this.buckets[bucket] = checkIfPresent;
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
                    var newHashCode = this.buckets[i].First().hashCode;
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
            int hashCode = value.GetHashCode();
            hashCode = ReHash(hashCode);
            if (hashCode < 0)
            {
                hashCode *= -1;
            }

            int bucket = hashCode % this.buckets.Length;
            var checkIfPresent = this.buckets[bucket];
            if (checkIfPresent != null)
            {
                for (int i = 0; i < checkIfPresent.Count; i++)
                {
                    var current = checkIfPresent[i];
                    if (current.hashCode == hashCode && current.value.Equals(value))
                    {
                        return true;
                    }
                }
            }

            return false;
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

            if (this.count > arrayIndex)
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
                        array[current] = tempArray[k].value;
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
            int hashCode = value.GetHashCode();
            hashCode = ReHash(hashCode);
            if (hashCode < 0)
            {
                hashCode *= -1;
            }

            int bucket = hashCode % this.buckets.Length;
            var checkIfPresent = this.buckets[bucket];
            if (checkIfPresent != null)
            {
                for (int i = 0; i < checkIfPresent.Count; i++)
                {
                    var current = checkIfPresent[i];
                    if (current.hashCode == hashCode && current.value.Equals(value))
                    {
                        checkIfPresent.Remove(checkIfPresent[i]);
                        this.count--;
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
            do
            {
                number++;
            } while (!isPrime(number));

            return number;
        }

        private bool isPrime(int number)
        {
            double sqRoot = Math.Sqrt(number);
            for (int i = 3; i <= sqRoot; i++)
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
                return NextPrimeNumber((int)(a >> 1));
            }
        }
    }
}
