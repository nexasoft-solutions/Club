using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.ConceptApplicationTypes;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.ConceptApplicationTypes.Queries.GetConceptApplicationTypes;

public class GetConceptApplicationTypesQueryHandler(
    IGenericRepository<ConceptApplicationType> _repository
) : IQueryHandler<GetConceptApplicationTypesQuery, Pagination<ConceptApplicationTypeResponse>>
{
    public async Task<Result<Pagination<ConceptApplicationTypeResponse>>> Handle(GetConceptApplicationTypesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new ConceptApplicationTypeSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<ConceptApplicationTypeResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<ConceptApplicationTypeResponse>(
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
            return Result.Failure<Pagination<ConceptApplicationTypeResponse>>(ConceptApplicationTypeErrores.ErrorConsulta);
        }
    }
}
