using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NT.DataStructures.Tests
{
    [TestClass]
    public class StaticPriorityQueueTests
    {
        [TestMethod]
        public void CreateEmptyQueueWorksCorrectly()
        {
            //Arrange
            var queue = new StaticPriorityQueue<string>();
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
            var queue = new StaticPriorityQueue<string>(-2);

            //Act
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void QueuePeekWithNoItemsInQueueThrowsException()
        {
            //Arrange
            var queue = new StaticPriorityQueue<string>();

            //Act
            queue.Peek();
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void QueueEnqueueWithNegativePriorityThrowsException()
        {
            //Arrange
            var queue = new StaticPriorityQueue<string>();

            //Act
            queue.Enqueue(-1, "add");
            //Assert
        }

        [TestMethod]
        public void QueueToArrayWorksCorrectly()
        {
            //Arrange
            var queue = new StaticPriorityQueue<string>();
            var expected = new string[] { "add '1", "add '2", "add '3", "add '4", "add '5", "add '6", "add '7" };

            //Act
            queue.Enqueue(1, "add '1");
            queue.Enqueue(0, "add '2");
            queue.Enqueue(0, "add '3");
            queue.Enqueue(2, "add '4");
            queue.Enqueue(3, "add '5");
            queue.Enqueue(7, "add '6");
            queue.Enqueue(9, "add '7");

            var result = queue.ToArray();

            //Assert
            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod]
        public void QueueEnqueueWithPriorityAndValueWorksCorrectly()
        {
            //Arrange
            var queue = new StaticPriorityQueue<string>();
            var expected = new string[] { "add '1", "add '2", "add '3", "add '4" };

            //Act
            queue.Enqueue(1, "add '1");
            queue.Enqueue(0, "add '2");
            queue.Enqueue(0, "add '3");
            queue.Enqueue(2, "add '4");
            var result = queue.ToArray();

            //Assert
            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod]
        public void QueueCountWorksCorrectly()
        {
            //Arrange
            var queue = new StaticPriorityQueue<string>();
            int count = 4;
            //Act
            queue.Enqueue(1, "add '1");
            queue.Enqueue(0, "add '2");
            queue.Enqueue(0, "add '3");
            queue.Enqueue(2, "add '4");
            int result = queue.Count;

            //Assert
            Assert.AreEqual(count, result);
        }

        [TestMethod]
        public void QueueClearWorksCorrectly()
        {
            //Arrange
            var queue = new StaticPriorityQueue<string>();
            int count = 0;
            //Act
            queue.Enqueue(1, "add '1");
            queue.Enqueue(0, "add '2");
            queue.Enqueue(0, "add '3");
            queue.Enqueue(2, "add '4");
            queue.Clear();
            int result = queue.Count;

            //Assert
            Assert.AreEqual(count, result);
        }

        [TestMethod]
        public void QueuePeekCorrectly()
        {
            //Arrange
            var queue = new StaticPriorityQueue<string>();
            var expected = "add '4";

            //Act
            queue.Enqueue(1, "add '1");
            queue.Enqueue(0, "add '2");
            queue.Enqueue(0, "add '3");
            queue.Enqueue(2, "add '4");
            var result = queue.Peek();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void QueueEnqueueWithIEnumerableWorksCorrectly()
        {
            //Arrange
            var queue = new StaticPriorityQueue<string>();
            var expected = new string[] { "add '1", "add '2", "add '3", "add '4" };

            //Act
            queue.Enqueue(expected);
            var result = queue.ToArray();

            //Assert
            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod]
        public void QueueEnqueueWithPriorityAndIEnumerableWorksCorrectly()
        {
            //Arrange
            var queue = new StaticPriorityQueue<string>();
            var input = new string[] { "add '1", "add '2", "add '3", "add '4" };
            var expected = new string[] { "add '2", "add '3", "add '4", "add '5", "add '6" };
            var expectedElement = "add '1";
            var expectedNextElement = "add '2";
            //Act
            queue.Enqueue(2, input);
            queue.Enqueue(1, "add '5");
            queue.Enqueue(1, "add '6");
            var element = queue.Peek();
            queue.Dequeue();
            var nextElement = queue.Peek();
            var result = queue.ToArray();

            //Assert
            CollectionAssert.AreEquivalent(expected, result);
            Assert.AreEqual(expectedElement, element);
            Assert.AreEqual(expectedNextElement, nextElement);

        }

        [TestMethod]
        public void QueueEnqueueValueCorrectly()
        {
            //Arrange
            var queue = new StaticPriorityQueue<string>();
            string expected = "add '4";
            string nextExpected = "add '1";

            //Act
            queue.Enqueue("add '1");
            queue.Enqueue("add '2");
            queue.Enqueue("add '3");
            queue.Enqueue(2, "add '4");
            string result = queue.Peek();
            queue.Dequeue();
            string next = queue.Peek();

            //Assert
            Assert.AreEqual(expected, result);
            Assert.AreEqual(nextExpected, next);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void QueueEnqueueWithNullCollectionThrowsException()
        {
            //Arrange
            var queue = new StaticPriorityQueue<string>();
            var input = default(string[]);
            //Act
            queue.Enqueue(input);
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void QueueEnqueueWithNullCollectionAndPriorityThrowsException()
        {
            //Arrange
            var queue = new StaticPriorityQueue<string>();
            var input = default(string[]);
            //Act
            queue.Enqueue(1, input);
            //Assert
        }

        [TestMethod]
        public void QueueContainsValueCorrectly()
        {
            //Arrange
            var queue = new StaticPriorityQueue<string>();

            //Act
            queue.Enqueue("add '1");
            queue.Enqueue("add '2");
            queue.Enqueue("add '3");
            queue.Enqueue(2, "add '4");
            bool result = queue.Contains("add '4");
            queue.Dequeue();
            bool next = queue.Contains("add '4");

            //Assert
            Assert.IsFalse(next);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void QueueContainsByPriorityAndValueCorrectly()
        {
            //Arrange
            var queue = new StaticPriorityQueue<string>();

            //Act
            queue.Enqueue("add '1");
            queue.Enqueue("add '2");
            queue.Enqueue("add '3");
            queue.Enqueue(2, "add '4");
            bool result = queue.Contains(2, "add '4");
            queue.Dequeue();
            bool next = queue.Contains(2, "add '4");

            //Assert
            Assert.IsFalse(next);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void QueueEnqueueNullCorrectly()
        {
            //Arrange
            var queue = new StaticPriorityQueue<string>();
            string expected = null;

            //Act
            queue.Enqueue("add '1");
            queue.Enqueue("add '2");
            queue.Enqueue("add '3");
            queue.Enqueue(2, expected);
            string result = queue.Dequeue();

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
