using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Domain.Specifications;

public class MemberByDniAndBirthDateSpec: BaseSpecification<Member>
{
    public MemberByDniAndBirthDateSpec(string dni, DateOnly birthDate) 
        : base(m => m.Dni == dni && m.BirthDate == birthDate && m.Status == "Active")
    {
        AddInclude(m => m.MemberType!);
    }
}
