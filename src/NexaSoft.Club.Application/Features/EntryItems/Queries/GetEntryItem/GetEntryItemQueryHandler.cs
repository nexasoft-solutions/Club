using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Features.EntryItems;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Features.EntryItems.Queries.GetEntryItem;

public class GetEntryItemQueryHandler(
    IGenericRepository<EntryItem> _repository
) : IQueryHandler<GetEntryItemQuery, EntryItemResponse>
{
    public async Task<Result<EntryItemResponse>> Handle(GetEntryItemQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new EntryItemSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<EntryItemResponse>(EntryItemErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<EntryItemResponse>(EntryItemErrores.ErrorConsulta);
        }
    }
}
