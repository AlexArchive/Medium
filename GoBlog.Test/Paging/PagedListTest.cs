using System;
using System.Collections.Generic;
using System.Linq;
using GoBlog.Infrastructure.Paging;
using NUnit.Framework;

namespace GoBlog.Test.Paging
{
    [TestFixture]
    public class PagedListTest
    {
        [Test]
        public void PageSizeIsCorrect()
        {
            // arrange
            var source = Enumerable.Range(0, 5);
            
            // act
            var pagedList = new PagedList<int>(source, 0, 5);

            // assert
            Assert.That(pagedList.PageSize, Is.EqualTo(5));
        }

        [Test]
        public void PageNumberIsCorrect()
        {
            // arrange
            var source = Enumerable.Range(0, 10);
            
            // act
            var pagedList = new PagedList<int>(source, 1, 5);
            
            // assert
            Assert.That(pagedList.PageNumber, Is.EqualTo(1));
        }

        [Test]
        public void TotalCountIsCorrect()
        {
            // arrange
            var source = Enumerable.Range(0, 10);
            
            // act
            var pagedList = new PagedList<int>(source, 0, 0);
            
            // assert
            Assert.That(pagedList.TotalCount, Is.EqualTo(10));
        }

        [TestCase(3, 0, 2, ExpectedResult = 2)]
        [TestCase(1, 0, 2, ExpectedResult = 1)]
        [TestCase(5, 2, 5, ExpectedResult = 0)]
        public int CountIsCorrect(int sourceLength, int pageNumber, int pageSize)
        {
            // arrange
            var source = Enumerable.Range(0, sourceLength);
            
            // act
            var pagedList = new PagedList<int>(source, pageNumber, pageSize);
            return pagedList.Count;
        }

        [TestCase(5, 5, ExpectedResult = 1)]
        [TestCase(10, 5, ExpectedResult = 2)]
        [TestCase(500, 50, ExpectedResult = 10)]
        public int PageCountCorrect(int sourceLength, int pageSize)
        {
            // arrange
            var source = Enumerable.Range(0, sourceLength);
            
            // act
            var pagedList = new PagedList<int>(source, 0, pageSize);
            return pagedList.PageCount;
        }

        [Test]
        public void EmptySourceDoesNotThrow()
        {
            // arrange
            var source = Enumerable.Empty<int>();
            
            // act
            var pagedList = new PagedList<int>(source, 0, 5);
            
            // assert
            Assert.That(pagedList.Count, Is.EqualTo(0));
        }

        [Test]
        public void NullSourceThrows()
        {
            // arrange
            IEnumerable<int> source = null;
            
            // assert
            Assert.Throws<ArgumentNullException>(() => new PagedList<int>(source, 0, 5));  
        }

        [Test]
        public void NegativePageNumberThrows()
        {
            // arrange
            var source = Enumerable.Empty<int>();

            // assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new PagedList<int>(source, -1, 5));
        }

        [Test]
        public void NegativePageSizeThrows()
        {
            // arrange
            var source = Enumerable.Empty<int>();

            // assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new PagedList<int>(source, 0, -5));
        }
    }
}