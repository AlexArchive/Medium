using GoBlog.Infrastructure.Paging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoBlog.Test.Paging
{
    [TestFixture]
    public class PagedListTest
    {
        [Test]
        public void PageSizeIsCorrect()
        {
            var source = Enumerable.Range(0, 5);
            
            var actual = new PagedList<int>(source, 0, 5);

            Assert.That(actual.PageSize, Is.EqualTo(5));
        }

        [Test]
        public void PageNumberIsCorrect()
        {
            var source = Enumerable.Range(0, 10);
            
            var actual = new PagedList<int>(source, 1, 5);
            
            Assert.That(actual.PageNumber, Is.EqualTo(1));
        }

        [Test]
        public void TotalCountIsCorrect()
        {
            var source = Enumerable.Range(0, 10);
            
            var actual = new PagedList<int>(source, 0, 0);
            
            Assert.That(actual.TotalCount, Is.EqualTo(10));
        }

        [TestCase(3, 0, 2, ExpectedResult = 2)]
        [TestCase(1, 0, 2, ExpectedResult = 1)]
        [TestCase(5, 2, 5, ExpectedResult = 0)]
        public int CountIsCorrect(int sourceLength, int pageNumber, int pageSize)
        {
            var source = Enumerable.Range(0, sourceLength);
            
            var actual = new PagedList<int>(source, pageNumber, pageSize);

            return actual.Count;
        }

        [TestCase(5, 5, ExpectedResult = 1)]
        [TestCase(10, 5, ExpectedResult = 2)]
        [TestCase(500, 50, ExpectedResult = 10)]
        public int PageCountIsCorrect(int sourceLength, int pageSize)
        {
            var source = Enumerable.Range(0, sourceLength);
            
            var actual = new PagedList<int>(source, 0, pageSize);

            return actual.PageCount;
        }

        [Test]
        public void EmptySourceDoesNotThrow()
        {
            var source = Enumerable.Empty<int>();
            
            var actual = new PagedList<int>(source, 0, 5);
            
            Assert.That(actual.Count, Is.EqualTo(0));
        }

        [Test]
        public void NullSourceThrows()
        {
            IEnumerable<int> source = null;
            
            Assert.Throws<ArgumentNullException>(() => new PagedList<int>(source, 0, 5));  
        }

        [Test]
        public void NegativePageNumberThrows()
        {
            var source = Enumerable.Empty<int>();

            Assert.Throws<ArgumentOutOfRangeException>(() => new PagedList<int>(source, -1, 5));
        }

        [Test]
        public void NegativePageSizeThrows()
        {
            var source = Enumerable.Empty<int>();

            Assert.Throws<ArgumentOutOfRangeException>(() => new PagedList<int>(source, 0, -5));
        }
    }
}