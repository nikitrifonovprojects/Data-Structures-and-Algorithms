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

        [TestMethod]
        public void RemoveRootWithOnlyOneChildWorksCorrectly()
        {
            //Arrange
            var tree = new Tree<string>();
            string value = "a";
            string value1 = "b";
            var expected = new List<string>();
            expected.Add(value1);
            var input = new Tree<string>.TreeNode(value);
            var input1 = new Tree<string>.TreeNode(value1);
            tree.Add(input);
            tree.Add(input1);

            //Act
            tree.Remove("a");
            var result = new List<string>();
            foreach (var val in tree)
            {
                result.Add(val);
            }

            //Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void RemoveNodeWhenOnlyTwoNodesInTreeWorksCorrectly()
        {
            //Arrange
            var tree = new Tree<string>();
            string value = "a";
            string value1 = "b";
            var expected = new List<string>();
            expected.Add(value);
            var input = new Tree<string>.TreeNode(value);
            var input1 = new Tree<string>.TreeNode(value1);
            tree.Add(input);
            tree.Add(input1);

            //Act
            tree.Remove("b");
            var result = new List<string>();
            foreach (var val in tree)
            {
                result.Add(val);
            }

            //Assert
            CollectionAssert.AreEqual(expected, result);
            Assert.IsNull(tree.Root.Parent);
        }

        [TestMethod]
        public void RemoveRootValueWorksCorrectly()
        {
            //Arrange
            var tree = new Tree<int>();
            var expected = new List<int>();
            int actual = 0;
            int count = 0;
            for (int i = 0; i < 10; i++)
            {
                var node = new Tree<int>.TreeNode(count);
                count++;
                tree.Add(node);
                expected.Add(node.Value);
                for (int j = 0; j < 10; j++)
                {
                    var child = new Tree<int>.TreeNode(count);
                    count++;
                    tree.Add(child, node);
                    expected.Add(child.Value);
                    for (int k = 0; k < 10; k++)
                    {
                        var newChild = new Tree<int>.TreeNode(count);
                        count++;
                        tree.Add(newChild, child);
                        expected.Add(newChild.Value);
                        for (int f = 0; f < 20; f++)
                        {
                            var lastChild = new Tree<int>.TreeNode(count);
                            count++;
                            tree.Add(lastChild, newChild);
                            expected.Add(lastChild.Value);
                        }
                    }
                }
            }

            //Act
            tree.Remove(0);
            expected.Remove(0);
            Assert.AreEqual(count - 1, expected.Count);
            bool notFound = tree.Contains(0);
            var actualCol = new List<int>();
            foreach (var item in tree)
            {
                actual++;
                actualCol.Add(item);
            }

            //Assert
            Assert.IsFalse(notFound);
            Assert.AreEqual(tree.Count, count - 1);
            Assert.AreEqual(tree.Count, actual);
            Assert.IsNull(tree.Root.Parent);
            CollectionAssert.AreEquivalent(expected, actualCol);
        }

        [TestMethod]
        public void CreateTreeWorksCorrectly()
        {
            //Arrange
            string input = "A";
            var expectedCount = 1;
            var expectedTree = new List<string>() { "A" };

            //Act
            var tree = new Tree<string>(input);
            var actual = new List<string>();
            foreach (var item in tree)
            {
                actual.Add(item);
            }

            //Assert
            Assert.AreEqual(expectedCount, tree.Count);
            CollectionAssert.AreEqual(expectedTree, actual);
        }
    }
}
