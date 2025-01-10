// doc : https://github.com/googleworkspace/dotnet-samples/blob/main/gmail/GmailQuickstart/GmailQuickstart.cs

using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace OpenMindProject.Services.Gmail;

public class GmailAuthService
{
	private readonly string[] _scopes = [GmailService.Scope.GmailModify];
	private readonly string _applicationName = "project-openmind";

	public GmailService AuthenticateGmail()
	{
		try
		{
			UserCredential credential;
			// Load client secrets.
			using (var stream =
				   new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
			{
				string credPath = "token.json";
				credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
					GoogleClientSecrets.FromStream(stream).Secrets,
					_scopes,
					"user",
					CancellationToken.None,
					new FileDataStore(credPath, true)).Result;
				Console.WriteLine("Credential file saved to: " + credPath);
			}
			// return created Gmail API service
			return new GmailService(new BaseClientService.Initializer
			{
				HttpClientInitializer = credential,
				ApplicationName = _applicationName
			});
		}
		catch (FileNotFoundException e)
		{
			Console.WriteLine(e.Message);
			throw;
		}
	}
}