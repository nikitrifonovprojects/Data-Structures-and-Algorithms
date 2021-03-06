﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NT.Algorithms.Tests
{
    [TestClass]
    public class MergeSortTests
    {
        [TestMethod]
        public void MergeSortSortsCorrectly()
        {
            //Arrange
            int[] array = new int[] { -50, 3, 3, 7, -125, 4, 0, -22, -22, -178, 99, 120, -33, 9, 5, 2, 6, 1, 1, 8, 10, 55, 110, 12, 34, 66, 66 };
            int[] otherArray = new int[] { -50, 3, 3, 7, -125, 4, 0, -22, -22, -178, 99, 120, -33, 9, 5, 2, 6, 1, 1, 8, 10, 55, 110, 12, 34, 66, 66 };
            var sort = new MergeSort<int>();
            int[] workArray = new int[array.Length];

            //Act
            sort.MergeSortArray(array);
            Array.Sort(otherArray);

            //Assert
            CollectionAssert.AreEqual(otherArray, array);
        }
    }
}
