using BookAPI.Dtos;
using BookAPI.Interfaces;
using BookAPI.Models.Entities;

namespace BookAPI.Services
{
    public class BookService: IBookService
    {
        private readonly MongoDBService _mongoDBService;

        public BookService(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        public async Task CreateBookAsync(CreateBookDto bookDto)
        {
            var bookEntity = new Book { CreatedDate=DateTime.Now, Price=bookDto.Price, Quantity=bookDto.Quantity,Name=bookDto.Name };
            await _mongoDBService.CreateAsync(bookEntity);
        }

        public async Task UpdateStockBookAsync(UpdateBookStockDto updateBookStockDto)
        {
            await _mongoDBService.UpdateBookQuantityAsync(updateBookStockDto.BookId,updateBookStockDto.NewQuantity);
        }
    }
}
