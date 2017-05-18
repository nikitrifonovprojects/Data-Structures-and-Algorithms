using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NT.DataStructures
{
    public class HashTable<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
    {
        private const int DefaultInitialValue = 0;
        private int[] buckets;
        internal HashTableEntry[] entries;
        private int count;
        private int freeList;
        private int freeCount;

        public HashTable()
        {
            Initialize(DefaultInitialValue);
        }

        public HashTable(IDictionary<TKey, TValue> dictionary)
            : this()
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException();
            }

            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
            {
                Add(pair.Key, pair.Value);
            }
        }

        public int Count
        {
            get
            {
                return this.count - this.freeCount;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                return this.entries.Where(x => x.HashCode >= 0).Select(x => x.Key).ToList();
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return this.entries.Where(x => x.HashCode >= 0).Select(x => x.Value).ToList();
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                int i = FindEntry(key);
                if (i >= 0)
                {
                    return this.entries[i].Value;
                }
                else
                {
                    throw new ArgumentException("Key not found");
                }
            }
            set
            {
                Insert(key, value, false);
            }
        }

        private void Initialize(int capacity)
        {
            int size = NextPrimeNumber(capacity);
            this.buckets = new int[size];
            for (int i = 0; i < this.buckets.Length; i++)
            {
                this.buckets[i] = -1;
            }

            this.entries = new HashTableEntry[size];
            this.count = 0;
            this.freeList = -1;
        }

        public void Add(TKey key, TValue value)
        {
            Insert(key, value, true);
        }

        private void Insert(TKey key, TValue value, bool add)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
            }

            if (this.buckets == null)
            {
                Initialize(DefaultInitialValue);
            }

            int hashCode = ReHash(key.GetHashCode()) & 0x7FFFFFFF;
            int targetBucket = hashCode % this.buckets.Length;
            for (int i = this.buckets[targetBucket]; i >= 0; i = this.entries[i].Next)
            {
                if (this.entries[i].HashCode == hashCode && this.entries[i].Key.Equals(key))
                {
                    if (add)
                    {
                        throw new ArgumentException("Cannot add duplicate!");
                    }

                    this.entries[i].Value = value;
                    return;
                }
            }

            int index;
            if (this.freeCount > 0)
            {
                index = this.freeList;
                this.freeList = this.entries[index].Next;
                this.freeCount--;
            }
            else
            {
                if (this.count == this.entries.Length)
                {
                    Resize();
                    targetBucket = hashCode % this.buckets.Length;
                }

                index = this.count;
                this.count++;
            }

            this.entries[index].HashCode = hashCode;
            this.entries[index].Next = this.buckets[targetBucket];
            this.entries[index].Key = key;
            this.entries[index].Value = value;
            this.buckets[targetBucket] = index;
        }

        private void Resize()
        {
            Resize(NextPrimeNumber(this.count * 2), false);
        }

        private void Resize(int newSize, bool ForceNewHashCodes)
        {
            if (newSize < this.entries.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            int[] newBuckets = new int[newSize];
            for (int i = 0; i < newBuckets.Length; i++)
            {
                newBuckets[i] = -1;
            }

            HashTableEntry[] newEntries = new HashTableEntry[newSize];
            Array.Copy(this.entries, 0, newEntries, 0, this.count);
            if (ForceNewHashCodes)
            {
                for (int i = 0; i < this.count; i++)
                {
                    if (newEntries[i].HashCode != -1)
                    {
                        newEntries[i].HashCode = ReHash(newEntries[i].Key.GetHashCode()) & 0x7FFFFFFF;
                    }
                }
            }

            for (int i = 0; i < this.count; i++)
            {
                if (newEntries[i].HashCode >= 0)
                {
                    int bucket = newEntries[i].HashCode % newSize;
                    newEntries[i].Next = newBuckets[bucket];
                    newBuckets[bucket] = i;
                }
            }

            this.buckets = newBuckets;
            this.entries = newEntries;
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public bool ContainsKey(TKey key)
        {
            if (FindEntry(key) >= 0)
            {
                return true;
            }

            return false;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            int i = FindEntry(item.Key);
            if (i >= 0 && this.entries[i].Value.Equals(item.Value))
            {
                return true;
            }

            return false;
        }

        public bool ContainsValue(TValue value)
        {
            if (value == null)
            {
                for (int i = 0; i < this.count; i++)
                {
                    if (this.entries[i].HashCode >= 0 && this.entries[i].Value == null)
                    {
                        return true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.count; i++)
                {
                    if (this.entries[i].HashCode >= 0 && this.entries[i].Value.Equals(value))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException();
            }

            if (arrayIndex < 0 || arrayIndex > array.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (array.Length - arrayIndex < this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            int count = this.count;
            HashTableEntry[] entries = this.entries;
            for (int i = 0; i < count; i++)
            {
                if (entries[i].HashCode >= 0)
                {
                    array[arrayIndex++] = new KeyValuePair<TKey, TValue>(entries[i].Key, entries[i].Value);
                }
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            int i = FindEntry(item.Key);
            if (i >= 0 && this.entries[i].Value.Equals(item.Value))
            {
                Remove(item.Key);
                return true;
            }

            return false;
        }

        public bool Remove(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
            }

            if (this.buckets != null)
            {
                int hashCode = ReHash(key.GetHashCode()) & 0x7FFFFFFF;
                int bucket = hashCode % this.buckets.Length;
                int last = -1;
                for (int i = this.buckets[bucket]; i >= 0; last = i, i = this.entries[i].Next)
                {
                    if (this.entries[i].HashCode == hashCode && this.entries[i].Key.Equals(key))
                    {
                        if (last < 0)
                        {
                            this.buckets[bucket] = this.entries[i].Next;
                        }
                        else
                        {
                            this.entries[last].Next = this.entries[i].Next;
                        }

                        this.entries[i].HashCode = -1;
                        this.entries[i].Next = this.freeList;
                        this.entries[i].Key = default(TKey);
                        this.entries[i].Value = default(TValue);
                        this.freeList = i;
                        this.freeCount++;

                        return true;
                    }
                }
            }

            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            int i = FindEntry(key);
            if (i >= 0)
            {
                value = this.entries[i].Value;
                return true;
            }

            value = default(TValue);
            return false;
        }

        private int FindEntry(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
            }

            if (this.buckets != null)
            {
                int hashCode = ReHash(key.GetHashCode()) & 0x7FFFFFFF;
                for (int i = this.buckets[hashCode % this.buckets.Length]; i >= 0; i = this.entries[i].Next)
                {
                    if (this.entries[i].HashCode == hashCode && this.entries[i].Key.Equals(key))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public void Clear()
        {
            if (this.count > 0)
            {
                for (int i = 0; i < this.buckets.Length; i++)
                {
                    this.buckets[i] = -1;
                    Array.Clear(this.entries, 0, this.count);
                    this.freeList = -1;
                    this.count = 0;
                    this.freeCount = 0;
                }
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new HashTableEnumerator(this);
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
                        return primes[i];
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

        public struct HashTableEntry
        {
            internal int HashCode { get; set; }

            internal int Next { get; set; }

            internal TKey Key { get; set; }

            internal TValue Value { get; set; }
        }

        public class HashTableEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            private HashTable<TKey, TValue> hashTable;
            private int index;
            private KeyValuePair<TKey, TValue> currentValue;

            public HashTableEnumerator(HashTable<TKey, TValue> hashTable)
            {
                this.hashTable = hashTable;
                this.index = 0;
                this.currentValue = new KeyValuePair<TKey, TValue>();
            }

            public KeyValuePair<TKey, TValue> Current
            {
                get
                {
                    return this.currentValue;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    if (this.index == 0 || (this.index == this.hashTable.count + 1))
                    {
                        throw new InvalidOperationException();
                    }

                    return new KeyValuePair<TKey, TValue>(this.currentValue.Key, this.currentValue.Value);
                }
            }

            public void Dispose()
            {
                
            }

            public bool MoveNext()
            {
                while ((uint)this.index < (uint)this.hashTable.count)
                {
                    if (this.hashTable.entries[this.index].HashCode >= 0)
                    {
                        this.currentValue = new KeyValuePair<TKey, TValue>(this.hashTable.entries[this.index].Key, this.hashTable.entries[this.index].Value);
                        this.index++;
                        return true;
                    }

                    this.index++;
                }

                this.index = this.hashTable.count + 1;
                this.currentValue = new KeyValuePair<TKey, TValue>();

                return false;
            }

            public void Reset()
            {
                this.index = 0;
                this.currentValue = new KeyValuePair<TKey, TValue>();
            }
        }
    }
}
