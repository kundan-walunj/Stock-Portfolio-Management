using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Stock_Portfolio_Management.Data;
using Stock_Portfolio_Management.Model;
using Stock_Portfolio_Management.Repositories;

namespace Stock_Portfolio_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IValuesRepo _valuesRepo;
        private readonly IMapper _mapper;

        public ValuesController(IValuesRepo valuesRepo, IMapper mapper)
        {
            _valuesRepo = valuesRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<Stock>>> GetStocks()
        {
            var result = await _valuesRepo.GetStocksAsync();
            var Tostocksdto= _mapper.Map<List<StocksDto>>(result);
            return Ok(Tostocksdto);
        }

        [HttpGet("{id}")]
         public async Task<ActionResult<Stock>> GetSingle([FromRoute]string id)
        {
            
            var GetStock= await _valuesRepo.GetSingleAsync(id); 
            if (GetStock == null)
            {
                return NotFound();  
            }
            var Tostocksdto = _mapper.Map<StocksDto>(GetStock);
            return Ok(Tostocksdto);
        }
       

        [HttpPost]  
        public async Task<ActionResult> InsertStock([FromBody] StocksDto Inputstock)
        {

            //var NewStock = new Stock
            //{

            //    Symbol = Inputstock.Symbol,
            //    BuyPrice = Inputstock.BuyPrice,
            //    CurrentPrice = Inputstock.CurrentPrice,
            //    Quantity = Inputstock.Quantity


            //};
            var NewStock=_mapper.Map<Stock>(Inputstock);
            var value = await _valuesRepo.InsertStockAsync(NewStock);
            var Tostocksdto = _mapper.Map<StocksDto>(value);
            return CreatedAtAction(nameof(GetSingle),new { id=value.Id},Tostocksdto);      
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateStock([FromRoute] string id, [FromBody] StocksDto stocksDto)
        {


            //var Updatestock = new Stock
            //{
            //    Id = id,
            //    Symbol = stocksDto.Symbol,
            //    BuyPrice = stocksDto.BuyPrice,
            //    CurrentPrice = stocksDto.CurrentPrice,
            //    Quantity =stocksDto.Quantity


            //};
            var Updatestock=_mapper.Map<Stock>(stocksDto);  
            var result = await _valuesRepo.UpdateStockAsync(id, Updatestock);
            var Tostocksdto = _mapper.Map<StocksDto>(result);
            return Ok(Tostocksdto);
           
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteStock([FromRoute] string id)
        {
          
            var result = await _valuesRepo.DeleteStockAsync(id);
            var Tostocksdto = _mapper.Map<StocksDto>(result);
            return Ok(Tostocksdto);
        }






    }
}
