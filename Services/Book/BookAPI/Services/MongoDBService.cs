using BookAPI.Interfaces;
using BookAPI.Models.Entities;
using MongoDB.Driver;

namespace BookAPI.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Book> _bookCollection;
        public MongoDBService(IConfiguration configuration)
        {
            MongoClient client = new(configuration.GetConnectionString("MongoDB"));
            IMongoDatabase _database = client.GetDatabase("BookAPIDB");
            _bookCollection = _database.GetCollection<Book>("Book");
        }
        public IMongoCollection<Book> GetCollection() => _bookCollection;
        public async Task CreateAsync(Book book) {
            await _bookCollection.InsertOneAsync(book);   
        }
        public async Task UpdateBookQuantityAsync(string bookId, int quantity) {

            Book entity = await (await _bookCollection.FindAsync(s => s.BookId == bookId)).FirstOrDefaultAsync();
            entity.Quantity = quantity;
            await _bookCollection.FindOneAndReplaceAsync(s => s.BookId == bookId, entity);
        }
    }
}
