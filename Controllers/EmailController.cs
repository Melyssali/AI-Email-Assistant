using Microsoft.AspNetCore.Mvc;
using OpenMindProject.Services.EmailService;
using Google.Apis.Gmail.v1;

namespace OpenMindProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmailController : Controller
    {
		private readonly GmailService _gmailService;
		private readonly EmailService _emailService;
        public EmailController(GmailService gmailService, EmailService emailService)
        {
            _gmailService = gmailService;
            _emailService = emailService;
        }
        [HttpGet]
        public IActionResult GetEmails()
        {
            var emailList = _emailService.EmailsToModel();
            return View("Index", emailList);
        }
    }
}
