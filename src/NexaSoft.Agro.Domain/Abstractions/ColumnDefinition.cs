namespace NexaSoft.Agro.Domain.Abstractions;

public class ColumnDefinition
{
    public string PropertyName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool ShowInHighlight { get; set; } = false;
    public string? ValueFormat { get; set; } // Para formatear valores (ej: "C" para currency)
}


public class DetailSectionDefinition
{
    public string PropertyName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public List<ColumnDefinition> ColumnDefinitions { get; set; } = new();
    public int Order { get; set; }
}