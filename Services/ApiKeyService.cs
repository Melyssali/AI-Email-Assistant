using System;
using Microsoft.Extensions.Configuration;

namespace OpenMindProject.Services.ApiKey;

public class ApiKeyService
{
    private readonly IConfiguration _configuration;
    public string _apiKey = string.Empty;
    public ApiKeyService(IConfiguration configuration)
    {
        // if (configuration == null)
        //     throw new ArgumentNullException(nameof(configuration));
        // depuis C#10: https://learn.microsoft.com/fr-fr/dotnet/fundamentals/code-analysis/quality-rules/ca1510
         ArgumentNullException.ThrowIfNull(configuration);
        _apiKey = configuration["ApiSettings:ApiKey"];
    }
    public string GetApi() => _apiKey;
}
