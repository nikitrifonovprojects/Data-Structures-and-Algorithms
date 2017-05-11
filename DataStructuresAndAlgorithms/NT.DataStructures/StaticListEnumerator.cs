using System;
using System.Collections;
using System.Collections.Generic;

namespace NT.DataStructures
{
    public class StaticListEnumerator<T> : IEnumerator<T>
    {
        private int position;

        public StaticListEnumerator(StaticList<T> list)
        {
            this.List = list;
            this.position = 0;
        }

        public StaticList<T> List { get; set; }

        public T Current
        {
            get
            {
                return this.List[this.position];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return (Object)this.Current;
            }
        }

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            if (this.position < this.List.Count)
            {
                this.position++;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            this.position = 0;
        }
    }
}
