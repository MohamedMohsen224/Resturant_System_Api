using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Resturant_Api_Core.Services.AuthServices;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Services.AuthService
{
    public class EmailSender : IEmailSender
    {

        private readonly SendGridClient client;
        public EmailSender(IConfiguration configuration )
        {
            var apiKey = configuration["SendGrid"];
            client = new SendGridClient(apiKey);
        }
        public async Task SendEmailAsync(string email, string subject, string Message)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("Resturant@gmail.com","Resturant"),
                Subject = subject,
                PlainTextContent = Message,
                HtmlContent = Message
            };
            msg.AddTo(new EmailAddress(email));
            var Response = await client.SendEmailAsync(msg);
            if (Response.StatusCode != System.Net.HttpStatusCode.Accepted) 
            { 
             throw new InvalidOperationException("Failed to send email");
            };
        }
    }
}
