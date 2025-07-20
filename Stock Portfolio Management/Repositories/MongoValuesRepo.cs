using MongoDB.Driver;
using Stock_Portfolio_Management.Data;
using Stock_Portfolio_Management.Model;

namespace Stock_Portfolio_Management.Repositories
{
    public class MongoValuesRepo : IValuesRepo
    {
        public readonly IMongoCollection<Stock>? _stocks;

        public MongoValuesRepo(MongoDbService mongoDbService)
        {
            _stocks = mongoDbService.Database?.GetCollection<Stock>("stocks");

        }

        public async Task<Stock> DeleteStockAsync(string id)
        {
            var filter = Builders<Stock>.Filter.Eq(s => s.Id, id);
            var stock = await _stocks.Find(filter).FirstOrDefaultAsync();
            await _stocks.DeleteOneAsync(filter);
            return stock;
        }

        public async Task<Stock> GetSingleAsync(string id)
        {
            var filter = Builders<Stock>.Filter.Eq(x => x.Id, id);
            var GetStock = await _stocks.Find(filter).FirstOrDefaultAsync();
            return GetStock;    
        }

        public async Task<List<Stock>> GetStocksAsync()
        {
            var result = await _stocks.Find(_ => true).ToListAsync();
            return result;  
        }

        public async Task<Stock> InsertStockAsync(Stock inputStock)
        {
            await _stocks.InsertOneAsync(inputStock);
            return inputStock; 
        }


        public async Task<Stock> UpdateStockAsync(string id, Stock stocks)
        {
            var filter = Builders<Stock>.Filter.Eq(s => s.Id, id);
            var update = Builders<Stock>.Update.Set(s => s.BuyPrice, stocks.BuyPrice)
                .Set(s => s.Quantity, stocks.Quantity);
            
            await _stocks.UpdateOneAsync(filter, update);
            var stock = await _stocks.Find(filter).FirstOrDefaultAsync();
            return stock;
        }
    }
}
