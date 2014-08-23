using System;
using GoBlog.Common.Pagination;
using NUnit.Framework;
using System.Linq;

namespace GoBlog.UnitTest
{
    [TestFixture]
    public class PagedListTests
    {
        [Test]
        public void Constructor_ReturnsCorrectPageSize()
        {
            var source     = new[] { 1, 2 };
            var pageNumber = 1;
            var pageSize   = 1;

            var actual = new PagedList<int>(source, pageSize, pageNumber);

            Assert.AreEqual(pageSize, actual.Count);
        }

        [Test]
        public void Constructor_NonExistentPage_ReturnsEmptySequence()
        {
            var source     = new[] { 1, 2, 3 };
            var pageNumber = 2;
            var pageSize   = 3;

            var actual = new PagedList<int>(source, pageNumber, pageSize);

            Assert.IsEmpty(actual);
        }

        [Test]
        public void Constructor_PageSizeGreaterThanSourceCount_ReturnsAll()
        {
            var source     = new[] { 1, 2, 3 };
            var pageNumber = 1;
            var pageSize   = 10;

            var actual = new PagedList<int>(source, pageNumber, pageSize);

            Assert.AreEqual(source.Length, actual.Count);
        }

        [Test]
        public void Constructor_PageNumberOne_ReturnsCorrectPartition()
        {
            var source     = new[] { 1, 2 };
            var pageNumber = 1;
            var pageSize   = 1;

            var actual = new PagedList<int>(source, pageNumber, pageSize);

            Assert.AreEqual(source.First(), actual.First());
        }

        [Test]
        public void Constructor_PageNumberTwo_ReturnsCorrectPartition()
        {
            var source     = new[] { 1, 2 };
            var pageNumber = 2;
            var pageSize   = 1;

            var actual = new PagedList<int>(source, pageNumber, pageSize);

            Assert.AreEqual(source.ElementAt(1), actual.First());
        }

        [Test]
        public void Constructor_OddSourceCount_ReturnsCorrectPartition()
        {   
            var source     = new[] { 1, 2, 3 };
            var pageNumber = 2;
            var pageSize   = 2;

            var actual = new PagedList<int>(source, pageNumber, pageSize);

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(source.ElementAt(2), actual.First());
        }

        [Test]
        public void Constructor_SetsPageNumber()
        {
            var source     = Enumerable.Empty<object>();
            var pageNumber = 2;
            var pageSize   = 0;

            var actual = new PagedList<object>(source, pageNumber, pageSize);

            Assert.AreEqual(pageNumber, actual.PageNumber);
        }

        [Test]
        public void Constructor_SetsTotalPageCount()
        {
            var source = new[] { 1, 2 };
            var pageNumber = 1;
            var pageSize = 1;

            var actual = new PagedList<int>(source, pageNumber, pageSize);

            Assert.AreEqual(2, actual.TotalPageCount);
        }

        [Test]
        public void Constructor_SetsHasNextPage()
        {
            var source     = new[] { 1, 2 };
            var pageNumber = 1;
            var pageSize   = 1;

            var actual = new PagedList<int>(source, pageNumber, pageSize);

            Assert.True(actual.HasNextPage);
        }

        [Test]
        public void Constructor_NoNextPage_SetsHasNextPage()
        {
            var source     = new[] { 1, 2 };
            var pageNumber = 2;
            var pageSize   = 1;

            var actual = new PagedList<int>(source, pageSize, pageNumber);

            Assert.False(actual.HasNextPage);
        }

        [Test]
        public void Constructor_SetsHasPreviousPage()
        {
            var source     = new[] { 1, 2 };
            var pageNumber = 2;
            var pageSize   = 1;

            var actual = new PagedList<int>(source, pageNumber, pageSize);

            Assert.True(actual.HasPreviousPage);
        }

        [Test]
        public void Constructor_NoPreviousPage_SetsHasPreviousPage()
        {
            var source     = new[] { 1, 2 };
            var pageNumber = 1;
            var pageSize   = 1;

            var actual = new PagedList<int>(source, pageNumber, pageSize);

            Assert.False(actual.HasPreviousPage);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_PageNumberLessThanOne_Throws()
        {
            var source     = Enumerable.Empty<object>();
            var pageNumber = 0;
            var pageSize   = 1;

            var actual = new PagedList<object>(source, pageNumber, pageSize);
        }
    }
}