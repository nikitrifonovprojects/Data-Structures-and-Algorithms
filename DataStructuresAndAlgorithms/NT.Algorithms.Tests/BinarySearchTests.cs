using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NT.Algorithms.Tests
{
    [TestClass]
    public class BinarySearchTests
    {
        [TestMethod]
        public void BinarySearchWorksCorrectly()
        {
            //Arrange
            int[] array = new int[] { -50, 3, 7, -125, 4, 0, -22, -178, 99, 120, -33, 9, 5, 2, 6, 1, 8, 10, 55, 110, 12, 34, 66 };
            Array.Sort(array);
            int numberToFind = 8;
            int position = 13;

            var search = new BinarySearch();

            //Act
            int result = search.BinarySearchArray(array, numberToFind);
            //Assert
            Assert.AreEqual(position, result);
        }

        [TestMethod]
        public void BinarySearchRecursiveWorksCorrectly()
        {
            //Arrange
            int[] array = new int[] { -50, 3, 7, -125, 4, 0, -22, -178, 99, 120, -33, 9, 5, 2, 6, 1, 8, 10, 55, 110, 12, 34, 66 };
            Array.Sort(array);
            int numberToFind = 9;
            int position = 14;

            var search = new BinarySearch();

            //Act
            int result = search.BinarySearchRecursive(array, numberToFind);
            //Assert
            Assert.AreEqual(position, result);
        }
    }
}
