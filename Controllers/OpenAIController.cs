using Microsoft.AspNetCore.Mvc;
using OpenMindProject.Services.ApiKey;
using OpenMindProject.Models.EmailRequest;
using OpenMindProject.Services.FileReader;
using OpenAI.Chat;

namespace OpenMindProject.Controllers.OpenAI
{ 
    [Route("[controller]")]
    [ApiController]
    public class OpenAIController : Controller
    {
        private readonly ApiKeyService _apiKeyService;
        private readonly ChatClient _chatClient;
        private readonly FileReaderService _file;
        public OpenAIController(ApiKeyService apiKeyService, FileReaderService filetxt)
        {
            _apiKeyService = apiKeyService;
            _file = filetxt;
            var apiKey = _apiKeyService.GetApi();
            _chatClient = new ChatClient("gpt-3.5-turbo", apiKey);
        }
        [HttpPost("SendAnswer")]
        public IActionResult SendAnswer([FromBody] EmailRequest request)
        {
            var context = _file.ReadFile();
            var prompt = string.Empty;
            if (string.IsNullOrEmpty(request.PromptContent))
            {
               prompt = $"Contexte : {context} Tâche : Voici l'email auquel il faut répondre : {request.EmailContent}. Si ce mail a un rapport avec ce que fait l'entreprise alors base toi sur le contexte et génère une réponse d'e-mail professionnel. Si non ou si tu ne connais pas la réponse alors génère un message d'erreur en précisant que tu ne peux pas générer de réponse.";
            }
            else
            {
                prompt = $"Contexte : {context} Tâche : Voici l'email auquel il faut répondre : {request.EmailContent}. Genere une réponse en suivant ces informations : {request.PromptContent}";
            }
            ChatCompletion completion = _chatClient.CompleteChat(prompt);
            var response = completion.Content[0].Text;
            return Ok(new { response });
        }
    }
}
