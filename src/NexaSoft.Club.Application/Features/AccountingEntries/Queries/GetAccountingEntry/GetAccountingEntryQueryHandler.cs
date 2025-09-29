using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Features.AccountingEntries;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Features.AccountingEntries.Queries.GetAccountingEntry;

public class GetAccountingEntryQueryHandler(
    IGenericRepository<AccountingEntry> _repository
) : IQueryHandler<GetAccountingEntryQuery, AccountingEntryResponse>
{
    public async Task<Result<AccountingEntryResponse>> Handle(GetAccountingEntryQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new AccountingEntrySpecification(specParams);
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
