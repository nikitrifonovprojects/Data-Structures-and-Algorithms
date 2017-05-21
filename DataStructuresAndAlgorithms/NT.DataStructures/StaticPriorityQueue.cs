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
                throw new ArgumentException("The capacity cannot be less than 0!");
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
                throw new ArgumentException("The priority cannot be less than 0!");
            }

            if (this.priorityChains.ContainsKey(priority))
            {
                this.priorityChains[priority].Enqueue(value);
                this.count++;
            }
            else
            {
                var newQueue = new Queue<T>();
                newQueue.Enqueue(value);
                this.priorityChains.Add(priority, newQueue);
                this.count++;
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

        public void Clear()
        {
            this.priorityChains.Clear();
        }
    }
}
