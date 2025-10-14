using NexaSoft.Club.Domain.Features.Members;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Specifications;

public class MemberByDniSpec: BaseSpecification<Member>
{
    public MemberByDniSpec(string dni) 
        : base(m => m.Dni == dni && m.StatusId == (int)StatusEnum.Activo)
    {
        AddInclude(m => m.MemberType!);
    }
}
