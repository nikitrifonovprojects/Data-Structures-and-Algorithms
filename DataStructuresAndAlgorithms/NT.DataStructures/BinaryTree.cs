using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT.DataStructures
{
    public class BinaryTree<T> : ICollection<T> where T : IComparable<T>
    {
        private Node root;
        private int count;

        public BinaryTree()
        {
            this.root = null;
        }

        public BinaryTree(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            this.root.Value = value;
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
            if (this.root.Value == null)
            {
                this.root.Value = item;
            }
            else
            {
                InsertNode(this.root, item);
            }
        }

        private void InsertNode(Node parent, T item)
        {
            if (parent.Value.CompareTo(item) > 0)
            {
                InsertNodeLeft(parent, item);
            }
            else
            {
                InsertNodeRight(parent, item);
            }
        }

        private void InsertNodeLeft(Node parent, T item)
        {
            var current = parent;
            while (current.Left != null)
            {
                if (current.Left.Value.CompareTo(item) > 0)
                {
                    current = current.Left;
                }
                else
                {
                    InsertNodeRight(current, item);
                }
            }

            current.Left = new Node(item);
        }

        private void InsertNodeRight(Node parent, T item)
        {
            var current = parent;
            while (current.Right != null)
            {
                if (current.Right.Value.CompareTo(item) <= 0)
                {
                    current = current.Right;
                }
                else
                {
                    InsertNodeLeft(current, item);
                }
            }

            current.Right = new Node(item);
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

        internal sealed class Node
        {
            internal Node(T value)
            {
                this.Value = value;
                this.Left = null;
                this.Right = null;
            }

            internal T Value { get; set; }

            internal Node Left { get; set; }

            internal Node Right { get; set; }
        }
    }
}
