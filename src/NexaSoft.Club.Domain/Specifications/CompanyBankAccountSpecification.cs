using NexaSoft.Club.Domain.HumanResources.CompanyBankAccounts;

namespace NexaSoft.Club.Domain.Specifications;

public class CompanyBankAccountSpecification : BaseSpecification<CompanyBankAccount, CompanyBankAccountResponse>
{
    public BaseSpecParams SpecParams { get; }

    public CompanyBankAccountSpecification(BaseSpecParams specParams) : base()
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
                case "companyid":
                    if (long.TryParse(specParams.Search, out var searchNumberCompanyId))
                        AddCriteria(x => x.CompanyId == searchNumberCompanyId);
                    break;
                case "bankid":
                    if (long.TryParse(specParams.Search, out var searchNumberBankId))
                        AddCriteria(x => x.BankId == searchNumberBankId);
                    break;
                case "bankaccounttypeid":
                    if (long.TryParse(specParams.Search, out var searchNumberBankAccountTypeId))
                        AddCriteria(x => x.BankAccountTypeId == searchNumberBankAccountTypeId);
                    break;
                case "currencyid":
                    if (long.TryParse(specParams.Search, out var searchNumberCurrencyId))
                        AddCriteria(x => x.CurrencyId == searchNumberCurrencyId);
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
            case "companyidasc":
                AddOrderBy(x => x.CompanyId!);
                break;
            case "companyiddesc":
                AddOrderByDescending(x => x.CompanyId!);
                break;
            default:
                AddOrderBy(x => x.CompanyId!);
                break;
        }
    }

      AddSelect(x => new CompanyBankAccountResponse(
             x.Id,
             x.CompanyId,
             x.Company!.BusinessName!,
             x.BankId,
             x.Bank!.Code!,
             x.BankAccountTypeId,
             x.BankAccountType!.Code!,
             x.BankAccountNumber,
             x.CciNumber,
             x.CurrencyId,
             x.Currency!.Code!,
             x.IsPrimary,
             x.IsActive,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
