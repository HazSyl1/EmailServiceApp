using Microsoft.AspNetCore.Mvc;
using OnionEmailApp.Application.Interfaces;
using System.Threading.Tasks;

namespace OnionEmailApp.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public IActionResult SendOtp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendOtp(string email, string name)
        {
            var otp = new Random().Next(100000, 999999).ToString();
            await _emailService.SendOtpEmailAsync(email, name, otp);
            return Json(new { otp });
        }

        [HttpPost]
        public async Task<IActionResult> SendOtpToAll()
        {
            var otp = new Random().Next(100000, 999999).ToString();
            await _emailService.SendOtpEmailToAllAsync(otp);
            return Json(new { otp });
        }

        [HttpPost]
        public async Task<IActionResult> AddEmail(string emailAddress)
        {
            await _emailService.AddEmailAsync(emailAddress);
            return Json(new { success = true });
        }
    }
}