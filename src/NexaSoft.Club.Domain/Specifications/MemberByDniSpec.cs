using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Domain.Specifications;

public class MemberByDniSpec: BaseSpecification<Member>
{
    public MemberByDniSpec(string dni) 
        : base(m => m.Dni == dni && m.Status == "Active")
    {
        AddInclude(m => m.MemberType!);
    }
}
