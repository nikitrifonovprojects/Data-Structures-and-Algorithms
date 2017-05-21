using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NT.Algorithms.Tests
{
    [TestClass]
    public class InsertionSortTests
    {
        [TestMethod]
        public void NormalSortingWorksCorrectly()
        {
            //Arrange
            int[] array = new int[] { -50, 3, 3, 7, -125, 4, 0, -22, -22, -178, 99, 120, -33, 9, 5, 2, 6, 1, 1, 8, 10, 55, 110, 12, 34, 66, 66 };
            int[] otherArray = new int[] { -50, 3, 3, 7, -125, 4, 0, -22, -22, -178, 99, 120, -33, 9, 5, 2, 6, 1, 1, 8, 10, 55, 110, 12, 34, 66, 66 };
            var sort = new InsertionSort<int>();

            //Act
            sort.Insertion(array);
            Array.Sort(otherArray);

            //Assert
            CollectionAssert.AreEqual(otherArray, array);
        }

        [TestMethod]
        public void RecursiveSortingWorksCorrectly()
        {
            //Arrange
            int[] array = new int[] { -50, 3, 3, 7, -125, 4, 0, -22, -22, -178, 99, 120, -33, 9, 5, 2, 6, 1, 1, 8, 10, 55, 110, 12, 34, 66, 66 };
            int[] otherArray = new int[] { -50, 3, 3, 7, -125, 4, 0, -22, -22, -178, 99, 120, -33, 9, 5, 2, 6, 1, 1, 8, 10, 55, 110, 12, 34, 66, 66 };
            var sort = new InsertionSort<int>();

            //Act
            sort.InsertionRecursive(array, array.Length - 1);
            Array.Sort(otherArray);

            //Assert
            CollectionAssert.AreEqual(otherArray, array);
        }

        [TestMethod]
        public void ListSortingWorksCorrectly()
        {
            //Arrange
            List<int> list = new List<int> { -50, 3, 3, 7, -125, 4, 0, -22, -22, -178, 99, 120, -33, 9, 5, 2, 6, 1, 1, 8, 10, 55, 110, 12, 34, 66, 66 };
            int[] otherArray = new int[] { -50, 3, 3, 7, -125, 4, 0, -22, -22, -178, 99, 120, -33, 9, 5, 2, 6, 1, 1, 8, 10, 55, 110, 12, 34, 66, 66 };
            var sort = new InsertionSort<int>();

            //Act
            sort.InsertionSortList(list);
            Array.Sort(otherArray);

            //Assert
            CollectionAssert.AreEqual(otherArray, list);
        }
    }
}
