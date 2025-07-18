using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Stock_Portfolio_Management.Model
{
    public class StocksDto
    {
        [BsonElement("customer_name"), BsonRepresentation(BsonType.String)]
        public string Symbol { get; set; }
        public int Quantity { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}
