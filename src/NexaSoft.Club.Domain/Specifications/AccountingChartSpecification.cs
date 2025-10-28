using NexaSoft.Club.Domain.Masters.AccountingCharts;

namespace NexaSoft.Club.Domain.Specifications;

public class AccountingChartSpecification : BaseSpecification<AccountingChart, AccountingChartResponse>
{
    public BaseSpecParams SpecParams { get; }

    public AccountingChartSpecification(BaseSpecParams specParams) : base()
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
                    case "accountcode":
                        AddCriteria(x => x.AccountCode != null && x.AccountCode.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "accountname":
                        AddCriteria(x => x.AccountName != null && x.AccountName.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "accounttype":
                        if (long.TryParse(specParams.Search, out var searchNumberAccountType))
                            AddCriteria(x => x.AccountTypeId == searchNumberAccountType);
                        break;
                    case "level":
                        if (long.TryParse(specParams.Search, out var searchNumber))
                            AddCriteria(x => x.Level == searchNumber);
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
                case "accountcodeasc":
                    AddOrderBy(x => x.AccountCode!);
                    break;
                case "accountcodedesc":
                    AddOrderByDescending(x => x.AccountCode!);
                    break;
                default:
                    AddOrderBy(x => x.AccountCode!);
                    break;
            }
        }

        AddSelect(x => new AccountingChartResponse(
               x.Id,
               x.AccountCode,
               x.AccountName,
               x.AccountTypeId,
               x.AccountType!.Name!,
               x.ParentAccountId,
               x.ParentAccount!.AccountName!,
               x.Level,
               x.AllowsTransactions,
               x.Description,
               x.CreatedAt,
               x.UpdatedAt,
               x.CreatedBy,
               x.UpdatedBy,
               null
         ));
    }
}
