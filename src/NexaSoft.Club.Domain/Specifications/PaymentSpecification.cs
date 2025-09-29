using NexaSoft.Club.Domain.Features.Payments;

namespace NexaSoft.Club.Domain.Specifications;

public class PaymentSpecification : BaseSpecification<Payment, PaymentResponse>
{
    public BaseSpecParams SpecParams { get; }

    public PaymentSpecification(BaseSpecParams specParams) : base()
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
                case "memberid":
                    if (long.TryParse(specParams.Search, out var searchNumber))
                        AddCriteria(x => x.MemberId == searchNumber);
                    break;
                case "paymentdate":
                    if (DateOnly.TryParse(specParams.Search, out var searchDate))
                        AddCriteria(x => x.PaymentDate == searchDate);
                    break;
                case "paymentmethod":
                    AddCriteria(x => x.PaymentMethod != null && x.PaymentMethod.ToLower().Contains(specParams.Search.ToLower()));
                    break;
                case "receiptnumber":
                    AddCriteria(x => x.ReceiptNumber != null && x.ReceiptNumber.ToLower().Contains(specParams.Search.ToLower()));
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
            case "memberidasc":
                AddOrderBy(x => x.MemberId!);
                break;
            case "memberiddesc":
                AddOrderByDescending(x => x.MemberId!);
                break;
            default:
                AddOrderBy(x => x.MemberId!);
                break;
        }
    }

      AddSelect(x => new PaymentResponse(
             x.Id,
             x.MemberId,
             x.Member!.FirstName!,
             x.Member!.LastName!,           
             x.TotalAmount,
             x.PaymentDate,
             x.PaymentMethod,
             x.ReferenceNumber,
             x.ReceiptNumber,
             x.IsPartial,
             x.AccountingEntryId,
             x.AccountingEntry!.EntryNumber!,
             x.PaymentItems.ToList(),
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
