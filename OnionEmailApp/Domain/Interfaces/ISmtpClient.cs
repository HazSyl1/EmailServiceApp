using System.Threading.Tasks;

namespace OnionEmailApp.Domain.Interfaces
{
    public interface ISmtpClient
    {
        Task<string> SendOtpEmailAsync(string to, string name, string otp);
        Task SendOtpEmailToAllAsync(IEnumerable<string> emails, string otp);

    }
}