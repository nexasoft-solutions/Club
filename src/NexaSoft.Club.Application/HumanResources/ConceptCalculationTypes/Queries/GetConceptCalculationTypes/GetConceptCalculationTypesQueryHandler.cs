using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.ConceptCalculationTypes;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.ConceptCalculationTypes.Queries.GetConceptCalculationTypes;

public class GetConceptCalculationTypesQueryHandler(
    IGenericRepository<ConceptCalculationType> _repository
) : IQueryHandler<GetConceptCalculationTypesQuery, Pagination<ConceptCalculationTypeResponse>>
{
    public async Task<Result<Pagination<ConceptCalculationTypeResponse>>> Handle(GetConceptCalculationTypesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new ConceptCalculationTypeSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<ConceptCalculationTypeResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<ConceptCalculationTypeResponse>(
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
            return Result.Failure<Pagination<ConceptCalculationTypeResponse>>(ConceptCalculationTypeErrores.ErrorConsulta);
        }
    }
}
