namespace NexaSoft.Agro.Infrastructure.ConfigSettings;

public class JwtOptions
{
    public JwtOptions() {} 

    public string Secret { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public int Expires { get; set; } // en horas
}
/*public sealed record JwtOptions
(
    string Issuer,
    string Secret,
    int Expires
);*/

