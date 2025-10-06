using NexaSoft.Club.Domain.Features.MemberVisits;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Specifications;

public class MonthlyMemberVisitsSpecification: BaseSpecification<MemberVisit>
{
    public MonthlyMemberVisitsSpecification(long memberId, DateOnly startDate, DateOnly endDate) : base()
    {
        AddCriteria(x => x.MemberId == memberId &&
                        x.VisitDate >= startDate &&
                        x.VisitDate <= endDate &&
                        x.StateMemberVisit == (int)EstadosEnum.Activo);
    }
}