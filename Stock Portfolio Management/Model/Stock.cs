using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Stock_Portfolio_Management.Model
{
    public class Stock
    {
        [BsonId]
        [BsonElement("_id"),BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Stock_name"), BsonRepresentation(BsonType.String)]
        public string Symbol { get; set; }
        public int Quantity { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal CurrentPrice { get; set; }

    }
}
