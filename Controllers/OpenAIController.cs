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
                        $"Tâche : Réponds directement à cet email reçu par Openmind Technologies : \"{request.EmailContent}\". \n" +
                        $"1. Si l'email est pertinent par rapport aux services ou activités de l'entreprise, formule une réponse complète, chaleureuse, et professionnelle. Intègre les valeurs d'Openmind (humain, collaboration, innovation). \n" +
                        $"2. Si l'email ne peut pas recevoir de réponse adéquate, informe l'utilisateur de l'application (celui qui utilise cette boîte mail) qu'aucune réponse ne peut être générée pour cet email. \n" +
                        $"Ta réponse doit être concise, refléter le style d'Openmind, et éviter de répéter des informations évidentes (comme l'adresse mail). " +
                        $"Inclue une signature professionnelle en fin de réponse.";
            }

            return  $"Contexte : {context} \n" +
                    $"Tâche : Réponds directement à cet email reçu par Openmind Technologies : \"{request.EmailContent}\" " +
                    $"en tenant compte des instructions supplémentaires suivantes : {request.PromptContent}. \n" +
                    $"Assure-toi que ta réponse est concise, reflète le style d'Openmind, et intègre une signature professionnelle.";
        }
    }
}
