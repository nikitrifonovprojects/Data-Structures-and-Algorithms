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

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateLinkedListFromNullCollectionThrowsException()
        {
            //Arrange
            List<string> inputList = default(List<string>); 
            var list = new LinkedList<string>(inputList);

            //Act
            //Assert
        }

        [TestMethod]
        public void LinkedListAddWorksCorrectly()
        {
            //Arrange
            List<string> inputList = new List<string>() { "one", "two", "three"};
            var list = new LinkedList<string>();

            //Act
            list.Add("one");
            list.Add("two");
            list.Add("three");
            var result = list.ToList();

            //Assert
            CollectionAssert.AreEquivalent(inputList, result);
        }

        [TestMethod]
        public void LinkedListCountsCorrectly()
        {
            //Arrange
            var list = new LinkedList<string>();
            int expected = 2;

            //Act
            list.Add("two");
            list.Add("three");
            int count = list.Count;

            //Assert
            Assert.AreEqual(expected, count);
        }

        [TestMethod]
        public void LinkedRemoveFirstWorksCorrectly()
        {
            //Arrange
            List<string> inputList = new List<string>() {"two", "three" };
            var list = new LinkedList<string>();

            //Act
            list.Add("one");
            list.Add("two");
            list.Add("three");
            list.RemoveFirst();
            var result = list.ToList();

            //Assert
            CollectionAssert.AreEqual(inputList, result);
        }

        [TestMethod]
        public void LinkedRemoveLastWorksCorrectly()
        {
            //Arrange
            List<string> inputList = new List<string>() { "one", "two" };
            var list = new LinkedList<string>();

            //Act
            list.Add("one");
            list.Add("two");
            list.Add("three");
            list.RemoveLast();
            var result = list.ToList();

            //Assert
            CollectionAssert.AreEqual(inputList, result);
        }

        [TestMethod]
        public void LinkedListAddFirstCorrectly()
        {
            //Arrange
            List<string> inputList = new List<string>() { "four", "one", "two", "three" };
            var list = new LinkedList<string>();
            
            //Act
            list.Add("one");
            list.Add("two");
            list.Add("three");
            list.AddFirst("four");
            var result = list.ToList();

            //Assert
            CollectionAssert.AreEqual(inputList, result);
        }

        [TestMethod]
        public void LinkedListFindCorrectly()
        {
            //Arrange
            var list = new LinkedList<string>();
            var expected = "two";

            //Act
            list.Add("one");
            list.Add("two");
            list.Add("three");
            var result = list.Find("two");

            //Assert
            Assert.AreEqual(expected, result.Content);
        }

        [TestMethod]
        public void LinkedListRemovesCorrectly()
        {
            //Arrange
            List<string> inputList = new List<string>() { "one", "three" };
            var list = new LinkedList<string>();

            //Act
            list.Add("one");
            list.Add("two");
            list.Add("three");
            list.Remove("two");
            var result = list.ToList();

            //Assert
            CollectionAssert.AreEqual(inputList, result);
        }

        [TestMethod]
        public void LinkedListContainsCorrectly()
        {
            //Arrange
            var list = new LinkedList<string>();

            //Act
            list.Add("one");
            list.Add("two");
            list.Add("three");
            var found = list.Contains("two");
            list.Remove("two");
            var notFound = list.Contains("two");

            //Assert
            Assert.IsTrue(found);
            Assert.IsFalse(notFound);
        }

        [TestMethod]
        public void LinkedListClersCorrectly()
        {
            //Arrange
            var list = new LinkedList<string>();
            int expected = 0;

            //Act
            list.Add("one");
            list.Add("two");
            list.Add("three");
            list.Clear();
            int result = list.Count();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinkedListCopyToCorrectly()
        {
            //Arrange
            string[] expected = new string[] { "one", "two", "three" };
            var list = new LinkedList<string>();
            string[] result = new string[3];

            //Act
            list.Add("one");
            list.Add("two");
            list.Add("three");
            list.CopyTo(result, 0);

            //Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinkedListEnumeratesCorrectly()
        {
            //Arrange
            List<string> expected = new List<string>() { "one", "two", "three" };
            var list = new LinkedList<string>();
            List<string> result = new List<string>();

            //Act
            list.Add("one");
            list.Add("two");
            list.Add("three");

            foreach (var item in list)
            {
                result.Add(item);
            }

            //Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinkedListDoesntEnumerateEmptyList()
        {
            //Arrange
            var list = new LinkedList<string>();
            bool result = false;

            //Act
            list.Add("one");
            list.Add("two");
            list.Add("three");
            list.Clear();

            foreach (var item in list)
            {
                result = true;
            }

            //Assert
            Assert.IsFalse(result);
        }
    }
}
