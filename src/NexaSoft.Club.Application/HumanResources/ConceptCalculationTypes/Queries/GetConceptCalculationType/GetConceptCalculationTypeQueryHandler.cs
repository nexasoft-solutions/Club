using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.ConceptCalculationTypes;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.ConceptCalculationTypes.Queries.GetConceptCalculationType;

public class GetConceptCalculationTypeQueryHandler(
    IGenericRepository<ConceptCalculationType> _repository
) : IQueryHandler<GetConceptCalculationTypeQuery, ConceptCalculationTypeResponse>
{
    public async Task<Result<ConceptCalculationTypeResponse>> Handle(GetConceptCalculationTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new ConceptCalculationTypeSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<ConceptCalculationTypeResponse>(ConceptCalculationTypeErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<ConceptCalculationTypeResponse>(ConceptCalculationTypeErrores.ErrorConsulta);
        }
    }
}
