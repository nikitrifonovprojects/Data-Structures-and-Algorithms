using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace NT.DataStructures
{
    public class BinaryTree<T> : ICollection<T> where T : IComparable<T>
    {
        private BinaryNode root;
        private int count;

        public BinaryTree()
        {
            
        }

        public BinaryTree(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            this.root.Value = value;
            this.count++;
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
            if (item == null)
            {
                throw new ArgumentNullException();
            }

            InsertNode(item);
        }

        private void InsertNode(T item)
        {
            BinaryNode newNode = new BinaryNode(item);
            if (this.root == null)
            {
                this.root = newNode;
                this.count++;
            }
            else
            {
                var current = this.root;
                while (current != null)
                {
                    var tempParent = current;
                    if (current.Value.CompareTo(item) < 0)
                    {
                        current = current.Left;
                        if (current == null)
                        {
                            tempParent.Left = newNode;
                            newNode.Parent = tempParent;
                        }
                    }
                    else
                    {
                        current = current.Right;
                        if (current == null)
                        {
                            tempParent.Right = newNode;
                            newNode.Parent = tempParent;
                        }
                    }
                }

                this.count++;
            }
        }

        public List<T> DepthFirstSearch()
        {
            if (this.count == 0)
            {
                return new List<T>();
            }

            Debug.Assert(this.root != null);

            var stack = new Stack<BinaryNode>();
            var result = new List<T>();
            stack.Push(this.root);
            while (stack.Count > 0)
            {
                BinaryNode node = stack.Pop();
                result.Add(node.Value);
                if (node.Left != null)
                {
                    stack.Push(node.Left);
                }

                if (node.Right != null)
                {
                    stack.Push(node.Right);
                }
            }

            return result;
        }

        public List<T> BreadthFirstSearch()
        {
            if (this.count == 0)
            {
                return new List<T>();
            }

            Debug.Assert(this.root != null);

            var queue = new Queue<BinaryNode>();
            var result = new List<T>();
            queue.Enqueue(this.root);
            while (queue.Count > 0)
            {
                BinaryNode node = queue.Dequeue();
                result.Add(node.Value);
                if (node.Left != null)
                {
                    queue.Enqueue(node.Left);
                }

                if (node.Right != null)
                {
                    queue.Enqueue(node.Right);
                }
            }

            return result;
        }

        public void Clear()
        {
            this.root = null;
            this.count = 0;
        }

        public bool Contains(T item)
        {
            return (FindNodeByValue(this.root, item) != null);
        }

        private BinaryNode FindNodeByValue(BinaryNode node, T item)
        {
            var currentCompare = node.Value.CompareTo(item);
            if (currentCompare == 0)
            {
                return node;
            }
            else
            {
                if (currentCompare < 0 && node.Left != null)
                {
                    return FindNodeByValue(node.Left, item);
                }
                else if (currentCompare > 0 && node.Right != null)
                {
                    return FindNodeByValue(node.Right, item);
                }
            }

            return null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            var treeList = DepthFirstSearch();
            if (array == null)
            {
                throw new ArgumentNullException();
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (array.Length - arrayIndex > treeList.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            int count = 0;
            for (int i = arrayIndex; i < array.Length; i++)
            {
                array[i] = treeList[count];
                count++;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return DepthFirstSearch().GetEnumerator();
        }

        public bool Remove(T item)
        {
            BinaryNode nodeToRemove = FindNodeByValue(this.root, item);
            if (nodeToRemove == null)
            {
                return false;
            }

            if (nodeToRemove.Left == null && nodeToRemove.Right == null)
            {
                BinaryNode tempParent = nodeToRemove.Parent;
                if (tempParent == null)
                {
                    this.root = null;
                }
                else if (nodeToRemove == tempParent.Left)
                {
                    tempParent.Left = null;
                }
                else
                {
                    tempParent.Right = null;
                }
            }
            else if (nodeToRemove.Left == null || nodeToRemove.Right == null)
            {
                BinaryNode tempChild = nodeToRemove.Left == null ? nodeToRemove.Right : nodeToRemove.Left;
                BinaryNode tempParent = nodeToRemove.Parent;

                if (tempParent == null)
                {
                    this.root = tempChild;
                    this.root.Parent = null;
                }
                else if (nodeToRemove == tempParent.Left)
                {
                    tempParent.Left = tempChild;
                }
                else
                {
                    tempParent.Right = tempChild;
                }
            }
            else if (nodeToRemove.Right != null && nodeToRemove.Left != null)
            {
                var minNode = FindMinNode(nodeToRemove.Left);
                var leftoverChain = minNode.Left;
                nodeToRemove.Value = minNode.Value;
                var minNodeParent = minNode.Parent;
                if (minNode.Parent != nodeToRemove)
                {
                    minNodeParent.Right = leftoverChain;
                }
                else
                {
                    minNodeParent.Left = leftoverChain;
                }

                if (leftoverChain != null)
                {
                    leftoverChain.Parent = minNodeParent;
                }
            }
            else
            {
                throw new InvalidOperationException();
            }

            this.count--;
            return true;
        }

        private BinaryNode FindMinNode(BinaryNode node)
        {
            var current = node;
            if (current.Right == null)
            {
                return current;
            }

            return FindMinNode(current.Right);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal sealed class BinaryNode
        {
            internal BinaryNode(T value)
            {
                this.Value = value;
                this.Left = null;
                this.Right = null;
                this.Parent = null;
            }

            internal T Value { get; set; }

            internal BinaryNode Parent { get; set; }

            internal BinaryNode Left { get; set; }

            internal BinaryNode Right { get; set; }
        }
    }
}
