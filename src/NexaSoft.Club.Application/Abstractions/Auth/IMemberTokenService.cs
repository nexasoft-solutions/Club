using NexaSoft.Club.Application.Features.Members.Services;
using NexaSoft.Club.Domain.Auth;
using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Application.Abstractions.Auth;

public interface IMemberTokenService
{
    Task<Result<MemberTokenResponse>> GenerateMemberToken(Member member, QrData qrData, CancellationToken cancellationToken);
    Task<Result<MemberTokenResponse>> GenerateMemberTokenWithPassword(Member member, string password, QrData qrData, CancellationToken cancellationToken);
    Task<Result<MemberTokenResponse>> RefreshMemberToken(string refreshToken, CancellationToken cancellationToken);
    Task<Result<MemberRefreshToken>> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
}
