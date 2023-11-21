using BookAPI.Dtos;
using BookAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateBook(CreateBookDto createBookDto)
        {
            await _bookService.CreateBookAsync(createBookDto);
            return Created("~/api/CreateBook",null);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateBook(UpdateBookStockDto updateBookStockDto)
        {
            await _bookService.UpdateStockBookAsync(updateBookStockDto);
            return Ok();
        }
    }
}
