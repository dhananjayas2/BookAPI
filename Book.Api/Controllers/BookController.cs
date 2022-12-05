using Microsoft.AspNetCore.Mvc;
using Book.Api.Service;
using Newtonsoft.Json;
using System.Data;
using System;
using System.Collections.Generic;
using Polly;

namespace Book.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        private IBook _objBook = null;
        private readonly ILogger<BookController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAsyncPolicy<HttpResponseMessage> _policy;
        public BookController(IBook objBook, ILogger<BookController> logger, IHttpClientFactory httpClientFactory, IAsyncPolicy<HttpResponseMessage> policy)
        {
            _objBook = objBook;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _policy = policy;
        }
        [HttpGet]
        [Route("GetBooks")]
        public async Task<IActionResult> GetBooks()
        {
            DataTable dt;
            try
            {
                dt = _objBook.GetBooks();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw;
            }
            
            return Ok(JsonConvert.SerializeObject(dt));
        }

        [HttpGet]
        [Route("BookAvailable/{BookId}")]
        public async Task<IActionResult> BookAvailable(string BookId)
        {
            bool returnValue = false;
            try
            {
                returnValue = _objBook.BookAvailable(BookId);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw;
            }
            
            return Ok(returnValue);
        }
    }

}
