using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Features.AccountingEntries;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Features.AccountingEntries.Queries.GetAccountingEntries;

public class GetAccountingEntriesQueryHandler(
    IGenericRepository<AccountingEntry> _repository
) : IQueryHandler<GetAccountingEntriesQuery, Pagination<AccountingEntryResponse>>
{
    public async Task<Result<Pagination<AccountingEntryResponse>>> Handle(GetAccountingEntriesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new AccountingEntrySpecification(query.SpecParams);
            var responses = await _repository.ListAsync<AccountingEntryResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<AccountingEntryResponse>(
                    query.SpecParams.PageIndex,
                    query.SpecParams.PageSize,
                    totalItems,
                    responses
              );

            return Result.Success(pagination);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<Pagination<AccountingEntryResponse>>(AccountingEntryErrores.ErrorConsulta);
        }
    }
}
