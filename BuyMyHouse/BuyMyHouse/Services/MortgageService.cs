using BuyMyHouse.Model;
using BuyMyHouse.Repositories;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using System.Text;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BuyMyHouse.Services
{
    public class MortgageService : IMortgageService
    {
        private readonly IUserService _UserService;
        private readonly IBlob _Blob;
        private readonly string _ServiceBusConnectString;
        private readonly string _CreateQueueName;
        private readonly string _SendQueueName;

        public MortgageService(IUserService userService, IBlob blob)
        {
            _UserService = userService;
            _Blob = blob;
            _ServiceBusConnectString = Environment.GetEnvironmentVariable("AzureConnectionString");
            _CreateQueueName = Environment.GetEnvironmentVariable("ServiceBusNameCreate");
            _SendQueueName = Environment.GetEnvironmentVariable("ServiceBusNameSend");
        }

        public async Task AddMortgage(string queue)
        {
            var queues = JsonSerializer.Deserialize<List<string>>(queue);
            foreach (var userid in queues)
            {
                var user = await _UserService.GetUserById(Guid.Parse(userid));
                var mortgage = new Mortgage()
                {
                    Id = Guid.NewGuid(),
                    Email = user.Email,
                    Amount = user.Income,
                    ZipCode = user.ZipCode
                };
                var pdf = PDFMaker.CreatePDF(mortgage);
                user.PDF = await _Blob.CreateFile(Convert.ToBase64String(pdf), Guid.NewGuid() + ".pdf");
                await _UserService.UpdateUser(user);
            }
        }

        public async Task CreateMortgageQueue()
        {
            var users = await _UserService.GetUsers();
            if (!string.IsNullOrEmpty(_CreateQueueName))
            {
                IQueueClient client = new QueueClient(_ServiceBusConnectString, _CreateQueueName);
                var batchListOfUsers = Splitter.Split(users.ToList());
                foreach (var batchusers in batchListOfUsers)
                {
                    var messageBody = JsonConvert.SerializeObject(batchusers.Select(a => a.Id));
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                    await client.SendAsync(message);
                }
            }
        }

        public async Task SendEmailWithMortgages(string queueString)
        {
            var queues = JsonSerializer.Deserialize<List<string>>(queueString);
            foreach (var userid in queues)
            {
                var user = await _UserService.GetUserById(Guid.Parse(userid));
                var link = await _Blob.GetBlob(user.PDF);
                var email = new EmailAddress(user.Email);
                await SendEmail.Send(email, "your mortgage pdf", "", $"<a>'{link}</a>");
            }
        }

        public async Task SendMortgageQueue()
        {
            var users = await _UserService.GetUsers();

            if (!string.IsNullOrEmpty(_CreateQueueName))
            {
                IQueueClient client = new QueueClient(_ServiceBusConnectString, _SendQueueName);
                var batchListOfUsers = Splitter.Split(users.ToList());
                foreach (var batchusers in batchListOfUsers)
                {
                    var messageBody = JsonConvert.SerializeObject(batchusers.Select(a => a.Id));
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                    await client.SendAsync(message);
                }
            }
        }

        public async Task DeleteMortgage(string queueString)
        {
            var queues = JsonSerializer.Deserialize<List<string>>(queueString);
            foreach (var fileId in queues)
            {
                await _Blob.DeleteBlob(fileId);
            }
        }
    }
}
