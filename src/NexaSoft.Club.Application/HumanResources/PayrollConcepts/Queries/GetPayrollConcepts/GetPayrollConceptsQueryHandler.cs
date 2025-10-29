using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.PayrollConcepts;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.PayrollConcepts.Queries.GetPayrollConcepts;

public class GetPayrollConceptsQueryHandler(
    IGenericRepository<PayrollConcept> _repository
) : IQueryHandler<GetPayrollConceptsQuery, Pagination<PayrollConceptResponse>>
{
    public async Task<Result<Pagination<PayrollConceptResponse>>> Handle(GetPayrollConceptsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new PayrollConceptSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<PayrollConceptResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<PayrollConceptResponse>(
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
            return Result.Failure<Pagination<PayrollConceptResponse>>(PayrollConceptErrores.ErrorConsulta);
        }
    }
}
