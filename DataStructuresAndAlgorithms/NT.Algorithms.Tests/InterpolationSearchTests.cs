using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NT.Algorithms.Tests
{
    [TestClass]
    public class InterpolationSearchTests
    {
        [TestMethod]
        public void InterpolationSearchWorksCorrectly()
        {
            //Arrange
            int[] array = new int[] { -50, 3, 7, -125, 4, 0, -22, -178, 99, 120, -33, 9, 5, 2, 6, 1, 8, 10, 55, 110, 12, 34, 66 };
            Array.Sort(array);
            int numberToFind = 10;
            int position = 15;
            var search = new InterpolationSearch();

            //Act
            int result = search.InterpolationArraySearch(array, numberToFind);

            //Assert
            Assert.AreEqual(position, result);
        }
    }
}
