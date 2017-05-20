using System;
using System.Collections;
using System.Collections.Generic;

namespace NT.DataStructures
{
    public class LinkedListEnumerator<T> : IEnumerator<T>
    {
        private T current;
        private int index;
        private LinkedList<T> list;
        private LinkedListNode<T> node;

        public LinkedListEnumerator(LinkedList<T> list)
        {
            this.List = list;
            this.Node = list.head;
            this.current = default(T);
            this.index = 0;
        }

        public LinkedList<T> List
        {
            get
            {
                return this.list;
            }
            set
            {
                this.list = value;
            }
        }

        public LinkedListNode<T> Node
        {
            get
            {
                return this.node;
            }
            set
            {
                this.node = value;
            }
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
                if (this.index == 0 || this.index == this.list.Count + 1)
                {
                    throw new InvalidOperationException();
                }

                return this.current;
            }
        }

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            if (this.node == null)
            {
                this.index = this.list.Count + 1;
                return false;
            }

            this.index++;
            this.current = this.node.content;
            this.node = this.node.next;
            if (this.node == this.list.head)
            {
                this.node = null;
            }

            return true;
        }

        public void Reset()
        {
            this.current = default(T);
            this.node = this.list.head;
            this.index = 0;
        }
    }
}
