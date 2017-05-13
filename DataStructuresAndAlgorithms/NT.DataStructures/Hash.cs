using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT.DataStructures
{
    public class Hash<T> : ICollection<T>
    {
        private const int DefaultInitialSize = 17;
        private List<HashSlot<T>>[] buckets;
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
            if (this.count * 3 > this.buckets.Length)
            {
                IncreaseCapacity();
            }

            int hashCode = value.GetHashCode();
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

            int currentPlace = 0;
            for (int i = 0; i < this.lenght; i++)
            {
                if (this.buckets[i] != null)
                {

                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool Remove(T value)
        {
            int hashCode = value.GetHashCode();
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
    }
}
