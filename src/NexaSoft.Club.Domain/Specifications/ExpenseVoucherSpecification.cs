using NexaSoft.Club.Domain.Features.ExpensesVouchers;

namespace NexaSoft.Club.Domain.Specifications;

public class ExpenseVoucherSpecification : BaseSpecification<ExpenseVoucher, ExpenseVoucherResponse>
{
    public BaseSpecParams SpecParams { get; }

    public ExpenseVoucherSpecification(BaseSpecParams specParams) : base()
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
                    case "vouchernumber":
                        AddCriteria(x => x.VoucherNumber != null && x.VoucherNumber.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "suppliername":
                        AddCriteria(x => x.SupplierName != null && x.SupplierName.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "issuedate":
                        if (DateTime.TryParse(specParams.Search, out var searchDate))
                        {
                            var dateOnly = DateOnly.FromDateTime(searchDate);
                            AddCriteria(x => x.IssueDate == dateOnly);
                        }
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
                case "vouchernumberasc":
                    AddOrderBy(x => x.VoucherNumber!);
                    break;
                case "vouchernumberdesc":
                    AddOrderByDescending(x => x.VoucherNumber!);
                    break;
                default:
                    AddOrderBy(x => x.VoucherNumber!);
                    break;
            }
        }

        AddSelect(x => new ExpenseVoucherResponse(
               x.Id,
               x.AccountingEntryId,
               x.AccountingEntry!.EntryNumber!,
               x.VoucherNumber,
               x.SupplierName,
               x.Amount,
               x.IssueDate,
               x.Description,
               x.ExpenseAccountId,
               x.ExpenseAccount!.AccountName!,
               x.CreatedAt,
               x.UpdatedAt,
               x.CreatedBy,
               x.UpdatedBy
         ));
    }
}
