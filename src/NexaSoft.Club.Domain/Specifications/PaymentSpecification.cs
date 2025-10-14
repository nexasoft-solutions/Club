using NexaSoft.Club.Domain.Features.MemberFees;
using NexaSoft.Club.Domain.Features.Payments;
using NexaSoft.Club.Domain.Masters.FeeConfigurations;

namespace NexaSoft.Club.Domain.Specifications;

public class PaymentSpecification : BaseSpecification<Payment, PaymentResponse>
{
    public BaseSpecParams SpecParams { get; }

    public PaymentSpecification(BaseSpecParams specParams) : base()
    {

        SpecParams = specParams;

        // Incluir las relaciones necesarias para acceder a MemberFee y MemberTypeFee
        AddInclude(x => x.PaymentItems);
        AddInclude($"{nameof(Payment.PaymentItems)}.{nameof(PaymentItem.MemberFee)}");
        AddInclude($"{nameof(Payment.PaymentItems)}.{nameof(PaymentItem.MemberFee)}.{nameof(MemberFee.MemberTypeFee)}");
        AddInclude($"{nameof(Payment.PaymentItems)}.{nameof(PaymentItem.MemberFee)}.{nameof(MemberFee.MemberTypeFee)}.{nameof(MemberTypeFee.FeeConfiguration)}");


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
               x.Member!.FirstName! + ", " + x.Member!.LastName!,
               x.TotalAmount,
               x.PaymentDate,
               x.PaymentMethodId,
               x.ReferenceNumber,
               x.DocumentTypeId,
               x.ReceiptNumber,
               x.IsPartial,
               x.AccountingEntryId,
               x.AccountingEntry!.EntryNumber!,
               x.StatusId,
               x.PaymentType!.Name!,
               x.DocumentType!.Name!,
               x.Status!.Name!,
               x.PaymentItems.Select(pi => new PaymentDetailResponse(
                        pi.Id,
                        pi.PaymentId,
                        pi.MemberFeeId,
                        pi.Amount,
                        pi.MemberFee != null && pi.MemberFee.MemberTypeFee != null && pi.MemberFee.MemberTypeFee.FeeConfiguration != null
                            ? pi.MemberFee.MemberTypeFee.FeeConfiguration.FeeName
                            : "Concepto no disponible",
                        pi.MemberFee!.Period ?? "Periodo no disponible"

                )).ToList(),
                x.CreatedAt,
                x.UpdatedAt,
                x.CreatedBy,
                x.UpdatedBy
        ));
    }
}
