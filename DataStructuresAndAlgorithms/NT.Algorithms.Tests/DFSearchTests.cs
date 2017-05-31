using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NT.DataStructures;

namespace NT.Algorithms.Tests
{
    [TestClass]
    public class DFSearchTests
    {
        [TestMethod]
        public void DFSearchWorksCorrectly()
        {
            //Arrange
            var tree = new Tree<int>();
            int count = 0;
            for (int i = 0; i < 10; i++)
            {
                var node = new Tree<int>.TreeNode(count);
                count++;
                tree.Add(node);
                for (int j = 0; j < 10; j++)
                {
                    var child = new Tree<int>.TreeNode(count);
                    count++;
                    tree.Add(child, node);
                    for (int k = 0; k < 10; k++)
                    {
                        var newChild = new Tree<int>.TreeNode(count);
                        count++;
                        tree.Add(newChild, child);
                        for (int f = 0; f < 20; f++)
                        {
                            var lastChild = new Tree<int>.TreeNode(count);
                            count++;
                            tree.Add(lastChild, newChild);
                        }
                    }
                }
            }

            //Act
            var search = new DFSearch<int>();
            var result = search.DepthFirstSearch(tree.Root);
            var actual = new List<int>();
            foreach (var item in tree)
            {
                actual.Add(item);
            }

            //Assert
            CollectionAssert.AreEquivalent(result, actual);
        }

        [TestMethod]
        public void DFSearchRecursiveWorksCorrectly()
        {
            //Arrange
            var tree = new Tree<int>();
            int count = 0;
            for (int i = 0; i < 10; i++)
            {
                var node = new Tree<int>.TreeNode(count);
                count++;
                tree.Add(node);
                for (int j = 0; j < 10; j++)
                {
                    var child = new Tree<int>.TreeNode(count);
                    count++;
                    tree.Add(child, node);
                    for (int k = 0; k < 10; k++)
                    {
                        var newChild = new Tree<int>.TreeNode(count);
                        count++;
                        tree.Add(newChild, child);
                        for (int f = 0; f < 20; f++)
                        {
                            var lastChild = new Tree<int>.TreeNode(count);
                            count++;
                            tree.Add(lastChild, newChild);
                        }
                    }
                }
            }

            //Act
            var search = new DFSearch<int>();
            var result = search.DFSearchRecursive(tree.Root);
            var actual = new List<int>();
            foreach (var item in tree)
            {
                actual.Add(item);
            }

            //Assert
            CollectionAssert.AreEquivalent(result, actual);
        }
    }
}
