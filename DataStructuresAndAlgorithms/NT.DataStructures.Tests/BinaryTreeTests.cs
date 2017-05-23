using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NT.DataStructures.Tests
{
    [TestClass]
    public class BinaryTreeTests
    {
        [TestMethod]
        public void BinaryTreeCopyToWorksCorrectly()
        {
            //Arrange
            var tree = new BinaryTree<int>();
            var expected = new int[] { 10, 15, 9, 22, 44, 5, 3, -4, 11, 17, 0, 6, 4 };
            var result = new int[expected.Length];

            //Act
            tree.Add(10);
            tree.Add(15);
            tree.Add(9);
            tree.Add(22);
            tree.Add(44);
            tree.Add(5);
            tree.Add(3);
            tree.Add(-4);
            tree.Add(11);
            tree.Add(17);
            tree.Add(0);
            tree.Add(6);
            tree.Add(4);

            int count = tree.Count;
            tree.CopyTo(result, 0);

            //Assert
            CollectionAssert.AreEquivalent(expected, result);
            Assert.AreEqual(expected.Length, count);
        }

        [TestMethod]
        public void BinaryTreeContainsWorksCorrectly()
        {
            //Arrange
            var tree = new BinaryTree<int>();

            //Act
            tree.Add(10);
            tree.Add(15);
            tree.Add(9);
            tree.Add(22);
            tree.Add(44);
            tree.Add(5);
            tree.Add(3);
            tree.Add(-4);
            tree.Add(11);
            tree.Add(17);
            tree.Add(0);
            tree.Add(6);
            tree.Add(4);

            bool result = tree.Contains(0);
            tree.Remove(0);
            bool notFound = tree.Contains(0);

            //Assert
            Assert.IsTrue(result);
            Assert.IsFalse(notFound);
        }

        [TestMethod]
        public void BinaryTreeRemoveWorksCorrectly()
        {
            //Arrange
            var tree = new BinaryTree<int>();

            //Act
            tree.Add(10);
            tree.Add(15);
            tree.Add(9);
            tree.Add(22);
            tree.Add(44);
            tree.Add(5);
            tree.Add(3);
            tree.Add(-4);
            tree.Add(11);
            tree.Add(17);
            tree.Add(0);
            tree.Add(6);
            tree.Add(4);
            tree.Add(55);
            tree.Add(31);

            bool result = tree.Contains(22);
            tree.Remove(22);
            bool notFound = tree.Contains(22);

            //Assert
            Assert.IsTrue(result);
            Assert.IsFalse(notFound);
        }

        [TestMethod]
        public void BreathFirstSearchWorksCorrectly()
        {
            //Arrange
            var tree = new BinaryTree<int>();
            var expected = new List<int>() { 5, 12, 2, 21, 9, 3, -4, 25, 19 };

            //Act
            tree.Add(5);
            tree.Add(2);
            tree.Add(-4);
            tree.Add(3);
            tree.Add(12);
            tree.Add(9);
            tree.Add(21);
            tree.Add(19);
            tree.Add(25);

            var result = tree.BreathFirstSearch();
            //Assert
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
