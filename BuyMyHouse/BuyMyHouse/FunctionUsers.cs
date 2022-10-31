using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace CloudDatabases
{
    public class FunctionUsers
    {
        private readonly IUserService _UserService;
        private ILogger _logger { get; }
        public FunctionUsers(IUserService userService, ILogger logger)
        {
            _UserService = userService;
            _logger = logger;   
        }

        [Function("AddUser")]
        public async Task<HttpResponseData> SaveBuyer(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req, 
            FunctionContext executionContext)
        {
            log.
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var user = JsonConvert.DeserializeObject<User>(requestBody);
            await _UserService.AddUser(user);

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            return response;
        }
    }
}
