using System;
using System.Collections;
using System.Collections.Generic;

namespace NT.DataStructures
{
    public class StaticStack<T> : IEnumerable<T>
    {
        internal T[] array;
        internal int size;
        private const int DefaultCapacity = 4;

        public StaticStack()
        {
            this.array = new T[0];
            this.size = 0;
        }

        public StaticStack(int capacity)
        {
            this.array = new T[capacity];
            this.size = 0;
        }

        public StaticStack(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }

            ICollection<T> col = collection as ICollection<T>;

            if (col != null)
            {
                int count = col.Count;
                this.array = new T[count];
                col.CopyTo(this.array, 0);
                this.size = count;
            }
        }

        public int Count
        {
            get
            {
                return this.size;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Clear()
        {
            Array.Clear(this.array, 0, this.size);
            this.size = 0;
        }

        public bool Contains(T item)
        {
            int count = this.size;

            while (count-- > 0)
            {
                if (((Object)item) == null)
                {
                    if (((Object)this.array[count]) == null)
                    {
                        return true;
                    }
                }
                else if (this.array[count] != null && this.array[count].Equals(item))
                {
                    return true;
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

            if (arrayIndex < 0 || arrayIndex > array.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (array.Length - arrayIndex < this.size)
            {
                throw new ArgumentException("Invalid length!");
            }

            Array.Copy(this.array, 0, array, arrayIndex, this.size);
            Array.Reverse(array, arrayIndex, this.size);
        }

        public T Peek()
        {
            if (this.size == 0)
            {
                throw new InvalidOperationException();
            }

            return this.array[this.size - 1];
        }

        public T Pop()
        {
            if (this.size == 0)
            {
                throw new InvalidOperationException();
            }

            T item = this.array[--this.size];
            this.array[this.size] = default(T);

            return item;
        }

        public void Push(T item)
        {
            if (this.size == this.array.Length)
            {
                if (this.array.Length == 0)
                {
                    T[] newArr = new T[DefaultCapacity];
                    Array.Copy(this.array, 0, newArr, 0 , this.size);
                    this.array = newArr;
                }
                else
                {
                    T[] newArr = new T[2 * this.array.Length];
                    Array.Copy(this.array, 0, newArr, 0, this.size);
                    this.array = newArr;
                }
            }

            this.array[this.size++] = item;
        }

        public T[] ToArray()
        {
            T[] objArray = new T[this.size];
            int i = 0;
            while (i < this.size)
            {
                objArray[i] = this.array[this.size - i - 1];
                i++;
            }

            return objArray;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new StaticStackEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
