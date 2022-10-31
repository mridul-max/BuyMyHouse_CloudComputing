using SendGrid;
using SendGrid.Helpers.Mail;
using System;

namespace BuyMyHouse.Services
{
    public static class SendEmail
    {
        public static async Task Send(EmailAddress to, string subject, string body, string htmlContent, string b64Content = null, string attachmentName = null)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var email = Environment.GetEnvironmentVariable("EMAIL");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(email, null);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, htmlContent);
            if (!string.IsNullOrEmpty(attachmentName))
            {
                msg.AddAttachment(attachmentName, b64Content);
            }
            await client.SendEmailAsync(msg);
        }
    }
}
