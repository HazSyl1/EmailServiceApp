using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnionEmailApp.Domain.Interfaces;
using ISmtpClient = OnionEmailApp.Domain.Interfaces.ISmtpClient;

namespace OnionEmailApp.Infrastructure.Services
{
    public class SmtpClient : ISmtpClient
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;

        public SmtpClient(string smtpHost, int smtpPort, string smtpUsername, string smtpPassword)
        {
            _smtpHost = smtpHost;
            _smtpPort = smtpPort;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
        }

        public async Task<string> SendOtpEmailAsync(string to, string name, string otp)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("ProInteriors - Contact Verification", _smtpUsername));
            message.To.Add(new MailboxAddress(name, to));
            message.Subject = "ProInteriors - Verify Your Email";

            message.Body = new TextPart("plain")
            {
                Text = $"Your One-Time Password (OTP) for verification is: {otp}"
            };

            using var client = new MailKit.Net.Smtp.SmtpClient();
            await client.ConnectAsync(_smtpHost, _smtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_smtpUsername, _smtpPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            return otp;
        }

        public async Task SendOtpEmailToAllAsync(IEnumerable<string> emails, string otp)
        {
            using var client = new MailKit.Net.Smtp.SmtpClient();
            await client.ConnectAsync(_smtpHost, _smtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_smtpUsername, _smtpPassword);

            foreach (var email in emails)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("ProInteriors - Contact Verification", _smtpUsername));
                message.To.Add(new MailboxAddress("Customer", email));
                message.Subject = "ProInteriors - Verify Your Email";

                message.Body = new TextPart("plain")
                {
                    Text = $"Your One-Time Password (OTP) for verification is: {otp}"
                };

                await client.SendAsync(message);
            }

            await client.DisconnectAsync(true);
        }
    }
}