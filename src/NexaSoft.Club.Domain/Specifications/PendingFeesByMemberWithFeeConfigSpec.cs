using NexaSoft.Club.Domain.Features.MemberFees;

namespace NexaSoft.Club.Domain.Specifications;

public class PendingFeesByMemberWithFeeConfigSpec : BaseSpecification<MemberFee>
{
    public PendingFeesByMemberWithFeeConfigSpec(long memberId)
        : base(f => f.MemberId == memberId && f.Status != "Pagado" && f.RemainingAmount > 0)
    {
        // 🔹 Incluir MemberTypeFee y FeeConfiguration para poder acceder a Priority, IncomeAccountId, etc.
        AddInclude(f => f.MemberTypeFee!);
        AddInclude(f => f.MemberTypeFee!.FeeConfiguration!);

        // 🔹 Si necesitas ordenar por prioridad del FeeConfiguration
        //ApplyOrderBy(f => f.MemberTypeFee!.FeeConfiguration!.Priority);
    }
}