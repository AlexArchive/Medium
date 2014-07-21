using GoBlog.Infrastructure.Paging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoBlog.Test.Infrastructure.Paging
{
    [TestFixture]
    public class PagedListTest
    {
        [Test]
        public void PagedList_HasCorrectPageSize()
        {
            var source = Enumerable.Range(0, 5);

            var actual = source.ToPagedList(pageNumber: 0, pageSize: 5);

            Assert.That(actual.PageSize, Is.EqualTo(5));
        }

        [Test]
        public void PagedList_HasCorrectPageNumber()
        {
            var source = Enumerable.Range(0, 10);

            var actual = source.ToPagedList(pageNumber: 1, pageSize: 5);

            Assert.That(actual.PageNumber, Is.EqualTo(1));
        }

        [Test]
        public void PagedList_HasCorrectTotalCount()
        {
            var source = Enumerable.Range(0, 10);

            var actual = source.ToPagedList(pageNumber: 0, pageSize: 0);

            Assert.That(actual.TotalCount, Is.EqualTo(10));
        }

        [TestCase(3, 0, 2, ExpectedResult = 2)]
        [TestCase(1, 0, 2, ExpectedResult = 1)]
        [TestCase(5, 2, 5, ExpectedResult = 0)]
        public int PagedList_HasExpectedCount(int sourceCount, int pageNumber, int pageSize)
        {
            var source = Enumerable.Range(0, sourceCount);

            var actual = source.ToPagedList(pageNumber, pageSize);

            return actual.Count;
        }

        [TestCase(5, 5, ExpectedResult = 1)]
        [TestCase(10, 5, ExpectedResult = 2)]
        [TestCase(500, 50, ExpectedResult = 10)]
        public int PagedList_HasExpectedPageCount(int sourceLength, int pageSize)
        {
            var source = Enumerable.Range(0, sourceLength);

            var actual = source.ToPagedList(pageNumber: 0, pageSize: pageSize);

            return actual.PageCount;
        }

        [Test]
        public void EmptySource_DoesNotThrow()
        {
            var source = Enumerable.Empty<int>();

            var actual = source.ToPagedList(pageNumber: 0, pageSize: 5);

            Assert.That(actual.Count, Is.EqualTo(0));
        }

        [Test]
        public void NullSource_Throws()
        {
            IEnumerable<int> source = null;

            Assert.Throws<ArgumentNullException>(() => new PagedList<int>(source, 0, 5));
        }

        [Test]
        public void NegativePageNumber_Throws()
        {
            var source = Enumerable.Empty<int>();

            Assert.Throws<ArgumentOutOfRangeException>(() => new PagedList<int>(source, -1, 5));
        }

        [Test]
        public void NegativePageSize_Throws()
        {
            var source = Enumerable.Empty<int>();

            Assert.Throws<ArgumentOutOfRangeException>(() => new PagedList<int>(source, 0, -5));
        }
    }
}