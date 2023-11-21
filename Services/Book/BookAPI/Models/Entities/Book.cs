using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookAPI.Models.Entities
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement(Order = 0)]
        public string BookId { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        [BsonElement(Order = 1)]
        public decimal Price { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        [BsonElement(Order = 2)]
        public string Name { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.Int64)]
        [BsonElement(Order = 2)]
        public int Quantity { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        [BsonElement(Order = 3)]
        public DateTime CreatedDate { get; set; }
       
    }
}
