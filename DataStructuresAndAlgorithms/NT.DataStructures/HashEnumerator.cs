using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT.DataStructures
{
    class HashEnumerator<T> : IEnumerator<T>
    {
        private Hash<T> hash;
        private int index;
        private int bucketCount;
        private T current;

        public HashEnumerator(Hash<T> hash)
        {
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
                    for (int k = 0; k < tempArray.Length; k++)
                    {
                        this.current = tempArray[k].value;
                        this.index++;
                        return true;

                    }
                }

                this.index++;
            }

            this.current = default(T);
            return false;
        }

        public void Reset()
        {
            this.index = 0;
            this.current = default(T);
        }
    }
}
