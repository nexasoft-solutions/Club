using NexaSoft.Club.Domain.Features.Members;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Specifications;

public class MemberByDniAndBirthDateSpec: BaseSpecification<Member>
{
    public MemberByDniAndBirthDateSpec(string dni, DateOnly birthDate) 
        : base(m => m.Dni == dni && m.BirthDate == birthDate && m.StatusId == (int)StatusEnum.Activo)
    {
        AddInclude(m => m.MemberType!);
    }
}
