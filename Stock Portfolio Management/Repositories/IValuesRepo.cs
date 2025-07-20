using Stock_Portfolio_Management.Model;

namespace Stock_Portfolio_Management.Repositories
{
    public interface IValuesRepo
    {
      Task<List<Stock>> GetStocksAsync();   
      Task<Stock> GetSingleAsync(string id);
      Task<Stock> UpdateStockAsync(string id,Stock stocksDto);
      Task<Stock> DeleteStockAsync(string id);
      Task<Stock> InsertStockAsync(Stock Inputstock);
    }
}
