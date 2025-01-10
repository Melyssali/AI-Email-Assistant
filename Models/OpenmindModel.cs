using System;

namespace OpenMindProject.Models.EmailRequest;

public class EmailRequest
{
    public string EmailContent { get; set; } = string.Empty;
    public string PromptContent { get; set; } = string.Empty;
}
