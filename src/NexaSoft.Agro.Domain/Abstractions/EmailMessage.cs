namespace NexaSoft.Agro.Domain.Abstractions;

public class EmailMessage
{
    public string To { get; set; } = string.Empty;
    public string? ToName { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string HtmlContent { get; set; } = string.Empty;
    public string? TextContent { get; set; }
    public List<EmailAttachment>? Attachments { get; set; }
    public bool IsImportant { get; set; }
    public List<string>? CC { get; set; }
    public List<string>? BCC { get; set; }
}

public class EmailAttachment
{
    public string Name { get; set; } = string.Empty;
    public byte[] Content { get; set; } = Array.Empty<byte>();
    public string ContentType { get; set; } = "application/octet-stream";
}


public class TemplateData
{
    public string Title { get; set; } = string.Empty;
    public string Greeting { get; set; } = string.Empty;
    public string MainContent { get; set; } = string.Empty;
    public List<KeyValuePair<string, string>> Details { get; set; } = new();
    public string ActionText { get; set; } = string.Empty;
    public string ActionUrl { get; set; } = string.Empty;
    public string Footer { get; set; } = string.Empty;
    public bool IsUrgent { get; set; }
}