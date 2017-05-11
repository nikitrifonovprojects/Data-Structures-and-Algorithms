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

        public LinkedListNode<T> AddLast(T item)
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

            return result;
        }

        private void AddLast(LinkedListNode<T> node)
        {
            ValidateNode(node);
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
            this.AddLast(item);
        }

        public void RemoveFirst()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException();
            }

            RemoveNode(this.head);
        }

        public void RemoveLast()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException();
            }

            RemoveNode(this.head.prev);
        }

        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
        {
            ValidateNode(node);
            LinkedListNode<T> result = new LinkedListNode<T>(value);
            InsertNodeBefore(node.next, result);

            return result;
        }

        public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            ValidateNode(node);
            ValidateNewNode(newNode);
            InsertNodeBefore(node.next, newNode);
            newNode.list = this;
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            ValidateNode(node);
            LinkedListNode<T> result = new LinkedListNode<T>(node.list, value);
            InsertNodeBefore(node, result);
            if (node == this.head)
            {
                this.head = result;
            }

            return result;
        }

        public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            ValidateNode(node);
            ValidateNewNode(newNode);
            InsertNodeBefore(node, newNode);
            newNode.list = this;
            if (node == this.head)
            {
                this.head = newNode;
            }
        }

        public LinkedListNode<T> AddFirst(T value)
        {
            LinkedListNode<T> result = new LinkedListNode<T>(this, value);
            if (this.head == null)
            {
                InsertNodeToEmptyList(result);
            }
            else
            {
                InsertNodeBefore(this.head, result);
                this.head = result;
            }

            return result;
        }

        public void AddFirst(LinkedListNode<T> node)
        {
            ValidateNewNode(node);
            if (this.head == null)
            {
                InsertNodeToEmptyList(node);
            }
            else
            {
                InsertNodeBefore(this.head, node);
                this.head = node;
            }

            node.list = this;
        }

        public void Clear()
        {
            LinkedListNode<T> current = this.head;
            while (current != null)
            {
                LinkedListNode<T> temp = current;
                current = current.Next;
                temp.Invalidate();
            }

            this.head = null;
            this.count = 0;
        }

        public bool Contains(T item)
        {
            return Find(item) != null;
        }

        public void CopyTo(T[] array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException();
            }

            if (index < 0 || index > array.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (array.Length - index < this.Count)
            {
                throw new ArgumentException();
            }

            LinkedListNode<T> node = this.head;
            if (node != null)
            {
                do
                {
                    array[index++] = node.content;
                    node = node.next;
                } while (node != this.head);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new LinkedListEnumerator<T>(this);
        }

        public bool Remove(T value)
        {
            LinkedListNode<T> node = Find(value);
            if (node != null)
            {
                RemoveNode(node);
                return true;
            }

            return false;
        }

        private void Remove(LinkedListNode<T> node)
        {
            ValidateNode(node);
            RemoveNode(node);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public LinkedListNode<T> Find(T value)
        {
            LinkedListNode<T> node = this.head;
            EqualityComparer<T> c = EqualityComparer<T>.Default;
            if (node != null)
            {
                if (value != null)
                {
                    do
                    {
                        if (c.Equals(node.content, value))
                        {
                            return node;
                        }
                        node = node.next;
                    } while (node != this.head);
                }
                else
                {
                    do
                    {
                        if (node.content == null)
                        {
                            return node;
                        }
                        node = node.next;
                    } while (node != this.head);
                }
            }

            return null;
        }

        public LinkedListNode<T> FindLast(T value)
        {
            if (this.head == null)
            {
                return null;
            }

            LinkedListNode<T> last = this.head.prev;
            LinkedListNode<T> node = last;
            EqualityComparer<T> c = EqualityComparer<T>.Default;
            if (node != null)
            {
                if (value != null)
                {
                    do
                    {
                        if (c.Equals(node.content, value))
                        {
                            return node;
                        }

                        node = node.prev;
                    } while (node != last);
                }
                else
                {
                    do
                    {
                        if (node.content == null)
                        {
                            return node;
                        }
                        node = node.prev;
                    } while (node != last);
                }
            }

            return null;
        }

        private void InsertNodeToEmptyList(LinkedListNode<T> newNode)
        {
            if (this.head == null && this.count == 0)
            {
                newNode.next = newNode;
                newNode.prev = newNode;
                this.head = newNode;
                this.count++;
            }
        }

        private void InsertNodeBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            newNode.next = node;
            newNode.prev = node.prev;
            node.prev.next = newNode;
            node.prev = newNode;
            this.count++;
        }

        private void RemoveNode(LinkedListNode<T> node)
        {
            if (node.next == node)
            {
                this.head = null;
            }
            else
            {
                node.next.prev = node.prev;
                node.prev.next = node.next;
                if (this.head == node)
                {
                    this.head = node.next;
                }
            }

            node.Invalidate();
            this.count--;
        }

        internal void ValidateNewNode(LinkedListNode<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }

            if (node.list != null)
            {
                throw new InvalidOperationException();
            }
        }


        internal void ValidateNode(LinkedListNode<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }

            if (node.list != this)
            {
                throw new InvalidOperationException();
            }
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
