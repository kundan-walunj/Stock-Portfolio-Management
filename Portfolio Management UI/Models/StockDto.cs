namespace Portfolio_Management_UI.Models

{
    public class StockDto
    {
        //[BsonElement("customer_name"), BsonRepresentation(BsonType.String)]
        public string Symbol { get; set; }
        public int Quantity { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}
