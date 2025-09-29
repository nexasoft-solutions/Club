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
                        AddCriteria(x => x.SpaceId == searchNumberSpace);
                    break;
                case "starttime":
                    if (DateTime.TryParse(specParams.Search, out var searchDate))
                        AddCriteria(x => x.StartTime == searchDate.Date);
                    break;
                case "status":
                    AddCriteria(x => x.Status != null && x.Status.ToLower().Contains(specParams.Search.ToLower()));
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
             x.Member!.FirstName!,
             x.Member!.LastName!,
             x.SpaceId,
             x.Space!.SpaceName!,
             x.StartTime,
             x.EndTime,
             x.Status,
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
