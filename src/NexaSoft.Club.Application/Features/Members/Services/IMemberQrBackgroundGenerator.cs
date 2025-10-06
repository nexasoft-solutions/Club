
namespace NexaSoft.Club.Application.Features.Members.Services;

public interface IMemberQrBackgroundGenerator
{
    Task GenerateMemberQrAsync(long memberId, string createdBy, CancellationToken cancellationToken);
}
