using BookAPI.Dtos;

namespace BookAPI.Interfaces
{
    public interface IBookService
    {
        Task CreateBookAsync(CreateBookDto bookDto);
        Task UpdateStockBookAsync(UpdateBookStockDto updateBookStockDto);
    }
}
