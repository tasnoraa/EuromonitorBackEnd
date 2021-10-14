using Ecommerce.Service;
using EcommerceRepository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Tests.ServiceTests
{
    [TestFixture]
    public class EcommerceServiceTests
    {

        [SetUp]
        public void Setup()
        {
            
        }
        [Test]
        public void GetBooks_WhenCalled_ReturnBooks()
        {
            var data = new List<Book>
            {
                new Book { Name = "BBB" },
                new Book { Name = "ZZZ" },
                new Book { Name = "AAA" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Book>>();
            var mockContext = new Mock<EcommerceDBContext>();
           

            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext.Setup(mc => mc.books).Returns(mockSet.Object);

            var service = new EcommerceService(mockContext.Object);
            var results = service.GetBooks();

            Assert.AreEqual(3, results.Count);
            Assert.AreEqual("BBB", results[0].Name);
            Assert.AreEqual("ZZZ", results[1].Name);
            Assert.AreEqual("AAA", results[2].Name);
        }

        [Test]
        public void GetNoBooks_WhenCalled_ReturnEmptyList()
        {
            var data = new List<Book>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Book>>();
            var mockContext = new Mock<EcommerceDBContext>();


            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext.Setup(mc => mc.books).Returns(mockSet.Object);

            var service = new EcommerceService(mockContext.Object);
            var results = service.GetBooks();

            Assert.AreEqual(0, results.Count);
        }
    }
}
