using Google.Apis.Gmail.v1;
using Microsoft.VisualBasic;
using OpenMindProject.Models.Emails;
using System.Text;

// Cette requete retourne que id et thread Id d'après la doc,
// il faut utiliser la méthode "Get" pour les détails
// _gmailService.Users.Messages.List("me");
// Pour récupérer les détails des messages
// _gmailService.Users.Messages.Get("me", message.Id).Execute();
// Dans la doc il est dit que le texte des emails est encodé

namespace OpenMindProject.Services.EmailService
{
	public class EmailService
	{
		private readonly GmailService _gmailService;
		public EmailService(GmailService gmailService)
		{
			ArgumentNullException.ThrowIfNull(gmailService);
			_gmailService = gmailService;
		}
		public List<EmailsModel> EmailsToModel()
		{
				var emailModels = new List<EmailsModel>();
				try
				{
					var request = _gmailService.Users.Messages.List("me");
					var response = request.Execute();
					if (response.Messages != null)
					{
						foreach (var message in response.Messages)
						{
							var email = _gmailService.Users.Messages.Get("me", message.Id).Execute();
							var subject = email.Payload.Headers.FirstOrDefault(h => h.Name == "Subject")?.Value ?? "Sans objet";
							var sender = email.Payload.Headers.FirstOrDefault(h => h.Name == "From")?.Value ?? "Non défini";
							long? internalDate = email.InternalDate;
							string content = ExtractContent(email);
							emailModels.Add(new EmailsModel
							{
								EmailId = message.Id,
								Sender = sender,
								Subjects = subject,
								Content = content,
								Date = internalDate
							});
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Erreur : {ex.Message}");
				}
				return emailModels;		
		}
		public string ExtractContent(Google.Apis.Gmail.v1.Data.Message email)
		{
			if (email.Payload.Body?.Data != null)
				return DecodeBase64Url(email.Payload.Body.Data);
			if (email.Payload.Parts != null)
			{
				foreach (var part in email.Payload.Parts)
				{
					if (part.MimeType == "text/plain" || part.MimeType == "text/html")
						return DecodeBase64Url(part.Body.Data);
				}
			}
			return "On a rien trouvé, c'est triste...";
		}
        private static string DecodeBase64Url(string base64Url)
        {
            try
            {
                // Remplace les caractères spécifiques à Base64URL
                string base64 = base64Url.Replace('-', '+').Replace('_', '/');
                // Ajoute le padding manquant si nécessaire
                base64 = base64.PadRight(base64.Length + (4 - base64.Length % 4) % 4, '=');
                // Décode le Base64 en bytes
                byte[] bytes = Convert.FromBase64String(base64);
                // Convertit les bytes en chaîne UTF-8
                return Encoding.UTF8.GetString(bytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur de décodage : {ex.Message}");
                return "Erreur lors du décodage du contenu";
            }
        }
	}
}
