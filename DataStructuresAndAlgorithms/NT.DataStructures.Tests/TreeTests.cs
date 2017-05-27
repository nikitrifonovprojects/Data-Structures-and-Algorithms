using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NT.DataStructures.Tests
{
    [TestClass]
    public class TreeTests
    {
        [TestMethod]
        public void ContainsWorksCorrectly()
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
            bool result = tree.Contains(22);
            tree.Remove(22);
            bool notFound = tree.Contains(22);

            //Assert
            Assert.IsTrue(result);
            Assert.IsFalse(notFound);
            Assert.AreEqual(tree.Count, count - 1);
        }

        [TestMethod]
        public void RemoveValueWorksCorrectly()
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
            tree.Remove(22);
            bool notFound = tree.Contains(22);

            //Assert
            Assert.IsFalse(notFound);
            Assert.AreEqual(tree.Count, count - 1);
        }

        [TestMethod]
        public void AddValueWorksCorrectly()
        {
            //Arrange
            var tree = new Tree<int>();
            var expected = new List<int>();
            int count = 0;
            for (int i = 0; i < 10; i++)
            {
                var node = new Tree<int>.TreeNode(count);
                expected.Add(count);
                count++;
                tree.Add(node);
            }

            var result = new List<int>();

            //Act
            foreach (var item in tree)
            {
                result.Add(item);
            }

            //Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AddValueToParentWorksCorrectly()
        {
            //Arrange
            var tree = new Tree<int>();
            var expected = new List<int>();
            int count = 0;
            for (int i = 0; i < 10; i++)
            {
                var node = new Tree<int>.TreeNode(count);
                expected.Add(count);
                count++;
                tree.Add(node);
                for (int k = 0; k < 10; k++)
                {
                    var newNode = new Tree<int>.TreeNode(count);
                    expected.Add(count);
                    count++;
                    tree.Add(newNode, node);
                }
            }

            var result = new List<int>();

            //Act
            foreach (var item in tree)
            {
                result.Add(item);
            }

            //Assert
            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod]
        public void EnumeratorWorksCorrectly()
        {
            //Arrange
            var tree = new Tree<int>();
            var expected = new List<int>();
            int count = 0;
            for (int i = 0; i < 10; i++)
            {
                var node = new Tree<int>.TreeNode(count);
                expected.Add(count);
                count++;
                tree.Add(node);
            }

            var result = new List<int>();

            //Act
            foreach (var item in tree)
            {
                result.Add(item);
            }

            //Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EnumeratorDoesntEnumerateEmptyTree()
        {
            //Arrange
            var tree = new Tree<int>();
            bool check = false;
            //Act
            foreach (var item in tree)
            {
                check = true;
            }

            //Assert
            Assert.IsFalse(check);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingTreeWithNullValueThrowsException()
        {
            //Arrange
            string input = null;
            var tree = new Tree<string>(input);
            
            //Act
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingANodehNullValueThrowsException()
        {
            //Arrange
            string value = null;
            var input = new Tree<string>.TreeNode(value);

            //Act
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddingTheSameValueThrowsException()
        {
            //Arrange
            string value = "a";
            var input = new Tree<string>.TreeNode(value);
            var tree = new Tree<string>();

            //Act
            tree.Add(input);
            tree.Add(input);
            //Assert
        }
    }
}
