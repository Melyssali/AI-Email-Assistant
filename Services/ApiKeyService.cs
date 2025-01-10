using System;
using Microsoft.Extensions.Configuration;

namespace OpenMindProject.Services.ApiKey;

public class ApiKeyService
{
    private readonly IConfiguration _configuration;
    public string ApiKey = string.Empty;
    public ApiKeyService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GetApi()
    {
        ApiKey = _configuration["ApiSettings:ApiKey"];
        return ApiKey;
    }
}
