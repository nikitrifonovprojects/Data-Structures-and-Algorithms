using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT.DataStructures
{
    public class NormalLinkedList<T> : ICollection<T>
    {
        internal NormalLinkedListNode<T> head;
        private int count;

        public NormalLinkedList()
        {

        }

        public NormalLinkedList(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }

            foreach (T item in collection)
            {
                AddLast(item);
            }
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public NormalLinkedListNode<T> First
        {
            get
            {
                return this.head;
            }
        }

        public NormalLinkedListNode<T> Last
        {
            get
            {
                if (this.head == null)
                {
                    return null;
                }

                return this.head.next;
            }
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new NormalLinkedListEnumerator<T>(this);
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public sealed class NormalLinkedListNode<T>
    {
        internal NormalLinkedList<T> list;
        internal NormalLinkedListNode<T> next;
        internal T content;

        public NormalLinkedListNode(T content)
        {
            this.content = content;
        }

        public NormalLinkedListNode(NormalLinkedList<T> list, T content)
        {
            this.list = list;
            this.content = content;
        }

        public NormalLinkedList<T> List
        {
            get
            {
                return this.list;
            }
        }

        public NormalLinkedListNode<T> Next
        {
            get
            {
                if (this.next == null || this == this.list.head)
                {
                    return null;
                }

                return this.next;
            }
        }

        public T Content { get; set; }

        public void Invalidate()
        {
            this.list = null;
            this.next = null;
        }
    }
}
