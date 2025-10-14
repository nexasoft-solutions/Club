using NexaSoft.Club.Domain.Features.Reservations;

namespace NexaSoft.Club.Domain.Specifications;

public class ReservationSpecification : BaseSpecification<Reservation, ReservationResponse>
{
    public BaseSpecParams SpecParams { get; }

    public ReservationSpecification(BaseSpecParams specParams) : base()
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
                case "spaceid":
                    if (long.TryParse(specParams.Search, out var searchNumberSpace))
                        AddCriteria(x => x.SpaceRateId == searchNumberSpace);
                    break;               
                case "status":
                    if (long.TryParse(specParams.Search, out var searchNumberStatus))
                        AddCriteria(x => x.StatusId == searchNumberStatus);
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

      AddSelect(x => new ReservationResponse(
             x.Id,
             x.MemberId,
             x.Member!.FirstName! + ", " + x.Member!.LastName!,
             x.Member!.MemberType!.TypeName!,
             x.SpaceRateId,
             x.SpaceRate!.Space!.SpaceName,
             x.SpaceAvailabilityId,
             x.Date,
             x.StartTime,
             x.EndTime,
             x.StatusId,
             x.Status!.Description!,
             x.PaymentMethodId,
             x.PaymentType!.Name!,
             x.ReferenceNumber,
             x.DocumentTypeId,
             x.DocumentType!.Name!,
             x.ReceiptNumber,
             x.TotalAmount,
             x.AccountingEntryId,
             x.AccountingEntry!.EntryNumber!,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
