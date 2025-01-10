using Microsoft.AspNetCore.Mvc;
using OpenMindProject.Services.EmailService;
using Google.Apis.Gmail.v1;

namespace OpenMindProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmailController(GmailService gmailService) : Controller
    {
		private readonly GmailService _gmailService = gmailService;
        [HttpGet]
        public IActionResult GetEmails()
        {
            var emails = new EmailService(_gmailService);
            var emailList = emails.EmailsToModel();
            return View("Index", emailList);
        }
    }
}
