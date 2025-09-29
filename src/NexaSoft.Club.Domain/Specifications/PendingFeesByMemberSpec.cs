
using NexaSoft.Club.Domain.Features.MemberFees;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Specifications;

public class PendingFeesByMemberSpec : BaseSpecification<MemberFee>
{
    public PendingFeesByMemberSpec(long memberId) : base()
    {
        AddCriteria(f => f.MemberId == memberId && 
                           f.RemainingAmount > 0 && // Solo cuotas con saldo pendiente
                           f.StateMemberFee == (int)EstadosEnum.Activo);
            AddOrderBy(f => f.DueDate);
            AddOrderBy(f => f.CreatedAt);
    }
}
