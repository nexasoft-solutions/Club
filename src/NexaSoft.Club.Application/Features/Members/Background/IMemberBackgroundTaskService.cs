using NexaSoft.Club.Application.Features.Members.Commands.CreateMember;

namespace NexaSoft.Club.Application.Features.Members.Background;

public interface IMemberBackgroundTaskService
{
    Task QueueMemberFeesGenerationAsync(long memberId, CreateMemberCommand command, CancellationToken cancellationToken = default);
}
