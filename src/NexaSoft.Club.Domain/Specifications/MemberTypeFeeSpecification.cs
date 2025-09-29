using NexaSoft.Club.Domain.Masters.FeeConfigurations;

namespace NexaSoft.Club.Domain.Specifications;

public class MemberTypeFeeSpecification : BaseSpecification<MemberTypeFee, MemberTypeFeeResponse>
{
    public BaseSpecParams SpecParams { get; }

    public MemberTypeFeeSpecification(BaseSpecParams specParams) : base()
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
                    case "membertype":
                        if (long.TryParse(specParams.Search, out var searchNumber))
                            AddCriteria(x => x.MemberTypeId == searchNumber);
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
                case "membertypeidasc":
                    AddOrderBy(x => x.MemberTypeId!);
                    break;
                case "membertypeiddesc":
                    AddOrderByDescending(x => x.MemberTypeId!);
                    break;
                default:
                    AddOrderBy(x => x.MemberTypeId!);
                    break;
            }
        }

        AddSelect(x => new MemberTypeFeeResponse(
               x.Id,
               x.FeeConfigurationId,
               x.MemberTypeId,
               x.Amount,
               x.FeeConfiguration.DefaultAmount,
               x.FeeConfiguration.FeeName!,
               x.FeeConfiguration.PeriodicityId,
               x.FeeConfiguration.DueDay,
               x.FeeConfiguration.Priority,
               x.CreatedAt,
               x.UpdatedAt,
               x.CreatedBy,
               x.UpdatedBy
         ));
    }
}
