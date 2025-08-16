using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;

namespace NexaSoft.Agro.Infrastructure.Services;

public class VaultTokenRenewer
{
    private readonly IVaultClient _vaultClient;
    private readonly string _token;
    private CancellationTokenSource? _cts;

    public VaultTokenRenewer(string vaultAddress, string token)
    {
        _token = token;
        var authMethod = new TokenAuthMethodInfo(token);
        _vaultClient = new VaultClient(new VaultClientSettings(vaultAddress, authMethod));
    }

    public void StartRenewing(TimeSpan renewBeforeExpiry)
    {
        _cts = new CancellationTokenSource();

        Task.Run(async () =>
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                try
                {
                    var tokenLookup = await _vaultClient.V1.Auth.Token.LookupSelfAsync();
                    DateTime expireTime;

                    if (!string.IsNullOrWhiteSpace(tokenLookup.Data.ExpireTime) &&
                        DateTime.TryParse(tokenLookup.Data.ExpireTime, out var parsedExpireTime))
                    {
                        expireTime = parsedExpireTime;
                    }
                    else
                    {
                        expireTime = DateTime.UtcNow.AddMinutes(10);
                    }

                    var delay = expireTime - DateTime.UtcNow - renewBeforeExpiry;

                    if (delay <= TimeSpan.Zero)
                        delay = TimeSpan.FromMinutes(1);

                    await Task.Delay(delay, _cts.Token);
                    await _vaultClient.V1.Auth.Token.RenewSelfAsync();

                    Console.WriteLine("Token Vault renovado correctamente");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error renovando token Vault: {ex.Message}");
                }
            }
        }, _cts.Token);
    }

    public void StopRenewing()
    {
        _cts?.Cancel();
    }
}
