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
            ArgumentNullException.ThrowIfNull(nameof(apiKeyService));
            ArgumentNullException.ThrowIfNull(nameof(filetxt));
            _apiKeyService = apiKeyService;
            _file = filetxt;
            var apiKey = _apiKeyService.GetApi();
            // _chatClient = new ChatClient("gpt-4", apiKey);
            _chatClient = new ChatClient("gpt-3.5-turbo", apiKey);
        }
        [HttpPost("SendAnswer")]
        public IActionResult SendAnswer([FromBody] EmailRequest request)
        {
            var context = _file.ReadFile();
            var prompt = CreatePrompt(request, context);
            ChatCompletion completion = _chatClient.CompleteChat(prompt);
            var response = completion.Content[0].Text;
            return Ok(new { response });
        }
        private string CreatePrompt(EmailRequest request, string context)
        {
            if (string.IsNullOrEmpty(request.PromptContent))
            {
                return  $"Contexte : {context} \n" +
                        $"Tâche : Réponds directement à cet email reçu par Openmind Technologies : \"{request.EmailContent}\". \n";
            }
            return  $"Contexte : {context} \n" +
                    $"Tâche : Réponds directement à cet email reçu par Openmind Technologies : \"{request.EmailContent}\" " +
                    $"en tenant compte des instructions supplémentaires suivantes : {request.PromptContent}.";
        }
    }
}
