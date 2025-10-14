using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.DocumentTypes;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.DocumentTypes.Queries.GetDocumentTypes;

public class GetDocumentTypesQueryHandler(
    IGenericRepository<DocumentType> _repository
) : IQueryHandler<GetDocumentTypesQuery, Pagination<DocumentTypeResponse>>
{
    public async Task<Result<Pagination<DocumentTypeResponse>>> Handle(GetDocumentTypesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new DocumentTypeSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<DocumentTypeResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<DocumentTypeResponse>(
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
            return Result.Failure<Pagination<DocumentTypeResponse>>(DocumentTypeErrores.ErrorConsulta);
        }
    }
}
