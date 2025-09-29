using NexaSoft.Club.Domain.Features.Payments;

namespace NexaSoft.Club.Domain.Specifications;

public class PaymentItemsSpecification : BaseSpecification<PaymentItem, PaymentItemsResponse>
{
    public BaseSpecParams SpecParams { get; }

    public PaymentItemsSpecification(BaseSpecParams specParams) : base()
    {

        SpecParams = specParams;

        if (specParams.Id.HasValue)
        {
            AddCriteria(x => x.Id == specParams.Id.Value);
        }
        else
        {
            if (!string.IsNullOrEmpty(specParams.Search) && !string.IsNullOrEmpty(specParams.SearchFields))
            {
                switch (specParams.SearchFields.ToLower())
                {
                    case "payment":
                        if (long.TryParse(specParams.Search, out var searchNumber))
                            AddCriteria(x => x.PaymentId == searchNumber);
                        break;

                    default:
                        Criteria = x => true;
                        break;
                }
            }


            // Aplicar paginación
            if (!specParams.NoPaging)
            {
                ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
            }

            // Aplicar ordenamiento
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

        AddSelect(x => new PaymentItemsResponse(
               x.Id,
               x.PaymentId,
               x.MemberFeeId,
               x.Amount
         ));
    }
}
