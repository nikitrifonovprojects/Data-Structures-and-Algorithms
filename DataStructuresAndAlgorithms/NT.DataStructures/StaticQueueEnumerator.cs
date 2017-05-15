using System;
using System.Collections;
using System.Collections.Generic;

namespace NT.DataStructures
{
    public class StaticQueueEnumerator<T> : IEnumerator<T>
    {
        private StaticQueue<T> queue;
        private int index;
        private T current;

        public StaticQueueEnumerator(StaticQueue<T> queue)
        {
            this.queue = queue;
            this.index = -1;
            this.current = default(T);
        }

        public T Current
        {
            get
            {
                if (this.index < 0)
                {
                    if (this.index == -1)
                    {
                        throw new InvalidOperationException("Enumaration not started!");
                    }
                    else
                    {
                        throw new InvalidOperationException("Enumaration ended");
                    }
                }

                return this.current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                if (this.index < 0)
                {
                    if (this.index == -1)
                    {
                        throw new InvalidOperationException("Enumaration not started!");
                    }
                    else
                    {
                        throw new InvalidOperationException("Enumaration ended");
                    }
                }

                return this.current;
            }
        }

        public void Dispose()
        {
            this.index = -2;
            this.current = default(T);
        }

        public bool MoveNext()
        {
            if (this.index == -2)
            {
                return false;
            }

            this.index++;
            if (this.index == this.queue.size)
            {
                this.index = -2;
                this.current = default(T);

                return false;
            }

            this.current = this.queue.GetElement(this.index);

            return true;
        }

        public void Reset()
        {
            this.index = -1;
            this.current = default(T);
        }
    }
}
