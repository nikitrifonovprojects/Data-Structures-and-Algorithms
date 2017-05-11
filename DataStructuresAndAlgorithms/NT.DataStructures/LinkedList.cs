using System;
using System.Collections;
using System.Collections.Generic;

namespace NT.DataStructures
{
    public class LinkedList<T> : ICollection<T>, IEnumerable<T>
    {
        public LinkedListNode<T> head;
        private int count;

        public LinkedList()
        {

        }

        public LinkedList(IEnumerable<T> collection)
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

        public LinkedListNode<T> First
        {
            get
            {
                return this.head;
            }
        }

        public LinkedListNode<T> Last
        {
            get
            {
                if (this.head == null)
                {
                    return null;
                }
                else
                {
                    return this.head.prev;
                }
            }
        }

        public void AddLast(T item)
        {
            LinkedListNode<T> result = new LinkedListNode<T>(this, item);
            if (this.head == null)
            {
                InsertNodeToEmptyList(result);
            }
            else
            {
                InsertNodeBefore(this.head, result);
            }
        }

        private void AddLast(LinkedListNode<T> node)
        {

            if (this.head == null)
            {
                InsertNodeToEmptyList(node);
            }
            else
            {
                InsertNodeBefore(this.head, node);
            }

            node.list = this;
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
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private void InsertNodeToEmptyList(LinkedListNode<T> newNode)
        {
            newNode.next = newNode;
            newNode.prev = newNode;
            this.head = newNode;
            this.count++;
        }

        private void InsertNodeBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            newNode.next = node;
            newNode.prev = node.prev;
            node.prev.next = newNode;
            node.prev = newNode;
            this.count++;
        }
    }

    public sealed class LinkedListNode<T>
    {
        internal LinkedList<T> list;
        internal LinkedListNode<T> next;
        internal LinkedListNode<T> prev;
        internal T content;

        public LinkedListNode(T content)
        {
            this.content = content;
        }

        public LinkedListNode(LinkedList<T> list, T content)
        {
            this.list = list;
            this.content = content;
        }

        public LinkedList<T> List
        {
            get
            {
                return this.list;
            }
        }

        public LinkedListNode<T> Next
        {
            get
            {
                if (this.next == null || this == this.list.head)
                {
                    return null;
                }
                else
                {
                    return this.next;
                }
            }
        }

        public LinkedListNode<T> Previous
        {
            get
            {
                if (this.prev == null || this == this.list.head)
                {
                    return null;
                }
                else
                {
                    return this.prev;
                }
            }
        }

        public T Content
        {
            get
            {
                return this.content;
            }
            set
            {
                this.content = value;
            }
        }

        public void Invalidate()
        {
            this.list = null;
            this.next = null;
            this.prev = null;
        }
    }
}
