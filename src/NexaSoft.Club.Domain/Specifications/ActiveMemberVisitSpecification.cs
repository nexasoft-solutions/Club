
using NexaSoft.Club.Domain.Features.MemberVisits;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Specifications;

public class ActiveMemberVisitSpecification: BaseSpecification<MemberVisit>
{
    public ActiveMemberVisitSpecification(long memberId) : base()
    {
        AddCriteria(x => x.MemberId == memberId && 
                        x.IsActiveVisit() && 
                        x.StateMemberVisit == (int)EstadosEnum.Activo);
    }
}
