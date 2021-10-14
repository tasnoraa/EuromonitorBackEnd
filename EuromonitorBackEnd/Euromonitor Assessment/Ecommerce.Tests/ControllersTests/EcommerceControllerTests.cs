using Ecommerce.Controllers;
using EcommerceRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Utilities;

namespace Ecommerce.Tests.ControllersTests
{
    [TestFixture]
    public class EcommerceServiceTests
    {
        private Mock<IEcommerceService> _ecommerceService;
        private EcommerceController _ecommerceController;
        [SetUp]
        public void Setup()
        {
            _ecommerceService = new Mock<IEcommerceService>();
            _ecommerceController = new EcommerceController(_ecommerceService.Object);
        }
        [Test]
        public void EmptyGet_WhenCalled_ReturnStatusCode200()
        {
            var books = new List<Book>();
            _ecommerceService.Setup(ec => ec.GetBooks()).Returns(books);
            IActionResult result = _ecommerceController.Get();
            var okResult = result as OkObjectResult;

            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void EmptyGet_WhenCalled_ReturnStatus()
        {
            var books = new List<Book>() 
            {
                new Book(){ Name = "Habits"}
            };
            _ecommerceService.Setup(ec => ec.GetBooks()).Returns(books);
            IActionResult result = _ecommerceController.Get();
            var okResult = result as OkObjectResult;
            var bookResult = okResult.Value as List<Book>;

            Assert.That(bookResult[0].Name, Is.EqualTo("Habits"));
        }
    }
}
