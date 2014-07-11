using System;
using System.CodeDom;
using System.Collections.Generic;
using Utils;
using Xunit;

namespace TddSample.Tests
{
    class StackTests
    {
        public class GivenStack : TestBase
        {
            protected Stack<int> stack;

            protected override sealed void Given()
            {
                stack = new Stack<int>();
            }
        }

        public class EmptyStack : GivenStack
        {
            [Fact]
            public void Count_ShouldReturnZero()
            {
                int count = stack.Count;

                Assert.Equal(0, count);
            }

            [Fact]
            public void Contains_ShouldReturnFalse()
            {
                bool contains = stack.Contains(10);

                Assert.False(contains);
            }

            [Fact]
            public void Pop_ShouldThrowInvalidOperationException()
            {
                Exception exception = Record.Exception(() => stack.Pop());

                Assert.IsType<InvalidOperationException>(exception);
            }

            [Fact]
            public void Peek_ShouldThrowInvalidOperationException()
            {
                Exception exception = Record.Exception(() => stack.Peek());

                Assert.IsType<InvalidOperationException>(exception);
            }
        }

        public class StackWithOneElement : GivenStack
        {
            readonly int pushedValue = Build.Any<int>();

            protected override sealed void When()
            {
                stack.Push(pushedValue);
            }

            [Fact]
            public void Count_ShouldBeOne()
            {
                int count = stack.Count;

                Assert.Equal(1, count);
            }

            [Fact]
            public void Pop_CountShouldBeZero()
            {
                stack.Pop();

                int count = stack.Count;

                Assert.Equal(0, count);
            }

            [Fact]
            public void Peek_CountShouldBeOne()
            {
                stack.Peek();

                int count = stack.Count;

                Assert.Equal(1, count);
            }

            [Fact]
            public void Pop_ShouldReturnPushedValue()
            {
                int actual = stack.Pop();

                Assert.Equal(pushedValue, actual);
            }

            [Fact]
            public void Peek_ShouldReturnPushedValue()
            {
                int actual = stack.Peek();

                Assert.Equal(pushedValue, actual);
            }
        }

        public class StackWithMultipleValues : GivenStack
        {
            readonly int firstPushedValue = Build.Any<int>();
            readonly int secondPushedValue = Build.Any<int>();
            readonly int thirdPushedValue = Build.Any<int>();

            protected override sealed void When()
            {
                stack.Push(firstPushedValue);
                stack.Push(secondPushedValue);
                stack.Push(thirdPushedValue);
            }

            [Fact]
            public void Count_ShouldBeThree()
            {
                int count = stack.Count;

                Assert.Equal(3, count);
            }

            [Fact]
            public void Pop_VerifyLifoOrder()
            {
                Assert.Equal(thirdPushedValue, stack.Pop());
                Assert.Equal(secondPushedValue, stack.Pop());
                Assert.Equal(firstPushedValue, stack.Pop());
            }

            [Fact]
            public void Peek_ReturnsLastPushedValue()
            {
                int actual = stack.Peek();

                Assert.Equal(thirdPushedValue, actual);
            }

            [Fact]
            public void Contains_ReturnsTrue()
            {
                bool contains = stack.Contains(secondPushedValue);

                Assert.True(contains);
            }
        }

        public class StackWithStrings
        {
            [Fact]
            public void Pop_ShouldReturnPushedValue()
            {
                // arrange
                var stack = new Stack<string>();
                var text = Build.Any<string>();
                stack.Push(text);

                // act
                string actual = stack.Pop();

                // assert
                Assert.Equal(text, actual);
            }
        }

        public class StackWithStringsAltnerate : TestBase
        {
            Stack<string> stack;
            string text;
            string result;

            protected override sealed void Given()
            {
                text = Build.Any<string>();
                stack = new Stack<string>();
                stack.Push(text);
            }

            protected override sealed void When()
            {
                result = stack.Pop();
            }

            [Fact]
            public void Pop_ShouldReturnPushedValue()
            {
                Assert.Equal(text, result);
            }
        }
    }
}
