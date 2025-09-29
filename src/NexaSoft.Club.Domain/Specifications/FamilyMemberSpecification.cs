using NexaSoft.Club.Domain.Features.FamilyMembers;

namespace NexaSoft.Club.Domain.Specifications;

public class FamilyMemberSpecification : BaseSpecification<FamilyMember, FamilyMemberResponse>
{
    public BaseSpecParams SpecParams { get; }

    public FamilyMemberSpecification(BaseSpecParams specParams) : base()
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
                case "dni":
                    AddCriteria(x => x.Dni != null && x.Dni.ToLower().Contains(specParams.Search.ToLower()));
                    break;
                case "firstname":
                    AddCriteria(x => x.FirstName != null && x.FirstName.ToLower().Contains(specParams.Search.ToLower()));
                    break;
                case "lastname":
                    AddCriteria(x => x.LastName != null && x.LastName.ToLower().Contains(specParams.Search.ToLower()));
                    break;
                case "relationship":
                    AddCriteria(x => x.Relationship != null && x.Relationship.ToLower().Contains(specParams.Search.ToLower()));
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

      AddSelect(x => new FamilyMemberResponse(
             x.Id,
             x.MemberId,
             x.Member!.FirstName!,
             x.Member!.LastName!,
             x.Dni,
             x.FirstName,
             x.LastName,
             x.Relationship,
             x.BirthDate,
             x.IsAuthorized,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
