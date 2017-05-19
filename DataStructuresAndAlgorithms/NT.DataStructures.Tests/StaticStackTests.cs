using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NT.DataStructures.Tests
{
    [TestClass]
    public class StaticStackTests
    {
        [TestMethod]
        public void CreateEmptyStackWorksCorrectly()
        {
            //Arrange
            var stack = new StaticStack<string>();
            int expected = 0;
            //Act
            int count = stack.Count;

            //Assert
            Assert.AreEqual(expected, count);
        }

        [TestMethod]
        public void StackCountWorksCorrectly()
        {
            //Arrange
            var stack = new StaticStack<string>(5);
            int expected = 5;

            //Act
            stack.Push("one");
            stack.Push("two");
            stack.Push("three");
            stack.Push("four");
            stack.Push("five");
            int count = stack.Count;

            //Assert
            Assert.AreEqual(expected, count);
        }

        [TestMethod]
        public void StackPushWorksCorrectly()
        {
            //Arrange
            var stack = new StaticStack<string>(5);
            var expected = new string[] { "three", "two", "one" };

            //Act
            stack.Push("one");
            stack.Push("two");
            stack.Push("three");
            var result = stack.ToArray();

            //Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void StackPopWorksCorrectly()
        {
            //Arrange
            var stack = new StaticStack<string>(5);
            var expected = "three";

            //Act
            stack.Push("one");
            stack.Push("two");
            stack.Push("three");
            var result = stack.Pop();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void StackPeekWorksCorrectly()
        {
            //Arrange
            var stack = new StaticStack<string>(5);
            var expectedArray = new string[] { "three", "two", "one" };
            var expected = "three";

            //Act
            stack.Push("one");
            stack.Push("two");
            stack.Push("three");
            var result = stack.Peek();
            var resultArray = stack.ToArray();

            //Assert
            Assert.AreEqual(expected, result);
            CollectionAssert.AreEqual(expectedArray, resultArray);
        }

        [TestMethod]
        public void StackContainsWorksCorrectly()
        {
            //Arrange
            var stack = new StaticStack<string>(5);
            
            //Act
            stack.Push("one");
            stack.Push("two");
            stack.Push("three");
            bool found = stack.Contains("three");
            stack.Pop();
            bool notFound = stack.Contains("three");

            //Assert
            Assert.IsTrue(found);
            Assert.IsFalse(notFound);
        }

        [TestMethod]
        public void StackCopyToWorksCorrectly()
        {
            //Arrange
            var stack = new StaticStack<string>(5);
            var emptyArray = new string[3];
            var expectedArray = new string[] { "three", "two", "one" };

            //Act
            stack.Push("one");
            stack.Push("two");
            stack.Push("three");
            stack.CopyTo(emptyArray, 0);

            //Assert
            CollectionAssert.AreEqual(expectedArray, emptyArray);
        }

        [TestMethod]
        public void StackClearWorksCorrectly()
        {
            //Arrange
            var stack = new StaticStack<string>(5);
            int expected = 0;

            //Act
            stack.Push("one");
            stack.Push("two");
            stack.Push("three");
            stack.Push("four");
            stack.Push("five");
            stack.Clear();
            int count = stack.Count;

            //Assert
            Assert.AreEqual(expected, count);
        }

        [TestMethod]
        public void StackToArrayWorksCorrectly()
        {
            //Arrange
            var stack = new StaticStack<string>(5);
            var expected = new string[] { "five", "four", "three", "two", "one" };

            //Act
            stack.Push("one");
            stack.Push("two");
            stack.Push("three");
            stack.Push("four");
            stack.Push("five");
            var array = stack.ToArray();

            //Assert
            CollectionAssert.AreEqual(expected, array);
        }

        [TestMethod]
        public void StackEnumeratesCorrectly()
        {
            //Arrange
            var stack = new StaticStack<string>(5);
            var expected = new List<string>() { "five", "four", "three", "two", "one" };

            //Act
            stack.Push("one");
            stack.Push("two");
            stack.Push("three");
            stack.Push("four");
            stack.Push("five");
            var list = new List<string>();

            foreach (var item in stack)
            {
                list.Add(item);
            }

            //Assert
            CollectionAssert.AreEqual(expected, list);
        }

        [TestMethod]
        public void StackDoesntEnumerateEmpty()
        {
            //Arrange
            var stack = new StaticStack<string>(5);
            var expected = false;

            //Act
            stack.Push("one");
            stack.Push("two");
            stack.Push("three");
            stack.Push("four");
            stack.Push("five");
            stack.Clear();

            foreach (var item in stack)
            {
                expected = true;
            }

            //Assert
            Assert.IsFalse(expected);
        }

        [TestMethod]
        public void StackPushNullWorksCorrectly()
        {
            //Arrange
            var stack = new StaticStack<string>(5);
            var expected = new string[] { null ,"three", "two", "one" };
            string input = null;
            //Act
            stack.Push("one");
            stack.Push("two");
            stack.Push("three");
            stack.Push(input);
            var result = stack.ToArray();

            //Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StackCopyToWithIllegalIndexThrowsException()
        {
            //Arrange
            var stack = new StaticStack<string>(5);
            var emptyArray = new string[5];

            //Act
            stack.Push("one");
            stack.Push("two");
            stack.Push("three");
            stack.Push("four");
            stack.Push("five");
            stack.CopyTo(emptyArray, 3);

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StackCopyToWithNullArrayThrowsException()
        {
            //Arrange
            var stack = new StaticStack<string>(5);
            var emptyArray = default(string[]);

            //Act
            stack.Push("one");
            stack.Push("two");
            stack.Push("three");
            stack.Push("four");
            stack.Push("five");
            stack.CopyTo(emptyArray, 0);

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void StackCopyToWithBiggerIndexThanStackLengthThrowsException()
        {
            //Arrange
            var stack = new StaticStack<string>(5);
            var emptyArray = new string[5];

            //Act
            stack.Push("one");
            stack.Push("two");
            stack.Push("three");
            stack.Push("four");
            stack.Push("five");
            stack.CopyTo(emptyArray, 6);

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PeekEmptyStackThrowsException()
        {
            //Arrange
            var stack = new StaticStack<string>(5);

            //Act
            stack.Peek();
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PopEmptyStackThrowsException()
        {
            //Arrange
            var stack = new StaticStack<string>(5);

            //Act
            stack.Pop();
            //Assert
        }
    }
}
