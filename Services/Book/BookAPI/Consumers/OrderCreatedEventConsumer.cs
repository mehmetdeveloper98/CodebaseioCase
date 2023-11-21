using BookAPI.Models.Entities;
using BookAPI.Services;
using MassTransit;
using MongoDB.Driver;
using Shared;
using Shared.Events;
using Shared.Messages;

namespace BookAPI.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        IMongoCollection<Book> _bookCollection;
        readonly ISendEndpointProvider _sendEndpointProvider;
        readonly IPublishEndpoint _publishEndpoint;

        public OrderCreatedEventConsumer(MongoDBService mongoDBService, ISendEndpointProvider sendEndpointProvider, IPublishEndpoint publishEndpoint)
        {
            _bookCollection = mongoDBService.GetCollection();
            _sendEndpointProvider = sendEndpointProvider;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            List<bool> stockResult = new();
            foreach (OrderItemMessage orderItem in context.Message.OrderItems)
            {
                stockResult.Add((await _bookCollection.FindAsync(s => s.BookId == orderItem.ProductId && s.Quantity >= orderItem.Count)).Any());
            }

            if (stockResult.TrueForAll(sr => sr.Equals(true)))
            {
                foreach (OrderItemMessage orderItem in context.Message.OrderItems)
                {
                    Book book = await (await _bookCollection.FindAsync(s => s.BookId == orderItem.ProductId)).FirstOrDefaultAsync();

                    book.Quantity -= orderItem.Count;
                    await _bookCollection.FindOneAndReplaceAsync(s => s.BookId == orderItem.ProductId, book);
                }

                StockReservedEvent stockReservedEvent = new()
                {
                    CustomerId = context.Message.CustomerId,
                    OrderId = context.Message.OrderId,
                    TotalPrice = context.Message.TotalPrice,
                };

                ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettings.Payment_StockReservedEventQueue}"));
                await sendEndpoint.Send(stockReservedEvent);
            }
            else
            {
                //Siparişin tutarsız/geçersiz olduğuna dair işlemler...
                StockNotReservedEvent stockNotReservedEvent = new()
                {
                    CustomerId = context.Message.CustomerId,
                    OrderId = context.Message.OrderId,
                    Message = "Out of stock."
                };

                await _publishEndpoint.Publish(stockNotReservedEvent);
            }
        }
    }
}
