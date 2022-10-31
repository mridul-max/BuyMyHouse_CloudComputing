using BuyMyHouse.Services;
using iTextSharp.text.log;
using Microsoft.Azure.Functions.Worker;

namespace BuyMyHouse
{
    class MortgageQueue
    {
        private readonly IMortgageService _mortgageService;
        public MortgageQueue(IMortgageService mortgageService)
        {
            _mortgageService = mortgageService;
        }

        [Function("MortgageCalCulation")]
        public async Task MortgageCalCulation([TimerTrigger("0 0 0 * * *")] TimerInfo timerInfo, FunctionContext context, ILogger log)
        {
            await _mortgageService.CreateMortgageQueue();
        }

        [Function("SendMails")]
        public async Task SendMails([TimerTrigger("0 0 4 * * *")] TimerInfo timerInfo, ILogger log)
        {
            await _mortgageService.SendMortgageQueue();
        }

        [Function("CalculateMortgages")]
        public async Task CalculateMortgages([ServiceBusTrigger("mortgages", Connection = "AzureConnectionString")] string myQueueItem, FunctionContext context)
        {
            await _mortgageService.AddMortgage(myQueueItem);
        }

        [Function("SendMortgages")]
        public async Task SendMortgages([ServiceBusTrigger("SendPDF", Connection = "AzureConnectionString")] string myQueueItem, FunctionContext context)
        {
            await _mortgageService.SendEmailWithMortgages(myQueueItem);
        }
    }
}
