using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookLogic _bookLogic;

        public BooksController(IBookLogic bookLogic)
        {
            _bookLogic = bookLogic;
        }

        [Route("{controller}")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookLogic.GetAll();
            return Ok(books);
        }

        [Route("{controller}/{isbn}")]
        [HttpGet]
        public async Task<IActionResult> GetByISBN(string isbn)
        {
            var book = await _bookLogic.GetByISBN(isbn);
            return Ok(book);
        }

        [Route("{controller}")]
        [HttpPost]
        public async Task<IActionResult> Add(Book book)
        {
            await _bookLogic.Add(book);
            return Ok("Added");
        }

        [Route("{controller}/{isbn}")]
        [HttpDelete]
        public async Task<IActionResult> Remove(string isbn)
        {
            await _bookLogic.Remove(isbn);
            return Ok("Removed");
        }

        [Route("{controller}/{isbn}")]
        [HttpPut]
        public async Task<IActionResult> Update(string isbn, Book book)
        {
            await _bookLogic.Update(isbn, book);
            return Ok("Updated");
        }
    }
}
