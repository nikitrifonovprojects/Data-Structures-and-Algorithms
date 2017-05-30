using System;
using System.Collections.Generic;
using NT.DataStructures;

namespace NT.Algorithms
{
    public class BFSearch<T> where T : IComparable<T>
    {
        public List<T> BreadthFirstSearch(Tree<T>.TreeNode root)
        {
            var queue = new Queue<Tree<T>.TreeNode>();
            var result = new List<T>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                result.Add(currentNode.Value);
                foreach (var node in currentNode.NodeChildren)
                {
                    queue.Enqueue(node);
                }
            }

            return result;
        }
    }
}
