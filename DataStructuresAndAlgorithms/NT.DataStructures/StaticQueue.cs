using System;
using System.Collections;
using System.Collections.Generic;

namespace NT.DataStructures
{
    public class StaticQueue<T> : IEnumerable<T>
    {
        private const int DefaultCapacity = 4;
        private T[] array;
        private int head;
        private int tail;
        internal int size;

        public StaticQueue()
        {
            this.array = new T[0];
        }

        public StaticQueue(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.array = new T[capacity];
            this.head = 0;
            this.tail = 0;
            this.size = 0;
        }

        public StaticQueue(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }

            this.array = new T[DefaultCapacity];
            this.size = 0;

            var enumerator = collection.GetEnumerator();

            while (enumerator.MoveNext())
            {
                Enqueue(enumerator.Current);
            }
        }

        public int Count
        {
            get
            {
                return this.size;
            }
        }

        public void Enqueue(T item)
        {
            if (this.size == this.array.Length)
            {
                int newCapacity = this.array.Length * 2;
                SetCapacity(newCapacity);
            }

            this.array[this.tail] = item;
            this.tail = (this.tail + 1) % this.array.Length;
            this.size++;
        }

        public T Dequeue()
        {
            if (this.size == 0)
            {
                throw new InvalidOperationException();
            }

            T removed = this.array[this.head];
            this.array[this.head] = default(T);
            this.head = (this.head + 1) % this.array.Length;
            this.size--;

            return removed;
        }

        public T Peek()
        {
            if (this.size == 0)
            {
                throw new InvalidOperationException();
            }

            return this.array[this.head];
        }

        private void SetCapacity(int newCapacity)
        {
            T[] newArray = new T[newCapacity];
            if (this.size > 0)
            {
                if (this.head < this.tail)
                {
                    Array.Copy(this.array, this.head, newArray, 0, this.size);
                }
                else
                {
                    Array.Copy(this.array, this.head, newArray, 0, this.array.Length - this.head);
                    Array.Copy(this.array, 0, newArray, this.array.Length - this.head, this.tail);
                }
            }

            this.array = newArray;
            this.head = 0;
            if (this.size == newCapacity)
            {
                this.tail = 0;
            }
            else
            {
                this.tail = this.size;
            }
        }

        public bool Contains(T item)
        {
            int index = this.head;
            int count = this.size;

            while (count-- > 0)
            {
                if ((Object)item == null)
                {
                    if ((Object)this.array[index] == null)
                    {
                        return true;
                    }
                }
                else if (this.array[index] != null && this.array[index].Equals(item))
                {
                    return true;
                }

                index = (index + 1) % this.array.Length;
            }

            return false;
        }

        public T[] ToArray()
        {
            T[] newArray = new T[this.size];
            if (this.size == 0)
            {
                return newArray;
            }

            if (this.head < this.tail)
            {
                Array.Copy(this.array, this.head, newArray, 0, this.size);
            }
            else
            {
                Array.Copy(this.array, this.head, newArray, 0, this.array.Length - this.head);
                Array.Copy(this.array, 0, newArray, this.array.Length - this.head, this.tail);
            }

            return newArray;
        }

        public void Clear()
        {
            if (this.head < this.tail)
            {
                Array.Clear(this.array, this.head, this.size);
            }
            else
            {
                Array.Clear(this.array, this.head, this.array.Length - this.head);
                Array.Clear(this.array, 0, this.tail);
            }

            this.head = 0;
            this.tail = 0;
            this.size = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new StaticQueueEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal T GetElement(int index)
        {
            return this.array[(this.head + index) % this.array.Length];
        }
    }
}
