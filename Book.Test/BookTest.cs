using Book.Api.Controllers;
using Book.Api.Service;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Polly;

namespace Book.Test
{
    [TestClass]
    public class BookTest
    {
        private Mock<IBook>? _book;
        private Mock<IHttpClientFactory>? _httpClientFactory;
        private Mock<IAsyncPolicy<HttpResponseMessage>>? _asyncPolicy;


        private BookController? _bookController;

        [TestInitialize]
        public void GetTestInitialize()
        {
            _book = new Mock<IBook>();
            var mock = new Mock<ILogger<BookController>>();
            ILogger<BookController> logger = mock.Object;
            logger = Mock.Of<ILogger<BookController>>();
            _httpClientFactory = new Mock<IHttpClientFactory>();

            _asyncPolicy = new Mock<IAsyncPolicy<HttpResponseMessage>>();
            _bookController = new BookController(_book.Object, logger, _httpClientFactory.Object, _asyncPolicy.Object);
        }

        [TestMethod]
        public void GetBooks()
        {
            // Act on Test  
            var response = _bookController.GetBooks().GetAwaiter().GetResult() as ObjectResult; ;

            // Assert the result  
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.StatusCode);
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [TestMethod]
        public void BookAvailable()
        {
            // Act on Test  
            var response = _bookController.BookAvailable("test").GetAwaiter().GetResult() as ObjectResult; ;

            // Assert the result  
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.StatusCode);
            Assert.AreEqual(200, (int)response.StatusCode);
        }
    }
}