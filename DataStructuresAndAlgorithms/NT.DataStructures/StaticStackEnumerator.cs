using System;
using System.Collections;
using System.Collections.Generic;

namespace NT.DataStructures
{
    public class StaticStackEnumerator<T> : IEnumerator<T>
    {
        private StaticStack<T> stack;
        private int index;
        private T current;

        public StaticStackEnumerator(StaticStack<T> stack)
        {
            this.stack = stack;
            this.index = -2;
            this.current = default(T);
        }

        public T Current
        {
            get
            {
                return ReturnCurrent();
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return ReturnCurrent();
            }
        }

        private T ReturnCurrent()
        {
            if (this.index == -2)
            {
                throw new InvalidOperationException("Enumeration not started!");
            }

            if (this.index == -1)
            {
                throw new InvalidOperationException("Enumeration ended!");
            }

            return this.current;
        }

        public void Dispose()
        {
            this.index = -1;
        }

        public bool MoveNext()
        {
            bool retrivedValue = false;

            if (this.index == -2)
            {
                this.index = this.stack.size - 1;
                if (this.index >= 0)
                {
                    retrivedValue = true;
                }

                if (retrivedValue)
                {
                    this.current = this.stack.array[this.index];
                    return retrivedValue;
                }
            }

            if (this.index == -1)
            {
                return false;
            }

            if (--this.index >= 0)
            {
                retrivedValue = true;
            }

            if (retrivedValue)
            {
                this.current = this.stack.array[this.index];
            }
            else
            {
                this.current = default(T);
            }

            return retrivedValue;
        }

        public void Reset()
        {
            this.index = -2;
            this.current = default(T);
        }
    }
}
