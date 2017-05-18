using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NT.DataStructures.Tests
{
    [TestClass]
    public class StaticListTests
    {
        [TestMethod]
        public void CreateEmptyStaticList()
        {
            //Arrange
            var list = new StaticList<int>();
            int expectedCount = 0;

            //Act
            int count = list.Count;

            //Assert
            Assert.AreEqual(expectedCount, count);
        }

        [TestMethod]
        public void StaticListAddsCorrectly()
        {
            //Arrange
            var list = new StaticList<int>();

            //Act
            list.Add(1);
            list.Add(2);
            var array = list.ToArray();

            //Assert
            CollectionAssert.Contains(array, 1);
            CollectionAssert.Contains(array, 2);
        }

        [TestMethod]
        public void CreateStaticListFromCollection()
        {
            //Arrange
            var collection = new int[] { 1, 2, 3, 4 };

            //Act
            var list = new StaticList<int>(collection);
            var result = list.ToArray();

            //Assert
            CollectionAssert.AreEqual(collection, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateStaticListFromNullCollectionThrowsException()
        {
            //Arrange
            int[] collection = default(int[]);

            //Act
            var list = new StaticList<int>(collection);

            //Assert
        }

        [TestMethod]
        public void StaticListCountsCorrectly()
        {
            //Arrange
            var list = new StaticList<int>();
            var expectedCount = 2;
            //Act
            list.Add(1);
            list.Add(2);
            int count = list.Count();

            //Assert
            Assert.AreEqual(expectedCount, count);
        }

        [TestMethod]
        public void StaticListChangeValueWithBracketsWorksCorrectly()
        {
            //Arrange
            var list = new StaticList<int>(5);
            var expected = new int[] { 2, 3 };
            list.Add(8);
            list.Add(7);

            //Act
            list[0] = 1;
            list[1] = 3;
            list[0] = 2;
            var result = list.ToArray();

            //Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void StaticListClearsCorrectly()
        {
            //Arrange
            var list = new StaticList<int>();
            var expectedCount = 0;
            //Act
            list.Add(1);
            list.Add(2);
            list.Clear();
            int count = list.Count();

            //Assert
            Assert.AreEqual(expectedCount, count);
        }

        [TestMethod]
        public void StaticListContainsItemCorrectly()
        {
            //Arrange
            var list = new StaticList<int>();

            //Act
            list.Add(1);
            list.Add(2);
            list.Add(88);
            list.Add(64);
            list.Add(54);
            list.Add(200);
            list.Add(132);
            bool contains = list.Contains(54);
            bool doesNotContain = list.Contains(53);

            //Assert
            Assert.IsTrue(contains);
            Assert.IsFalse(doesNotContain);
        }

        [TestMethod]
        public void StaticListCopyToWorksCorrectly()
        {
            //Arrange
            var list = new StaticList<int>();
            var expected = new int[] { 8, 7, 11, 3, 1, 6 };
            var result = new int[6];
            //Act
            list.Add(8);
            list.Add(7);
            list.Add(11);
            list.Add(3);
            list.Add(1);
            list.Add(6);

            list.CopyTo(result, 0);

            //Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void StaticListIndexOfWorksCorrectly()
        {
            //Arrange
            var list = new StaticList<int>();
            int expected = 2;
            //Act
            list.Add(1);
            list.Add(2);
            list.Add(88);
            list.Add(64);
            list.Add(54);
            list.Add(200);
            list.Add(132);

            int result = list.IndexOf(88);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void StaticListRemoveAtWorksCorrectly()
        {
            //Arrange
            var list = new StaticList<int>();
            var expected = new int[] { 8, 7, 3, 1, 6 };
            
            //Act
            list.Add(8);
            list.Add(7);
            list.Add(11);
            list.Add(3);
            list.Add(1);
            list.Add(6);

            list.RemoveAt(2);
            var result = list.ToArray();

            //Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void StaticListRemoveWorksCorrectly()
        {
            //Arrange
            var list = new StaticList<int>();
            var expected = new int[] { 8, 7, 3, 1, 6 };

            //Act
            list.Add(8);
            list.Add(7);
            list.Add(11);
            list.Add(3);
            list.Add(1);
            list.Add(6);

            list.Remove(11);
            var result = list.ToArray();

            //Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void StaticListRemoveAtIllegalIndexThrowsException()
        {
            //Arrange
            var list = new StaticList<int>();
            var expected = new int[] { 8, 7, 3, 1, 6 };

            //Act
            list.Add(8);
            list.Add(7);
            list.Add(11);
            list.Add(3);
            list.Add(1);
            list.Add(6);

            list.RemoveAt(11);
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void StaticListChangeValueWithBracketsIllegalIndexThrowsException()
        {
            //Arrange
            var list = new StaticList<int>();
            var expected = new int[] { 2, 3 };

            //Act
            list.Add(8);
            list.Add(7);
            list[8] = 4;
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void StaticListGetalueWithBracketsIllegalIndexThrowsException()
        {
            //Arrange
            var list = new StaticList<int>();
            var expected = new int[] { 2, 3 };

            //Act
            list.Add(8);
            list.Add(7);
            var result = list[14];
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateStaticListWithSizeLessThanZeroThrowsException()
        {
            //Arrange
            var list = new StaticList<int>(-1);

            //Act
            //Assert
        }
    }
}
