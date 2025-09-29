using NexaSoft.Club.Domain.Features.Payments;

namespace NexaSoft.Club.Domain.Specifications;

public class PaymentItemsByPaymentSpec : BaseSpecification<PaymentItem>
{
    public PaymentItemsByPaymentSpec(BaseSpecParams specParams)
        : base()
    {
        //AddInclude(x => x.MemberFee!);
        // ✅ Parsear primero
        if (long.TryParse(specParams.Search, out var paymentId))
        {
            AddCriteria(x => x.PaymentId == paymentId);
        }

        if (!specParams.NoPaging)
        {
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }

        switch (specParams.Sort)
        {
            case "idasc":
                AddOrderBy(x => x.Id!);
                break;
            case "iddesc":
                AddOrderByDescending(x => x.Id!);
                break;
            default:
                AddOrderBy(x => x.Id!);
                break;
        }
    }
}