using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NT.DataStructures
{
    public class Tree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private TreeNode root;
        private HashSet<TreeNode> uniqueNodes;

        public Tree()
        {
            this.uniqueNodes = new HashSet<TreeNode>();
        }

        public Tree(T value) : this()
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            this.root = new TreeNode(value);
            this.uniqueNodes.Add(this.root);
        }

        public TreeNode Root
        {
            get
            {
                return this.root;
            }
        }

        public int Count
        {
            get
            {
                return this.uniqueNodes.Count;
            }
        }

        public void Add(TreeNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }

            if (this.root == null)
            {
                this.root = node;
            }
            else
            {
                if (this.uniqueNodes.Contains(node))
                {
                    throw new ArgumentException("The tree does not allow duplicate nodes!");
                }

                node.Parent = this.root;
                this.root.NodeChildren.Add(node);
            }

            this.uniqueNodes.Add(node);
        }

        public void Add(TreeNode node, TreeNode parent)
        {
            if (this.root == null)
            {
                throw new ArgumentNullException("The tree has no root!");
            }

            if (node == null || parent == null)
            {
                throw new ArgumentNullException();
            }

            if (this.uniqueNodes.Contains(node))
            {
                throw new ArgumentException("The tree does not allow duplicate nodes!");
            }

            this.uniqueNodes.Add(node);
            node.Parent = parent;
            parent.AddChild(node);
        }

        public bool Remove(T value)
        {
            var nodeToRemove = DepthFirstSearch(value, this.root);
            if (nodeToRemove != null)
            {
                RemoveNode(nodeToRemove);
                return true;
            }

            return false;
        }

        private void RemoveNode(TreeNode node)
        {
            if (node == this.root)
            {
                if (this.root.NodeCount == 0)
                {
                    this.root = null;
                }
                else
                {
                    var futureRoot = this.root.NodeChildren[0];
                    for (int i = 1; i < this.root.NodeCount; i++)
                    {
                        futureRoot.AddChild(this.root.NodeChildren[i]);
                    }

                    this.root = futureRoot;
                    this.root.Parent = null;
                }

            }
            else
            {
                node.Parent.RemoveChild(node);
            }

            this.uniqueNodes.Remove(node);
        }

        public bool Contains(T value)
        {
            return DepthFirstSearch(value, this.root) != null;
        }

        public TreeNode DepthFirstSearch(T value, TreeNode node)
        {
            if (node.Value.CompareTo(value) == 0)
            {
                return node;
            }

            foreach (var child in node.NodeChildren)
            {
                var resultNode = DepthFirstSearch(value, child);
                if (resultNode != null)
                {
                    return resultNode;
                }
            }

            return null;
        }

        private TreeNode DepthFirstSearch(TreeNode nodeToFind, TreeNode node)
        {
            if (node == nodeToFind)
            {
                return node;
            }

            foreach (var child in node.NodeChildren)
            {
                var resultNode = DepthFirstSearch(node, child);
                if (resultNode != null)
                {
                    return resultNode;
                }
            }

            return null;
        }

        public void Clear()
        {
            this.root = null;
            this.uniqueNodes.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new TreeEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class TreeEnumerator : IEnumerator<T>
        {
            private Tree<T> tree;
            private T current;
            private Queue<TreeNode> queue;
            private bool hasStarted;

            public TreeEnumerator(Tree<T> tree)
            {
                this.tree = tree;
                this.current = default(T);
                this.queue = new Queue<TreeNode>();
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
                    return this.current;
                }
            }

            public void Dispose()
            {
                
            }

            public bool MoveNext()
            {
                if (this.tree.Count == 0)
                {
                    return false;
                }

                if (!this.hasStarted)
                {
                    this.hasStarted = true;
                    this.queue.Enqueue(this.tree.root);
                }

                if (this.queue.Count > 0)
                {
                    var currentNode = this.queue.Dequeue();
                    this.current = currentNode.Value;

                    foreach (var node in currentNode.NodeChildren)
                    {
                        this.queue.Enqueue(node);
                    }

                    return true;
                }

                return false;
            }

            public void Reset()
            {
                this.current = default(T);
                this.queue.Clear();
                this.hasStarted = false;
            }
        }

        public sealed class TreeNode
        {
            public TreeNode(T value)
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                this.Value = value;
                this.Parent = null;
                this.NodeChildren = new List<TreeNode>();
            }

            public T Value { get; set; }

            public TreeNode Parent { get; set; }

            public List<TreeNode> NodeChildren { get; set; }

            public int NodeCount
            {
                get
                {
                    return this.NodeChildren.Count;
                }
            }

            internal void AddChild(TreeNode node)
            {
                this.NodeChildren.Add(node);
            }

            internal void RemoveChild(TreeNode node)
            {
                if (!this.NodeChildren.Contains(node))
                {
                    throw new ArgumentException("The node doesn't exist!");
                }

                if (node.NodeCount == 0)
                {
                    this.NodeChildren.Remove(node);
                }
                else
                {
                    foreach (var item in node.NodeChildren)
                    {
                        this.NodeChildren.Add(item);
                    }

                    this.NodeChildren.Remove(node);
                }
            }
        }
    }
}
