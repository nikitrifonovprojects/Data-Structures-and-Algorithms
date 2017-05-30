using System;
using System.Collections.Generic;
using NT.DataStructures;

namespace NT.Algorithms
{
    public class DFSearch<T> where T : IComparable<T>
    {
        public List<T> DepthFirstSearch(Tree<T>.TreeNode root)
        {
            var stack = new Stack<Tree<T>.TreeNode>();
            var result = new List<T>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                result.Add(currentNode.Value);
                foreach (var node in currentNode.NodeChildren)
                {
                    stack.Push(node);
                }
            }

            return result;
        }
    }
}
