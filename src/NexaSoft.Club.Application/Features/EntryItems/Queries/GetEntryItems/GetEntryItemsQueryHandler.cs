using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Features.EntryItems;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Features.EntryItems.Queries.GetEntryItems;

public class GetEntryItemsQueryHandler(
    IGenericRepository<EntryItem> _repository
) : IQueryHandler<GetEntryItemsQuery, Pagination<EntryItemResponse>>
{
    public async Task<Result<Pagination<EntryItemResponse>>> Handle(GetEntryItemsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new EntryItemSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<EntryItemResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<EntryItemResponse>(
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
            return Result.Failure<Pagination<EntryItemResponse>>(EntryItemErrores.ErrorConsulta);
        }
    }
}
