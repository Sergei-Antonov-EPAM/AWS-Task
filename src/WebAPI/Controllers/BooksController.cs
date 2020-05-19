using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Get()
        {
            var books = _bookLogic.GetAll();
            return Ok(books);
        }
    }
}
