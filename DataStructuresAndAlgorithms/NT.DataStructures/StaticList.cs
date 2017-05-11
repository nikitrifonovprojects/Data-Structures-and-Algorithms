using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NT.DataStructures
{
    public class StaticList<T> : ICollection<T>, IEnumerable<T>
    {
        private const int DefaultCapacity = 4;
        private T[] items;
        private int size;

        public StaticList()
        {
            this.items = new T[0];
        }

        public StaticList(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException("The capacity cannot be less than 0!");
            }

            if (capacity == 0)
            {
                this.items = new T[0];
            }
            else
            {
                this.items = new T[capacity];
            }
        }

        public StaticList(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("The collection cannot be null!");
            }

            ICollection<T> colect = collection as ICollection<T>;
            if (colect != null)
            {
                int count = colect.Count;
                if (count == 0)
                {
                    this.items = new T[0];
                }
                else
                {
                    this.items = new T[count];
                    colect.CopyTo(this.items, 0);
                    this.size = count;
                }
            }
            else
            {
                this.size = 0;
                this.items = new T[0];
            }
        }

        public int Capacity
        {
            get
            {
                return this.items.Length;
            }
            private set
            {
                if (value < this.size)
                {
                    throw new ArgumentOutOfRangeException("The capacity is too small!");
                }

                if (value != this.items.Length)
                {
                    if (value > 0)
                    {
                        T[] newItems = new T[value];
                        if (this.size > 0)
                        {
                            Array.Copy(this.items, 0, newItems, 0, this.size);
                        }

                        this.items = newItems;
                    }
                }
                else
                {
                    this.items = new T[0];
                }
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

        public T this[int index]
        {
            get
            {
                if (index >= this.size)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return this.items[index];
            }
            set
            {
                if (index >= this.size)
                {
                    throw new ArgumentOutOfRangeException();
                }

                this.items[index] = value;
            }
        }

        public void Add(T item)
        {
            if (this.size == this.items.Length)
            {
                this.EnsureCapacity(this.size + 1);
            }

            this.items[this.size] = item;
            this.size++;
        }

        public void Clear()
        {
            if (this.size > 0)
            {
                Array.Clear(this.items, 0, this.size);
                this.size = 0;
            }
        }

        public bool Contains(T item)
        {
            if ((Object)item == null)
            {
                for (int i = 0; i < this.size; i++)
                {
                    if ((Object)this.items[i] == null)
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                EqualityComparer<T> compare = EqualityComparer<T>.Default;
                for (int i = 0; i < this.size; i++)
                {
                    if (compare.Equals(this.items[i], item))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(this.items, 0, array, arrayIndex, this.size);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T t in this.items)
            {
                if ((Object)t == null)
                {
                    break;
                }

                yield return t;
            }
        }

        public int IndexOf(T item)
        {
            return Array.IndexOf(this.items, item, 0, this.size);
        }

        public void RemoveAt(int index)
        {
            if (index >= this.size)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.size--;
            if (index < this.size)
            {
                Array.Copy(this.items, index + 1, this.items, index, this.size - index);
            }

            this.items[this.size] = default(T);  //?
        }

        public bool Remove(T item)
        {
            int index = this.IndexOf(item);
            if (index >= 0)
            {
                this.RemoveAt(index);
                return true;
            }

            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void EnsureCapacity(int minimum)
        {
            if (this.items.Length < minimum)
            {
                int newCapacity = this.items.Length == 0 ? DefaultCapacity : this.items.Length * 2;

                if (newCapacity < minimum)
                {
                    newCapacity = minimum;
                }

                this.Capacity = newCapacity;
            }
        }
    }
}
