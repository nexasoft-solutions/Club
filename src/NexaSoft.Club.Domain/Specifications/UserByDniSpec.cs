using NexaSoft.Club.Domain.Masters.Users;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Specifications;

public class UserByDniSpec : BaseSpecification<User>
{
    public UserByDniSpec(string dni) 
        : base(m => m.Dni == dni && m.Member!.StatusId == (int)StatusEnum.Activo)
    {
        AddInclude(m => m.Member!);
        AddInclude(m => m.Member!.MemberType!);
    }
}
