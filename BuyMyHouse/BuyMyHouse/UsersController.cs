using BuyMyHouse.Model;
using BuyMyHouse.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace BuyMyHouse
{
    public class UsersController
    {
        private readonly IUserService _UserService;
        private ILogger _logger { get; }
        public UsersController(IUserService userService, ILogger logger)
        {
            _UserService = userService;
            _logger = logger;   
        }

        [Function("AddUser")]
        public async Task<HttpResponseData> SaveBuyer(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req, 
            FunctionContext executionContext)
        {
            _logger.LogInformation("Started saving a Buyes");
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var user = JsonConvert.DeserializeObject<User>(requestBody);
            await _UserService.AddUser(user);
            var response = req.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}
