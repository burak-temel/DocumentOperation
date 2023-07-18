using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;
using DocumentOperation.ServiceContracts;

namespace DocumentOperation.Services
{
    public class EmailService:IEmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;


        public EmailService(IConfiguration configuration)
        {
            _smtpHost = configuration.GetConnectionString("SmtpHost");
            _smtpPort = int.Parse(configuration.GetConnectionString("SmtpPort"));
            _smtpUsername = configuration.GetConnectionString("SmtpUsername");
            _smtpPassword = configuration.GetConnectionString("SmtpPassword");
        }

        public async Task SendEmail(string recipientEmail, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Your Name", _smtpUsername));
                message.To.Add(new MailboxAddress("", recipientEmail));
                message.Subject = subject;
                message.Body = new TextPart("plain")
                {
                    Text = body
                };

                using var client = new SmtpClient();
                await client.ConnectAsync(_smtpHost, _smtpPort, useSsl: false);
                await client.AuthenticateAsync(_smtpUsername, _smtpPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while sending the email: {ex.Message}");
            }
        }
    }
}
