using System.Threading.Tasks;

namespace OnionEmailApp.Application.Interfaces
{
    public interface IEmailService
    {
        Task<string> SendOtpEmailAsync(string to, string name, string otp);
        Task SendOtpEmailToAllAsync(string otp);
        Task AddEmailAsync(string emailAddress);
    }
}