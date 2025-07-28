using Microsoft.AspNetCore.Mvc;
using Portfolio_Management_UI.Models;
using System.Collections;
using System.Text;
using System.Text.Json;

namespace Portfolio_Management_UI.Controllers
{
    public class ValueUiController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ValueUiController(IHttpClientFactory httpClient) {
            httpClientFactory = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            List<StockGetDto> AllStocks = new List<StockGetDto>();
            try
            {
                var client = httpClientFactory.CreateClient();
                var getResponse = await client.GetAsync("https://localhost:7034/api/Values");
                getResponse.EnsureSuccessStatusCode();
                AllStocks.AddRange(await getResponse.Content.ReadFromJsonAsync<IEnumerable<StockGetDto>>());

            }
            catch (Exception ex) { throw; }
            return View(AllStocks);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(StockDto stockDto)
        {
            var client = httpClientFactory.CreateClient();
            var content = new StringContent(
        JsonSerializer.Serialize(stockDto),
        Encoding.UTF8,
        "application/json"
          );
            var httpreq = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7034/api/Values"),
                Content = content,
            };
            var httpresponsemsg = await client.SendAsync(httpreq);
            httpresponsemsg.EnsureSuccessStatusCode();
            var response = httpresponsemsg.Content.ReadFromJsonAsync<StockDto>();
            if (response != null)
            {
                return RedirectToAction("Index", "ValueUi");
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {  
            var client = httpClientFactory.CreateClient();
            var httpResp = await client.GetFromJsonAsync<StockGetDto>($"https://localhost:7034/api/Values/{id.ToString()}");
            if (httpResp != null) { 
            return View(httpResp);
            }
            return View(null);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(StockGetDto stockGetDto)
        {
            var client = httpClientFactory.CreateClient();
            var httpreqmsg = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7034/api/Values/{stockGetDto.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(stockGetDto), Encoding.UTF8, "application/json")
            };
            var Responsemsg = await client.SendAsync(httpreqmsg);
            Responsemsg.EnsureSuccessStatusCode();
            var Response = await Responsemsg.Content.ReadFromJsonAsync<StockGetDto>();
            if (Response != null)
            {
                return RedirectToAction("Index", "ValueUi");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpresp = await client.DeleteAsync($"https://localhost:7034/api/Values/{id.ToString()}");
                httpresp.EnsureSuccessStatusCode(); 
                return RedirectToAction("Index", "ValueUi");
            }
            catch (Exception ex) {
            throw(ex);  
            }
            return View("Edit") ;
        }
    } 
}
