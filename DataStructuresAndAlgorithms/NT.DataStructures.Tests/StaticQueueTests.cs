using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NT.DataStructures.Tests
{
    [TestClass]
    public class StaticQueueTests
    {
        [TestMethod]
        public void CreateEmptyQueueWorksCorrectly()
        {
            //Arrange
            var queue = new StaticQueue<string>();
            int expected = 0;

            //Act
            int count = queue.Count;

            //Assert
            Assert.AreEqual(expected, count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateQueueWithNegativeCapacityThrowsException()
        {
            //Arrange
            var queue = new StaticQueue<string>(-2);

            //Act
            //Assert
        }

        [TestMethod]
        public void CreateQueueFromCollectionWorksCorrectly()
        {
            //Arrange
            var collection = new string[] { "one", "two", "three", "four" };
            var queue = new StaticQueue<string>(collection);

            //Act
            var array = queue.ToArray();

            //Assert
            CollectionAssert.AreEqual(collection, array);
        }

        [TestMethod]
        public void QueueToArrayWorksCorrectly()
        {
            //Arrange
            var collection = new string[] { "one", "two", "three", "four" };
            var queue = new StaticQueue<string>(4);

            //Act
            queue.Enqueue("one");
            queue.Enqueue("two");
            queue.Enqueue("three");
            queue.Enqueue("four");

            var array = queue.ToArray();

            //Assert
            CollectionAssert.AreEqual(collection, array);
        }

        [TestMethod]
        public void QueuePeekWorksCorrectly()
        {
            //Arrange
            var queue = new StaticQueue<string>(4);
            string expected = "one";

            //Act
            queue.Enqueue("one");
            queue.Enqueue("two");

            var result = queue.Peek();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void QueueEnqueueWorksCorrectly()
        {
            //Arrange
            var queue = new StaticQueue<string>(4);
            string expected = "one";

            //Act
            queue.Enqueue("one");

            //Assert
            Assert.AreEqual(expected, queue.Peek());
        }

        [TestMethod]
        public void QueueDequeueWorksCorrectly()
        {
            //Arrange
            var queue = new StaticQueue<string>(4);
            string expected = "one";

            //Act
            queue.Enqueue("one");
            queue.Enqueue("two");
            queue.Enqueue("three");
            queue.Enqueue("four");

            string result = queue.Dequeue();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void QueueContainsWorksCorrectly()
        {
            //Arrange
            var queue = new StaticQueue<string>(4);

            //Act
            queue.Enqueue("one");
            queue.Enqueue("two");
            queue.Enqueue("three");
            queue.Enqueue("four");
            var contains = queue.Contains("two");
            queue.Dequeue();
            var doesNotContain = queue.Contains("one");

            //Assert
            Assert.IsTrue(contains);
            Assert.IsFalse(doesNotContain);
        }

        [TestMethod]
        public void QueueClearWorksCorrectly()
        {
            //Arrange
            var queue = new StaticQueue<string>(4);
            int expected = 0;

            //Act
            queue.Enqueue("one");
            queue.Enqueue("two");
            queue.Enqueue("three");
            queue.Enqueue("four");
            queue.Clear();
            int count = queue.Count;

            //Assert
            Assert.AreEqual(expected, count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PeekOnEmptyQueueThrowsException()
        {
            //Arrange
            var queue = new StaticQueue<string>(4);

            //Act
            queue.Peek();

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DequeueOnEmptyQueueThrowsException()
        {
            //Arrange
            var queue = new StaticQueue<string>(4);

            //Act
            queue.Dequeue();

            //Assert
        }

        [TestMethod]
        public void QueueEnqueueNullWorksCorrectly()
        {
            //Arrange
            var queue = new StaticQueue<string>(4);
            var collection = new string[] { null , "one", "two", "three", "four" };
            string input = null;

            //Act
            queue.Enqueue(input);
            queue.Enqueue("one");
            queue.Enqueue("two");
            queue.Enqueue("three");
            queue.Enqueue("four");

            var array = queue.ToArray();

            //Assert
            CollectionAssert.AreEqual(collection, array);
        }

        [TestMethod]
        public void QueuePeekNullWorksCorrectly()
        {
            //Arrange
            var queue = new StaticQueue<string>(4);
            var collection = new string[] { null, "one", "two", "three", "four" };
            string input = null;

            //Act
            queue.Enqueue(input);
            queue.Enqueue("one");
            queue.Enqueue("two");
            queue.Enqueue("three");
            queue.Enqueue("four");

            var result = queue.Peek();

            //Assert
            Assert.AreEqual(input, result);
        }

        [TestMethod]
        public void QueueEnumarateWorksCorrectly()
        {
            //Arrange
            var expected = new List<string> { "one", "two", "three", "four" };
            var queue = new StaticQueue<string>(4);
            var result = new List<string>();

            //Act
            queue.Enqueue("one");
            queue.Enqueue("two");
            queue.Enqueue("three");
            queue.Enqueue("four");

            foreach (var item in queue)
            {
                result.Add(item);
            }

            //Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EnumarateEmptyQueueDoesntEnumerate()
        {
            //Arrange
            var queue = new StaticQueue<string>(4);
            bool enumerate = false;

            //Act
            foreach (var item in queue)
            {
                enumerate = true;
            }

            //Assert
            Assert.IsFalse(enumerate);
        }
    }
}
