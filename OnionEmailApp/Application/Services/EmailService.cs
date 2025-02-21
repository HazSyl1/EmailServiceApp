using OnionEmailApp.Application.Interfaces;
using OnionEmailApp.Domain.Interfaces;
using OnionEmailApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnionEmailApp.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly ISmtpClient _smtpClient;
        private readonly IRepository<Email> _emailRepository;

        public EmailService(ISmtpClient smtpClient, IRepository<Email> emailRepository)
        {
            _smtpClient = smtpClient;
            _emailRepository = emailRepository;
        }

        public async Task<string> SendOtpEmailAsync(string to, string name, string otp)
        {
            return await _smtpClient.SendOtpEmailAsync(to, name, otp);
        }

        public async Task SendOtpEmailToAllAsync(string otp)
        {
            var emails = await _emailRepository.GetAllAsync();
            var emailAddresses = new List<string>();

            foreach (var email in emails)
            {
                emailAddresses.Add(email.Address);
            }

            await _smtpClient.SendOtpEmailToAllAsync(emailAddresses, otp);
        }

        public async Task AddEmailAsync(string emailAddress)
        {
            var email = new Email { Address = emailAddress };
            await _emailRepository.AddAsync(email);
        }
    }
}