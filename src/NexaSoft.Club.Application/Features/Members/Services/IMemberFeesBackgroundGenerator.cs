using NexaSoft.Club.Application.Features.Members.Commands.CreateMember;

namespace NexaSoft.Club.Application.Features.Members.Services;

public interface IMemberFeesBackgroundGenerator
{
    Task GenerateMemberFeesAsync(MemberFeesBackgroundData data, CancellationToken cancellationToken);
}
