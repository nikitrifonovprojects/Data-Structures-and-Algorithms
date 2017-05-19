using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NT.DataStructures.Tests
{
    [TestClass]
    public class LinkedListTests
    {
        [TestMethod]
        public void CreateEmptyLinkedListCorrectly()
        {
            //Arrange
            var list = new LinkedList<string>();
            int expected = 0;

            //Act
            int count = list.Count;

            //Assert
            Assert.AreEqual(expected, count);
        }

        [TestMethod]
        public void CreateLinkedListFromCollectionCorrectly()
        {
            //Arrange
            List<string> inputList = new List<string>() { "one", "two", "three", "four", "five" };
            var list = new LinkedList<string>(inputList);
            int expected = 5;

            //Act
            int count = list.Count;
            var result = list.ToList();

            //Assert
            Assert.AreEqual(expected, count);
            CollectionAssert.AreEquivalent(inputList, result);
        }
    }
}
