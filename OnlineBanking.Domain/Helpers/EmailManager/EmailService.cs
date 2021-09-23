using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace OnlineBanking.Domain.Helpers.EmailManager
{
    public class EmailSender 
    {
        public EmailSender(IOptions<EmailSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public EmailSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.ApiKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                // should be a domain other than yahoo.com, outlook.com, hotmail.com, gmail.com
                From = new EmailAddress("donotreply@somewhere.com", "IndieGamesLab"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            return client.SendEmailAsync(msg);
        }
    }

}
