using System.Collections;
using System.Collections.Generic;

namespace NT.DataStructures
{
    class HashEnumerator<T> : IEnumerator<T>
    {
        private Hash<T> hash;
        private int index;
        private int innerIndex;
        private int bucketCount;
        private T current;

        public HashEnumerator(Hash<T> hash)
        {
            this.innerIndex = 0;
            this.index = 0;
            this.hash = hash;
            this.bucketCount = this.hash.buckets.Length;
            this.current = default(T);
        }

        public T Current
        {
            get
            {
                return this.current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            while (this.index < this.bucketCount)
            {
                if (this.hash.buckets[this.index] != null)
                {
                    var tempArray = this.hash.buckets[this.index].ToArray();
                    while (tempArray.Length > this.innerIndex)
                    {
                        this.current = tempArray[this.innerIndex].Value;
                        this.innerIndex++;
                        return true;
                    }
                }

                this.innerIndex = 0;
                this.index++;
            }

            this.current = default(T);
            return false;
        }

        public void Reset()
        {
            this.innerIndex = 0;
            this.index = 0;
            this.current = default(T);
        }
    }
}
