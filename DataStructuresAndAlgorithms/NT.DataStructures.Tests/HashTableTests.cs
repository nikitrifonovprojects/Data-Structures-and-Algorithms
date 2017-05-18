using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NT.DataStructures.Tests
{
    [TestClass]
    public class HashTableTests
    {
        [TestMethod]
        public void HashTableAddsKeyAndValueCorrectly()
        {
            //Arrange
            var dictionary = new HashTable<int, string>();
            var expectedArray = new KeyValuePair<int, string>[]
            {
               new KeyValuePair<int, string>(1, "Item no'1"),
               new KeyValuePair<int, string>(2, "Item no'2"),
               new KeyValuePair<int, string>(3, "Item no'3"),
               new KeyValuePair<int, string>(4, "Item no'4"),
               new KeyValuePair<int, string>(5, "item no'5")
            };

            //Act
            dictionary.Add(1, "Item no'1");
            dictionary.Add(2, "Item no'2");
            dictionary.Add(3, "Item no'3");
            dictionary.Add(4, "Item no'4");
            dictionary.Add(5, "item no'5");

            var array = dictionary.ToArray();

            //Assert
            CollectionAssert.AreEquivalent(expectedArray, array);
        }

        [TestMethod]
        public void HashTableAddsKeyValueItemCorrectly()
        {
            //Arrange
            var dictionary = new HashTable<int, string>();
            var expectedArray = new KeyValuePair<int, string>[]
            {
               new KeyValuePair<int, string>(1, "Item no'1"),
               new KeyValuePair<int, string>(2, "Item no'2"),
               new KeyValuePair<int, string>(3, "Item no'3"),
               new KeyValuePair<int, string>(4, "Item no'4"),
               new KeyValuePair<int, string>(5, "item no'5")
            };

            var itemOne = new KeyValuePair<int, string>(1, "Item no'1");
            var itemTwo = new KeyValuePair<int, string>(2, "Item no'2");
            var itemThree = new KeyValuePair<int, string>(3, "Item no'3");
            var itemFour = new KeyValuePair<int, string>(4, "Item no'4");
            var itemFive = new KeyValuePair<int, string>(5, "item no'5");

            //Act
            dictionary.Add(itemOne);
            dictionary.Add(itemTwo);
            dictionary.Add(itemThree);
            dictionary.Add(itemFour);
            dictionary.Add(itemFive);

            var array = dictionary.ToArray();

            //Assert
            CollectionAssert.AreEquivalent(expectedArray, array);
        }

        [TestMethod]
        public void HashTableRemovesKeyValueItemCorrectly()
        {
            //Arrange
            var dictionary = new HashTable<int, string>();
            var expectedArray = new KeyValuePair<int, string>[]
            {
               new KeyValuePair<int, string>(1, "Item no'1"),
               new KeyValuePair<int, string>(3, "Item no'3"),
               new KeyValuePair<int, string>(4, "Item no'4"),
               new KeyValuePair<int, string>(6, "Item no'6")
            };

            var itemOne = new KeyValuePair<int, string>(1, "Item no'1");
            var itemTwo = new KeyValuePair<int, string>(2, "Item no'2");
            var itemThree = new KeyValuePair<int, string>(3, "Item no'3");
            var itemFour = new KeyValuePair<int, string>(4, "Item no'4");
            var itemFive = new KeyValuePair<int, string>(5, "item no'5");

            //Act
            dictionary.Add(itemOne);
            dictionary.Add(itemTwo);
            dictionary.Add(itemThree);
            dictionary.Add(itemFour);
            dictionary.Add(itemFive);

            dictionary.Remove(itemTwo);
            dictionary.Remove(itemFive);

            dictionary.Add(6, "Item no'6");

            var array = dictionary.ToArray();

            //Assert
            CollectionAssert.AreEquivalent(expectedArray, array);
        }

        [TestMethod]
        public void HashTableRemovesByKeyCorrectly()
        {
            //Arrange
            var dictionary = new HashTable<int, string>();
            var expectedArray = new KeyValuePair<int, string>[]
            {
               new KeyValuePair<int, string>(3, "Item no'3"),
               new KeyValuePair<int, string>(4, "Item no'4"),
               new KeyValuePair<int, string>(5, "item no'5")
            };

            //Act
            dictionary.Add(1, "Item no'1");
            dictionary.Add(2, "Item no'2");
            dictionary.Add(3, "Item no'3");
            dictionary.Add(4, "Item no'4");
            dictionary.Add(5, "item no'5");

            dictionary.Remove(2);
            dictionary.Remove(1);
            var array = dictionary.ToArray();

            //Assert
            CollectionAssert.AreEquivalent(expectedArray, array);
        }

        [TestMethod]
        public void HashTableCountsCorrectly()
        {
            //Arrange
            var dictionary = new HashTable<int, string>();
            var expectedArray = new KeyValuePair<int, string>[]
            {
               new KeyValuePair<int, string>(3, "Item no'3"),
               new KeyValuePair<int, string>(4, "Item no'4"),
               new KeyValuePair<int, string>(5, "item no'5")
            };
            var expectedCount = expectedArray.Length;

            //Act
            dictionary.Add(1, "Item no'1");
            dictionary.Add(2, "Item no'2");
            dictionary.Add(3, "Item no'3");

            var count = dictionary.Count;

            //Assert
            Assert.AreEqual(expectedCount, count);
        }

        [TestMethod]
        public void HashTableContainsByItemCorrectly()
        {
            //Arrange
            var dictionary = new HashTable<int, string>();
            var item = new KeyValuePair<int, string>(3, "Item no'3");

            //Act
            dictionary.Add(1, "Item no'1");
            dictionary.Add(2, "Item no'2");
            dictionary.Add(3, "Item no'3");
            dictionary.Add(4, "Item no'4");
            dictionary.Add(5, "item no'5");

            var containsItem = dictionary.Contains(item);

            //Assert
            Assert.IsTrue(containsItem);
        }

        [TestMethod]
        public void HashTableContainsByKeyCorrectly()
        {
            //Arrange
            var dictionary = new HashTable<int, string>();

            //Act
            dictionary.Add(1, "Item no'1");
            dictionary.Add(2, "Item no'2");
            dictionary.Add(3, "Item no'3");
            dictionary.Add(4, "Item no'4");
            dictionary.Add(5, "item no'5");

            var containsItem = dictionary.ContainsKey(2);

            //Assert
            Assert.IsTrue(containsItem);
        }

        [TestMethod]
        public void HashTableContainsByValueCorrectly()
        {
            //Arrange
            var dictionary = new HashTable<int, string>();

            //Act
            dictionary.Add(1, "Item no'1");
            dictionary.Add(2, "Item no'2");
            dictionary.Add(3, "Item no'3");
            dictionary.Add(4, "Item no'4");
            dictionary.Add(5, "item no'5");

            var containsItem = dictionary.ContainsValue("Item no'4");

            //Assert
            Assert.IsTrue(containsItem);
        }

        [TestMethod]
        public void HashTableClearMethodWorksCorrectly()
        {
            //Arrange
            var dictionary = new HashTable<int, string>();
            int expectedCount = 0;
            //Act
            dictionary.Add(1, "Item no'1");
            dictionary.Add(2, "Item no'2");
            dictionary.Add(3, "Item no'3");
            dictionary.Add(4, "Item no'4");
            dictionary.Add(5, "item no'5");

            dictionary.Clear();
            var count = dictionary.Count;

            //Assert
            Assert.AreEqual(expectedCount, count);
        }

        [TestMethod]
        public void EnumeratorDoesntEnumerateEmptyHashTable()
        {
            //Arrange
            var dictionary = new HashTable<int, string>();
            bool enumerate = false;
            int count = 0;

            //Act
            foreach (var item in dictionary)
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

        [TestMethod]
        public void HashTableCopyToWorksCorrectly()
        {
            //Arrange
            var dictionary = new HashTable<int, string>();
            var expectedArray = new KeyValuePair<int, string>[5];

            var itemOne = new KeyValuePair<int, string>(1, "Item no'1");
            var itemTwo = new KeyValuePair<int, string>(2, "Item no'2");
            var itemThree = new KeyValuePair<int, string>(3, "Item no'3");
            var itemFour = new KeyValuePair<int, string>(4, "Item no'4");
            var itemFive = new KeyValuePair<int, string>(5, "item no'5");

            //Act
            dictionary.Add(itemOne);
            dictionary.Add(itemTwo);
            dictionary.Add(itemThree);
            dictionary.Add(itemFour);
            dictionary.Add(itemFive);

            dictionary.CopyTo(expectedArray, 0);
            var array = dictionary.ToArray();

            //Assert
            CollectionAssert.AreEquivalent(expectedArray, array);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HashTableCopyToWithNegativeIndexThrowsException()
        {
            //Arrange
            var dictionary = new HashTable<int, string>();
            var expectedArray = new KeyValuePair<int, string>[10];

            var itemOne = new KeyValuePair<int, string>(1, "Item no'1");
            var itemTwo = new KeyValuePair<int, string>(2, "Item no'2");
            var itemThree = new KeyValuePair<int, string>(3, "Item no'3");
            var itemFour = new KeyValuePair<int, string>(4, "Item no'4");
            var itemFive = new KeyValuePair<int, string>(5, "item no'5");

            //Act
            dictionary.Add(itemOne);
            dictionary.Add(itemTwo);
            dictionary.Add(itemThree);
            dictionary.Add(itemFour);
            dictionary.Add(itemFive);

            dictionary.CopyTo(expectedArray, -1);

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HashTableCopyToWithNullArrayThrowsException()
        {
            //Arrange
            var dictionary = new HashTable<int, string>();
            var expectedArray = default(KeyValuePair<int, string>[]);

            var itemOne = new KeyValuePair<int, string>(1, "Item no'1");
            var itemTwo = new KeyValuePair<int, string>(2, "Item no'2");
            var itemThree = new KeyValuePair<int, string>(3, "Item no'3");
            var itemFour = new KeyValuePair<int, string>(4, "Item no'4");
            var itemFive = new KeyValuePair<int, string>(5, "item no'5");

            //Act
            dictionary.Add(itemOne);
            dictionary.Add(itemTwo);
            dictionary.Add(itemThree);
            dictionary.Add(itemFour);
            dictionary.Add(itemFive);

            dictionary.CopyTo(expectedArray, 0);

            //Assert
        }

        [TestMethod]
        public void HashTableTryGetValueWorksCorrectly()
        {
            //Arrange
            var dictionary = new HashTable<int, string>();
         
            var itemOne = new KeyValuePair<int, string>(1, "Item no'1");
            var itemTwo = new KeyValuePair<int, string>(2, "Item no'2");
            var itemThree = new KeyValuePair<int, string>(3, "Item no'3");
            var itemFour = new KeyValuePair<int, string>(4, "Item no'4");
            var itemFive = new KeyValuePair<int, string>(5, "item no'5");

            string foundResult = string.Empty;
            string expectedFoundResult = "Item no'2";
            string notFoundResult = string.Empty;
            string expectedNotFoundResult = null;

            //Act
            dictionary.Add(itemOne);
            dictionary.Add(itemTwo);
            dictionary.Add(itemThree);
            dictionary.Add(itemFour);
            dictionary.Add(itemFive);
            
            bool found = dictionary.TryGetValue(2, out foundResult);
            bool notFound = dictionary.TryGetValue(6, out notFoundResult);

            //Assert
            Assert.IsTrue(found);
            Assert.IsFalse(notFound);
            Assert.AreEqual(expectedFoundResult, foundResult);
            Assert.AreEqual(expectedNotFoundResult, notFoundResult);

        }
    }
}
