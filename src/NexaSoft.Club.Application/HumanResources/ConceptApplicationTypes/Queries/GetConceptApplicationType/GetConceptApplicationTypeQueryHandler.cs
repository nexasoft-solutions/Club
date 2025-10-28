using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.ConceptApplicationTypes;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.ConceptApplicationTypes.Queries.GetConceptApplicationType;

public class GetConceptApplicationTypeQueryHandler(
    IGenericRepository<ConceptApplicationType> _repository
) : IQueryHandler<GetConceptApplicationTypeQuery, ConceptApplicationTypeResponse>
{
    public async Task<Result<ConceptApplicationTypeResponse>> Handle(GetConceptApplicationTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new ConceptApplicationTypeSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<ConceptApplicationTypeResponse>(ConceptApplicationTypeErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<ConceptApplicationTypeResponse>(ConceptApplicationTypeErrores.ErrorConsulta);
        }
    }
}
