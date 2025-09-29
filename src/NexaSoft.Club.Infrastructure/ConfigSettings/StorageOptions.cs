namespace NexaSoft.Club.Infrastructure.ConfigSettings;

public class StorageOptions
{
    public string ServiceUrl { get; set; } = default!;
    public string AccessKey { get; set; } = default!;
    public string SecretKey { get; set; } = default!;
    public string Bucket { get; set; } = default!;
}
