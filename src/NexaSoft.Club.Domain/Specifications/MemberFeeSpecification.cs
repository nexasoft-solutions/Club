using NexaSoft.Club.Domain.Features.MemberFees;

namespace NexaSoft.Club.Domain.Specifications;

public class MemberFeeSpecification : BaseSpecification<MemberFee, MemberFeeResponse>
{
    public BaseSpecParams SpecParams { get; }

    public MemberFeeSpecification(BaseSpecParams specParams) : base()
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
                case "membertypefee":
                    if (long.TryParse(specParams.Search, out var searchNumberConfig))
                        AddCriteria(x => x.MemberTypeFeeId == searchNumberConfig);
                    break;
                case "period":
                    AddCriteria(x => x.Period != null && x.Period.ToLower().Contains(specParams.Search.ToLower()));
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

      AddSelect(x => new MemberFeeResponse(
             x.Id,
             x.MemberId,
             x.Member!.FirstName!,
             x.Member!.LastName!,
             x.MemberTypeFeeId,
             x.MemberTypeFee!.FeeConfiguration.FeeName,
             x.MemberTypeFee!.FeeConfigurationId,
             x.Period,
             x.Amount,
             x.DueDate,
             x.Status,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
