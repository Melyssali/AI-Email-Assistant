using System;
// using Microsoft.Extensions.Configuration;

namespace OpenMindProject.Services.ApiKey;

public class ApiKeyService
{
    string? _apiKey = Environment.GetEnvironmentVariable("API_OPENAI");
    public string GetApi() => _apiKey;
}
