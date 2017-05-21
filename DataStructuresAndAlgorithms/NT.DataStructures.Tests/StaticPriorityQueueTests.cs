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
    }
}
