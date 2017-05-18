﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NT.DataStructures.Tests
{
    [TestClass]
    public class HashTests
    {
        [TestMethod]
        public void CrateEmptyHashCorrectly()
        {
            //Arrange
            var hash = new Hash<int>();
            var defaultCount = 0;

            //Act
            var count = hash.Count;

            //Assert
            Assert.AreEqual(defaultCount, count);
        }

        [TestMethod]
        public void HashRemovesCorrectly()
        {
            //Arrange
            var hash = new Hash<int>();
            hash.Add(1);
            hash.Add(2);
            hash.Add(5);
            hash.Add(8);
            hash.Add(12);
            hash.Add(66);
            hash.Add(99);
            hash.Remove(12);
            hash.Remove(1);
            var expectedArry = new int[] { 2, 5, 8, 66, 99 };
            var actualArray = new int[expectedArry.Length];

            //Act
            hash.CopyTo(actualArray, 0);

            //Assert
            CollectionAssert.AreEquivalent(expectedArry, actualArray);
        }

        [TestMethod]
        public void HashAddsCorrectly()
        {
            //Arrange
            var hash = new Hash<int>();
            hash.Add(1);
            hash.Add(2);
            hash.Add(5);
            hash.Add(8);
            hash.Add(12);
            hash.Add(66);
            hash.Add(99);
            var expectedArry = new int[] { 1, 2, 5, 8, 12, 66, 99 };
            var actualArray = new int[expectedArry.Length];

            //Act
            hash.CopyTo(actualArray, 0);

            //Assert
            CollectionAssert.AreEquivalent(expectedArry, actualArray);
        }

        [TestMethod]
        public void HashCopyToCopiesCorrectly()
        {
            //Arrange
            var hash = new Hash<int>();
            hash.Add(1);
            hash.Add(2);
            hash.Add(5);
            hash.Add(8);
            hash.Add(12);
            hash.Add(66);
            hash.Add(99);
            var expectedArry = new int[] { 1, 2, 5, 8, 12, 66, 99 };

            //Act
            var actualArray = new int[expectedArry.Length];
            hash.CopyTo(actualArray, 0);

            //Assert
            CollectionAssert.AreEquivalent(expectedArry, actualArray);
        }

        [TestMethod]
        public void CrateHashFromCollectionCorrectly()
        {
            //Arrange
            var list = new List<int>() { 1, 2, 5, 8, 12, 66, 99, 8, 2, 66 };
            var hash = new Hash<int>(list);
            var expectedArry = new int[] { 1, 2, 5, 8, 12, 66, 99 };

            //Act
            var actualArray = new int[expectedArry.Length];
            hash.CopyTo(actualArray, 0);

            //Assert
            CollectionAssert.AreEquivalent(expectedArry, actualArray);
        }

        [TestMethod]
        public void HashClearsCollectionCorrectly()
        {
            //Arrange
            var list = new List<int>() { 1, 2, 5, 8, 12, 66, 99, 8 };
            var hash = new Hash<int>(list);
            var expectedCount = 0;

            //Act
            hash.Clear();

            //Assert
            Assert.AreEqual(expectedCount, hash.Count);
        }

        [TestMethod]
        public void HashContainsCorrectly()
        {
            //Arrange
            var list = new List<int>() { 1, 2, 5, 8, 12, 66, 99, 8 };
            var hash = new Hash<int>(list);

            //Act
            bool contains = hash.Contains(66);

            //Assert
            Assert.IsTrue(contains);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CrateHashFromNullCollectionThrowsException()
        {
            //Arrange
            var list = default(List<int>);
            var hash = new Hash<int>(list);
            
            //Act
            //Assert
        }

        [TestMethod]
        public void HashEnumeratesCorrectly()
        {
            //Arrange
            var hash = new Hash<int>();
            hash.Add(1);
            hash.Add(2);
            hash.Add(5);
            hash.Add(8);
            hash.Add(12);
            hash.Add(66);
            hash.Add(99);
            hash.Remove(12);
            hash.Remove(1);
            hash.Add(102);
            hash.Add(9911);
            hash.Add(321);

            var expectedArry = new int[] { 2, 5, 8, 66, 99, 102, 9911, 321 };

            //Act
            var actualArray = hash.AsEnumerable<int>().ToList();

            //Assert
            CollectionAssert.AreEquivalent(expectedArry, actualArray);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HashCopyToSmallerArrayThrowsException()
        {
            //Arrange
            var hash = new Hash<int>();
            hash.Add(1);
            hash.Add(2);
            hash.Add(5);
            hash.Add(8);

            //Act
            hash.CopyTo(new int[hash.Count - 1], 0);

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HashCopyNullArrayThrowsException()
        {
            //Arrange
            var hash = new Hash<int>();
            hash.Add(1);
            hash.Add(2);
            hash.Add(5);
            hash.Add(8);

            //Act
            hash.CopyTo(default(int[]), 0);

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HashCopyArrayWithArrayIndexLessThanZeroThrowsException()
        {
            //Arrange
            var hash = new Hash<int>();
            hash.Add(1);
            hash.Add(2);
            hash.Add(5);
            hash.Add(8);

            //Act
            hash.CopyTo(new int[hash.Count], -1);

            //Assert
        }

        [TestMethod]
        public void EnumeratorDoesntEnumerateEmptyHash()
        {
            //Arrange
            var hash = new Hash<int>();
            bool enumerate = false;
            int count = 0;

            //Act
            foreach (var item in hash)
            {
                count++;
                if (count > 0)
                {
                    enumerate = true;
                }
            }

            //Assert
            Assert.IsFalse(enumerate);
        }
    }
}
