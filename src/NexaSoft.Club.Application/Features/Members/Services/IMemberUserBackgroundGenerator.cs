namespace NexaSoft.Club.Application.Features.Members.Services;

public interface IMemberUserBackgroundGenerator
{
    Task<long?> GenerateMemberUserAsync(long memberId, long userTypeId, string createdBy, CancellationToken cancellationToken = default);
}
