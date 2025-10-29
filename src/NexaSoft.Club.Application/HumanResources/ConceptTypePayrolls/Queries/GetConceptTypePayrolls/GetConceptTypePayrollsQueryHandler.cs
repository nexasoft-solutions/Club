using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.ConceptTypePayrolls;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.ConceptTypePayrolls.Queries.GetConceptTypePayrolls;

public class GetConceptTypePayrollsQueryHandler(
    IGenericRepository<ConceptTypePayroll> _repository
) : IQueryHandler<GetConceptTypePayrollsQuery, Pagination<ConceptTypePayrollResponse>>
{
    public async Task<Result<Pagination<ConceptTypePayrollResponse>>> Handle(GetConceptTypePayrollsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new ConceptTypePayrollSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<ConceptTypePayrollResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<ConceptTypePayrollResponse>(
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
            return Result.Failure<Pagination<ConceptTypePayrollResponse>>(ConceptTypePayrollErrores.ErrorConsulta);
        }
    }
}
