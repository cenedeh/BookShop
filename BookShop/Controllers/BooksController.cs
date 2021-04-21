using BookShop.Application.UseCases.Book.Command;
using BookShop.Application.UseCases.Book.Dto;
using BookShop.Application.UseCases.Book.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// This endpoint saves a new book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     POST /addBook
        ///     {
        ///         "title": "James Obodo",
        ///         "isbnCode": "12hy2822",
        ///         "category": "Fiction",
        ///         "DatePublished" "2021-04-23T00:00:00"
        ///     }
        /// </remarks>
        [HttpPost("/addBook")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> AddBook([FromBody]AddBooksCommand book)
        {
            await _mediator.Send(book);
            return Ok();
        }
        
        /// <summary>
        /// this endpoint is used to get books
        /// </summary>
        ///  <remarks>
        /// Sample Request:
        /// 
        ///     GET /book
        ///     {
        ///         "bookId": "1"
        ///         
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpGet("/books")]
        [ProducesResponseType(typeof(GetBooksDto), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Books()
        {
            var books = await _mediator.Send(new BookQuery());
            return Ok(books);
        }

        /// <summary>
        /// this endpoint is used to get books
        /// </summary>
        ///  <remarks>
        /// Sample Request:
        /// 
        ///     GET /booksByAuthor
        ///     {
        ///         "authorId": "1"
        ///         
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpGet("/booksByAuthor")]
        [ProducesResponseType(typeof(List<GetBooksDto>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> BooksByAuthors()
        {
            var books = await _mediator.Send(new BooksAuthorQuery());
            return Ok(books);
        }

        /// <summary>
        /// this endpoint is used to get books
        /// </summary>
        ///  <remarks>
        /// Sample Request:
        /// 
        ///     GET /booksByCategory
        ///     {
        ///         "categoryId": "1"
        ///         
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpGet("/booksByCategory")]
        [ProducesResponseType(typeof(List<GetBooksDto>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> BooksByCategory()
        {
            var books = await _mediator.Send(new BookCategoryQuery());
            return Ok(books);
        }

        /// <summary>
        /// This endpoint deletes a book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     POST /deleteBook
        ///     {
        ///         "bookId": "2"
        ///     }
        /// </remarks>
        [HttpPost("/deleteBook")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> DeleteBook([FromBody]DeleteBooksCommand book)
        {
            await _mediator.Send(book);
            return Ok();
        }

        /// <summary>
        /// This endpoint updates a book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     POST /updateBook
        ///     {
        ///         "title": "James Obodo",
        ///         "isbnCode": "12hy2822",
        ///         "category": "Fiction",
        ///         "DatePublished" "2021-04-23T00:00:00"
        ///     }
        /// </remarks>
        [HttpPost("/updateBook")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> UpdateBook([FromBody]UpdateBooksCommand book)
        {
            await _mediator.Send(book);
            return Ok();
        }
    }
}
