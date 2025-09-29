using NexaSoft.Club.Domain.Masters.MemberStatuses;

namespace NexaSoft.Club.Domain.Specifications;

public class MemberStatusSpecification : BaseSpecification<MemberStatus, MemberStatusResponse>
{
    public BaseSpecParams SpecParams { get; }

    public MemberStatusSpecification(BaseSpecParams specParams) : base()
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
                case "statusname":
                    AddCriteria(x => x.StatusName != null && x.StatusName.ToLower().Contains(specParams.Search.ToLower()));
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
            case "statusnameasc":
                AddOrderBy(x => x.StatusName!);
                break;
            case "statusnamedesc":
                AddOrderByDescending(x => x.StatusName!);
                break;
            default:
                AddOrderBy(x => x.StatusName!);
                break;
        }
    }

      AddSelect(x => new MemberStatusResponse(
             x.Id,
             x.StatusName,
             x.Description,
             x.CanAccess,
             x.CanReserve,
             x.CanParticipate,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
