using BookAPI.Models.Entities;
using MassTransit;
using MongoDB.Driver;
using Shared.Events;

namespace BookAPI.Consumers
{
    public class OrderFailedEventConsumer : IConsumer<OrderFailedEvent>
    {
        IMongoCollection<Book> _bookCollection;

        public OrderFailedEventConsumer(IMongoCollection<Book> bookCollection)
        {
            _bookCollection = bookCollection;
        }

        public async Task Consume(ConsumeContext<OrderFailedEvent> context)
        {
            foreach (var orderItem in context.Message.OrderItems)
            {
                var bookStock = await (await _bookCollection.FindAsync(s => s.BookId == orderItem.ProductId.ToString())).FirstOrDefaultAsync();
                if (bookStock != null)
                {
                    bookStock.Quantity += orderItem.Count;
                    await _bookCollection.FindOneAndReplaceAsync(s => s.BookId == orderItem.ProductId.ToString(), bookStock);
                }
            }
        }
    }
}
