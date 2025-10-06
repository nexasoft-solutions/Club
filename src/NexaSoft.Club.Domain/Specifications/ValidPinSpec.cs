using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Domain.Specifications;

public class ValidPinSpec: BaseSpecification<MemberPin>
{
    public ValidPinSpec(long memberId, string pin, string deviceId) 
        : base(p => p.MemberId == memberId && 
                   p.Pin == pin && 
                   p.DeviceId == deviceId &&
                   !p.IsUsed && 
                   p.ExpiresAt > DateTime.UtcNow)
    {
        AddInclude(p => p.Member);
        AddInclude($"{nameof(MemberPin.Member)}.{nameof(Member.MemberType)}");
    }
}