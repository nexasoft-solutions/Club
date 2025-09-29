using NexaSoft.Club.Domain.Features.EntryItems;

namespace NexaSoft.Club.Domain.Specifications;

public class EntryItemSpecification : BaseSpecification<EntryItem, EntryItemResponse>
{
    public BaseSpecParams SpecParams { get; }

    public EntryItemSpecification(BaseSpecParams specParams) : base()
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
                case "entryid":
                    if (long.TryParse(specParams.Search, out var searchNumber))
                        AddCriteria(x => x.AccountingEntryId == searchNumber);
                    break;
                case "accountid":
                    if (long.TryParse(specParams.Search, out var searchNumberChar))
                        AddCriteria(x => x.AccountingChartId == searchNumberChar);
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
            case "entryidasc":
                AddOrderBy(x => x.AccountingEntryId!);
                break;
            case "entryiddesc":
                AddOrderByDescending(x => x.AccountingEntryId!);
                break;
            default:
                AddOrderBy(x => x.AccountingEntryId!);
                break;
        }
    }

      AddSelect(x => new EntryItemResponse(
             x.Id,
             x.AccountingEntryId,
             x.AccountingEntry!.EntryNumber!,
             x.AccountingChartId,
             x.AccountingChart!.AccountName!,
             x.DebitAmount,
             x.CreditAmount,
             x.Description,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
