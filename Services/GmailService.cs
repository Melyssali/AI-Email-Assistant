using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using System.Text;

namespace OpenMindProject.Services.Gmail
{
    public class GmailAuthService
    {
        private static readonly string[] Scopes = { GmailService.Scope.GmailModify };
        private const string ApplicationName = "project-openmind";

        public GmailService AuthenticateGmail()
        {
            // Je récupère ce que j'ai stocké dans les variables d'env azure
            string credentialsJson = Environment.GetEnvironmentVariable("GOOGLE_CREDENTIALS");
            string tokenJson = Environment.GetEnvironmentVariable("GOOGLE_TOKEN");

            if (string.IsNullOrWhiteSpace(credentialsJson) || string.IsNullOrWhiteSpace(tokenJson))
            {
                throw new Exception("Les variables d'environnement GOOGLE_CREDENTIALS ou GOOGLE_TOKEN sont introuvables ou vides.");
            }

            var clientSecrets = GoogleClientSecrets.FromStream(new MemoryStream(Encoding.UTF8.GetBytes(credentialsJson)));

            var tokenResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenResponse>(tokenJson);
            if (tokenResponse == null)
            {
                throw new Exception("Impossible de désérialiser le token.");
            }
            // Créer les credentials OAuth2
            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = clientSecrets.Secrets,
                Scopes = Scopes
            });

            var credential = new UserCredential(flow, "user", tokenResponse);

            return new GmailService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });
        }
    }
}