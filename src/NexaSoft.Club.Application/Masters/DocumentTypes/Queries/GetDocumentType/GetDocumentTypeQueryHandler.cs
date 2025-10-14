using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.DocumentTypes;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.DocumentTypes.Queries.GetDocumentType;

public class GetDocumentTypeQueryHandler(
    IGenericRepository<DocumentType> _repository
) : IQueryHandler<GetDocumentTypeQuery, DocumentTypeResponse>
{
    public async Task<Result<DocumentTypeResponse>> Handle(GetDocumentTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new DocumentTypeSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<DocumentTypeResponse>(DocumentTypeErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<DocumentTypeResponse>(DocumentTypeErrores.ErrorConsulta);
        }
    }
}
