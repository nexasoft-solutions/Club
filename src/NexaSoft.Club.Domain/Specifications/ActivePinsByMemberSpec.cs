using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Domain.Specifications;

public class ActivePinsByMemberSpec: BaseSpecification<MemberPin>
{
    public ActivePinsByMemberSpec(long memberId) 
        : base(p => p.MemberId == memberId && !p.IsUsed && p.ExpiresAt > DateTime.UtcNow)
    {
    }
}
