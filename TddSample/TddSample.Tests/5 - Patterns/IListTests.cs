using System.Collections.Generic;
using Utils;
using Xunit;

namespace TddSample.Tests
{
    public abstract class IListTests<T>
    {
        protected abstract IList<T> CreateList();

        protected abstract T CreateValue();

        [Fact]
        public void Count_EmptyList_ShouldReturnZero()
        {
            IList<T> list = CreateList();

            int count = list.Count;

            Assert.Equal(0, count);
        }

        [Fact]
        public void Count_AddedOneElement_ShouldBeOne()
        {
            IList<T> list = CreateList();
            T value = CreateValue();

            list.Add(value);

            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void Remove_AddedOneElement_CountShouldBeZero()
        {
            IList<T> list = CreateList();
            T value = CreateValue();
            list.Add(value);

            list.Remove(value);

            Assert.Equal(0, list.Count);
        }
    }

    class StringListTests : IListTests<string>
    {
        protected override IList<string> CreateList()
        {
            return new List<string>();
        }

        protected override string CreateValue()
        {
            return Build.Any<string>();
        }
    }

    class IntListTests : IListTests<int>
    {
        protected override IList<int> CreateList()
        {
            return new List<int>();
        }

        protected override int CreateValue()
        {
            return Build.Any<int>();
        }
    }
}