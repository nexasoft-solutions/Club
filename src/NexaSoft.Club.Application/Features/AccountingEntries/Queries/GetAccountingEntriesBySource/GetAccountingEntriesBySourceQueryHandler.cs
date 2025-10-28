using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.AccountingEntries;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.Features.AccountingEntries.Queries.GetAccountingEntriesBySource;

public class GetAccountingEntriesBySourceQueryHandler(IGenericRepository<AccountingEntry> _repository) : IQueryHandler<GetAccountingEntriesBySourceQuery, AccountingEntryResponse>
{
    public async Task<Result<AccountingEntryResponse>> Handle(GetAccountingEntriesBySourceQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new AccountingEntrySpecification(query.SourceModuleId, query.SourceId);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<AccountingEntryResponse>(AccountingEntryErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<AccountingEntryResponse>(AccountingEntryErrores.ErrorConsulta);
        }
    }
}
