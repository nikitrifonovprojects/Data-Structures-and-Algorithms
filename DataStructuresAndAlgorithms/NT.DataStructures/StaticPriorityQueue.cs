using System;
using System.Collections.Generic;
using System.Linq;

namespace NT.DataStructures
{
    public class StaticPriorityQueue<T>
    {
        private SortedList<int, Queue<T>> priorityChains;
        private int count;

        public StaticPriorityQueue(int capacity = 0)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("The capacity cannot be less than 0!");
            }

            this.priorityChains = new SortedList<int, Queue<T>>(capacity);
            this.count = 0;
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public T Peek()
        {
            if (this.count == 0)
            {
                throw new ArgumentException("There are no items in the queue!");
            }

            return this.priorityChains.Last(x => x.Value.Count > 0).Value.Peek();
        }

        public void Enqueue(int priority, T value)
        {
            if (priority < 0)
            {
                throw new ArgumentOutOfRangeException("The priority cannot be less than 0!");
            }

            if (!this.priorityChains.ContainsKey(priority))
            {
                this.priorityChains.Add(priority, new Queue<T>());
            }

            this.priorityChains[priority].Enqueue(value);
            this.count++;
        }

        public void Enqueue(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }

            var enumerator = collection.GetEnumerator();

            while (enumerator.MoveNext())
            {
                Enqueue(enumerator.Current);
            }
        }

        public void Enqueue(int priority, IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }

            var enumerator = collection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Enqueue(priority, enumerator.Current);
            }
        }

        public void Enqueue(T value)
        {
            Enqueue(0, value);
        }

        public T Dequeue()
        {
            if (this.count == 0)
            {
                throw new ArgumentException("There are no items in the queue!");
            }

            T itemToRemove = this.priorityChains.Last(x => x.Value.Count > 0).Value.Dequeue();
            this.count--;

            return itemToRemove;
        }

        public bool Contains(int priority, T value)
        {
            return this.priorityChains[priority].Contains(value);
        }

        public bool Contains(T value)
        {
            foreach (var chain in this.priorityChains.Where(x => x.Value.Count > 0))
            {
                if (chain.Value.Contains(value))
                {
                    return true;
                }
            }

            return false;
        }

        public T[] ToArray()
        {
            T[] newArray = new T[this.count];
            if (this.count == 0)
            {
                return newArray;
            }

            int index = 0;
            foreach (var chain in this.priorityChains.Where(x => x.Value.Count > 0).Select(x => x.Value))
            {
                Array.Copy(chain.ToArray(), 0, newArray, index, chain.Count);
                index += chain.Count;
            }

            return newArray;
        }

        public void Clear()
        {
            this.priorityChains.Clear();
            this.count = 0;
        }
    }
}
