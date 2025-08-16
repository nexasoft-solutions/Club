namespace NexaSoft.Agro.Infrastructure.ConfigSettings;

public class JwtOptions
{
    public JwtOptions() { }

    public string Secret { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Expires { get; set; } = null!;
    
    public int GetExpiresInt() => int.TryParse(Expires, out var val) ? val : 8;
}


