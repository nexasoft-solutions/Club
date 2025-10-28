using NexaSoft.Club.Domain.Features.AccountingEntries;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Specifications;

public class AccountingEntrySpecification : BaseSpecification<AccountingEntry, AccountingEntryResponse>
{
    public BaseSpecParams? SpecParams { get; }

    public AccountingEntrySpecification(BaseSpecParams specParams) : base()
    {

        SpecParams = specParams;

        AddInclude(x => x.AccountingDetails.Where(p => p.StateEntryItem == (int)EstadosEnum.Activo));

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
                    case "entrynumber":
                        AddCriteria(x => x.EntryNumber != null && x.EntryNumber.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "entrydate":
                        if (DateTime.TryParse(specParams.Search, out var searchDate))
                        {
                            var dateOnly = DateOnly.FromDateTime(searchDate);
                            AddCriteria(x => x.EntryDate == dateOnly);
                        }
                        break;
                    case "description":
                        AddCriteria(x => x.Description != null && x.Description.ToLower().Contains(specParams.Search.ToLower()));
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
                case "entrynumberasc":
                    AddOrderBy(x => x.EntryNumber!);
                    break;
                case "entrynumberdesc":
                    AddOrderByDescending(x => x.EntryNumber!);
                    break;
                default:
                    AddOrderBy(x => x.EntryNumber!);
                    break;
            }
        }

        AddSelect(x => new AccountingEntryResponse(
               x.Id,
               x.EntryNumber,
               x.EntryDate,
               x.Description,
               x.SourceModuleId,
               x.SourceModule!.Name,
               x.SourceId,
               x.TotalDebit,
               x.TotalCredit,
               x.IsAdjusted,
               x.AdjustmentReason,
               x.CreatedAt,
               x.UpdatedAt,
               x.CreatedBy,
               x.UpdatedBy,
               x.AccountingDetails.Select(d => new AccountingEntryItemResponse(
                   d.Id,
                   d.DebitAmount,
                   d.CreditAmount,
                   d.Description,
                   d.AccountingChart!.AccountName,
                   d.AccountingChart!.AccountCode
               )).ToList()
         ));
    }

    public AccountingEntrySpecification(long sourceModuleId, long sourceId, string? sort = null) : base()
    {
        AddInclude(x => x.AccountingDetails.Where(p => p.StateEntryItem == (int)EstadosEnum.Activo));

        AddCriteria(x => x.SourceModuleId == sourceModuleId);
        AddCriteria(x => x.SourceId == sourceId);

        switch (sort?.ToLower())
        {
            case "entrynumberasc":
                AddOrderBy(x => x.EntryNumber!);
                break;
            case "entrynumberdesc":
                AddOrderByDescending(x => x.EntryNumber!);
                break;
            default:
                AddOrderBy(x => x.EntryNumber!);
                break;
        }

        AddSelect(x => new AccountingEntryResponse(
               x.Id,
               x.EntryNumber,
               x.EntryDate,
               x.Description,
               x.SourceModuleId,
               x.SourceModule!.Name,
               x.SourceId,
               x.TotalDebit,
               x.TotalCredit,
               x.IsAdjusted,
               x.AdjustmentReason,
               x.CreatedAt,
               x.UpdatedAt,
               x.CreatedBy,
               x.UpdatedBy,
               x.AccountingDetails.Select(d => new AccountingEntryItemResponse(
                   d.Id,
                   d.DebitAmount,
                   d.CreditAmount,
                   d.Description,
                   d.AccountingChart!.AccountName,
                   d.AccountingChart!.AccountCode
               )).ToList()
         ));
    }



}
