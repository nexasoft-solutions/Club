namespace NexaSoft.Club.Domain.Abstractions;

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


//Para comprobantes
public class ReceiptTemplateData
{
    public string OrganizationName { get; set; } = "CLUB CENTRO SOCIAL ICA";
    public string OrganizationAddress { get; set; } = "Calle Bolivar 166 - Ica";
    public string OrganizationContact { get; set; } = "Tel: 056-219198 / 231411";
    public string DocumentType { get; set; } = "Comprobante de Pago";
    public string DocumentNumber { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; } = DateTime.Now;
    public string CustomerName { get; set; } = string.Empty;
    public string Message { get; set; } = "Adjuntamos su comprobante generado recientemente.";
    public decimal TotalAmount { get; set; }
    public string AmountInWords { get; set; } = string.Empty;
    public string Reference { get; set; } = string.Empty;
    public string AdditionalNotes { get; set; } = "Este comprobante es un documento oficial que acredita la transacción en nuestro sistema.";
    public List<ReceiptItemRecord> Items { get; set; } = new List<ReceiptItemRecord>();
}

public class ReceiptItemRecord
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}

public class NotificationTemplateData
{
    public string Title { get; set; } = string.Empty;
    public string Greeting { get; set; } = "Estimado usuario,";
    public string Message { get; set; } = string.Empty;
    public bool IsUrgent { get; set; } = false;
    public string OrganizationName { get; set; } = "CLUB CENTRO SOCIAL ICA";
    public string? ActionUrl { get; set; }
    public string? ActionText { get; set; }
    public string FooterNotes { get; set; } = "Este es un mensaje automático, por favor no responda a este correo.";
    public List<KeyValuePair<string, string>> Details { get; set; } = new List<KeyValuePair<string, string>>();
}