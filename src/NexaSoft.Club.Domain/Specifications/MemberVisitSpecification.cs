using NexaSoft.Club.Domain.Features.MemberVisits;

namespace NexaSoft.Club.Domain.Specifications;

public class MemberVisitSpecification : BaseSpecification<MemberVisit, MemberVisitResponse>
{
    public BaseSpecParams SpecParams { get; }

    public MemberVisitSpecification(BaseSpecParams specParams) : base()
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
                    case "visitdate":
                        if (DateOnly.TryParse(specParams.Search, out var searchDate))
                        {
                            AddCriteria(x => x.VisitDate == searchDate);
                        }
                        break;
                    case "checkinby":
                        AddCriteria(x => x.CheckInBy != null && x.CheckInBy.ToLower().Contains(specParams.Search.ToLower()));
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
                case "visitdateasc":
                    AddOrderBy(x => x.VisitDate!);
                    break;
                case "visitdatedesc":
                    AddOrderByDescending(x => x.VisitDate!);
                    break;
                default:
                    AddOrderBy(x => x.VisitDate!);
                    break;
            }
        }

        AddSelect(x => new MemberVisitResponse(
               x.Id,
               x.MemberId,
               x.Member!.FirstName!,
               x.Member!.LastName!,
               x.VisitDate,
               x.EntryTime,
               x.ExitTime,
               x.QrCodeUsed,
               x.Notes,
               x.CheckInBy,
               x.CheckOutBy,
               x.VisitType,
               x.CreatedAt,
               x.UpdatedAt,
               x.CreatedBy,
               x.UpdatedBy
         ));
    }
}
