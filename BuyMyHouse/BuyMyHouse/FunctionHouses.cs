using System.IO;
using System.Net;
using BuyMyHouse.Model;
using BuyMyHouse.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BuyMyHouse
{
    public class FunctionHouses
    {
        private readonly IHouseService _HouseService;
        public FunctionHouses(IHouseService houseService)
        {
            _HouseService = houseService;
        }

        [Function("GetHouses")]
        public async Task<HttpResponseData> GetAllHouses([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var houses = _HouseService.GetHouses();
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync(JsonConvert.SerializeObject(houses));
            return response;
        }

        [Function("GetHouseById")]
        public async Task<HttpResponseData> GetHouseId([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
            FunctionContext executionContext, Guid Id)
        {
            var house = _HouseService.GetHouseById(Id);
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync(JsonConvert.SerializeObject(house));
            return response;
        }

        [Function("GetHousesByPrice")]
        public async Task<HttpResponseData> GetHousesByPriceAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            FunctionContext executionContext, int priceFrom, int priceTo)
        {
            var houses = _HouseService.GetHousesBetweenPrice(priceFrom, priceTo);
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync(JsonConvert.SerializeObject(houses));
            return response;
        }

        [Function("AddHouse")]
        public async Task<HttpResponseData> AddHouse([HttpTrigger(AuthorizationLevel.Anonymous, "post")] 
        HttpRequestData req, FunctionContext executionContext)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var house = JsonConvert.DeserializeObject<House>(requestBody);
            var addedHouse = await _HouseService.AddHouse(house);
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync(JsonConvert.SerializeObject(addedHouse));
            return response;
        }

    }
}
