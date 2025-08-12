using NexaSoft.Agro.Application.Masters.Auths.Queries.AuthToken;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Auth;
using NexaSoft.Agro.Domain.Masters.Users;

namespace NexaSoft.Agro.Application.Abstractions.Auth;

public interface IAuthService
{
    Task<Result<AuthTokenResponse>> Login(string userName, string password, CancellationToken cancellationToken);
    Task<Result<User?>> GetByUserNameAsync(string userName, CancellationToken cancellationToken);
    Task<Result<User?>> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task<Result<RefreshToken>> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);

    Task<Result<AuthTokenResponse>> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);

    Task<string> GenerateAccessToken(User user, Guid activeRoleId, CancellationToken cancellationToken);
    
    Task<Result<AuthTokenResponse>> ChangeActiveRoleAsync(Guid userId, Guid newRoleId, CancellationToken cancellationToken);
}
