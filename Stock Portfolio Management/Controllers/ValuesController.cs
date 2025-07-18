using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Stock_Portfolio_Management.Data;
using Stock_Portfolio_Management.Model;

namespace Stock_Portfolio_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMongoCollection<Stock>? _stocks;

        public ValuesController(MongoDbService mongoDbService)
        {
            _stocks = mongoDbService.Database?.GetCollection<Stock>("stocks");

        }
        [HttpGet]
        public async Task<ActionResult<List<Stock>>> GetStocks()
        {
            var result = await _stocks.Find(_ => true).ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
         public async Task<ActionResult<Stock>> GetSingle([FromRoute]string id)
        {   
            var GetStock= await _stocks.Find(x => x.Id == id).FirstOrDefaultAsync();    
            return Ok(GetStock);
        }
       

        [HttpPost]  
        public async Task<ActionResult> InsertStock([FromBody] StocksDto Inputstock)
        {

            var NewStock = new Stock
            {
           
                Symbol = Inputstock.Symbol,
                BuyPrice = Inputstock.BuyPrice,
                CurrentPrice = Inputstock.CurrentPrice,
                Quantity = Inputstock.Quantity


            };

            await _stocks.InsertOneAsync(NewStock);
           return CreatedAtAction(nameof(GetSingle),new { id=NewStock.Id},NewStock);      
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateStock([FromRoute] string id, [FromBody] StocksDto stocksDto)
        {

            var filter = Builders<Stock>.Filter.Eq(s=>s.Id,id);
            var update = Builders<Stock>.Update.Set(s => s.BuyPrice, stocksDto.BuyPrice)
                .Set(s => s.Quantity, stocksDto.Quantity);
           
            var result = await _stocks.UpdateOneAsync(filter,update);
            return Ok(result);
           
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteStock([FromRoute] string id)
        {
            var filter = Builders<Stock>.Filter.Eq(s => s.Id, id);
            var result = await _stocks.DeleteOneAsync(filter);
            return Ok(result);
        }






    }
}
