using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;

namespace NexaSoft.Club.Infrastructure.Services;

public class VaultService
{
   private readonly IVaultClient _vaultClient;
    private readonly string _mountPoint;

    public VaultService(string vaultAddress, string token, string mountPoint = "secret")
    {
        var authMethod = new TokenAuthMethodInfo(token);
        var vaultClientSettings = new VaultClientSettings(vaultAddress, authMethod);
        _vaultClient = new VaultClient(vaultClientSettings);
        _mountPoint = mountPoint;
    }

    public async Task<Dictionary<string, string>> GetSecretsAsync(string path)
    {
        var secret = await _vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(path, mountPoint: _mountPoint);

        if (secret?.Data?.Data == null)
        {
            throw new InvalidOperationException($"No se encontraron datos en la ruta {path}");
        }

        return secret.Data.Data.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value?.ToString() ?? string.Empty
        );
    }

    public async Task<T?> GetSecretAsObjectAsync<T>(string path)
    {
        var secret = await _vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(path, mountPoint: _mountPoint);

        if (secret?.Data?.Data == null)
        {
            throw new InvalidOperationException($"No se encontraron datos en la ruta {path}");
        }

        var json = System.Text.Json.JsonSerializer.Serialize(secret.Data.Data);
        return System.Text.Json.JsonSerializer.Deserialize<T>(json);
    }

}
