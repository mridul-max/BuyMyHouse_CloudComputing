using BuyMyHouse.Model;
using BuyMyHouse.Services;
using BuyMyHouse.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid.Helpers.Errors.Model;
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
        public async Task<IActionResult> SaveBuyer(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req, 
            FunctionContext executionContext)
        {
            UserValidation validator = new UserValidation();
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var user = JsonConvert.DeserializeObject<User>(requestBody);
            var validationResult = validator.Validate(user);
            if (!validationResult.IsValid)
            {
                return new BadRequestObjectResult(validationResult.Errors.Select(e => new {
                    Field = e.PropertyName,
                    Error = e.ErrorMessage
                }));
            }
            _logger.LogInformation("Started saving a Buyes");
            var response = _UserService.AddUser(user);
            return new CreatedAtActionResult("","",null, response);
        }
    }
}
