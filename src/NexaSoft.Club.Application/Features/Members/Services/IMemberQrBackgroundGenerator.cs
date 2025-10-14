
namespace NexaSoft.Club.Application.Features.Members.Services;

public interface IMemberQrBackgroundGenerator
{
    Task GenerateUserQrAsync(long userId, long memberId, string createdBy, CancellationToken cancellationToken);
}
