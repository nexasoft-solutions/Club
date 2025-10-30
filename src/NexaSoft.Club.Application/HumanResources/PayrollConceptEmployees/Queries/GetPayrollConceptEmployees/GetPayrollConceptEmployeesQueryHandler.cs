using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployees;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptEmployees.Queries.GetPayrollConceptEmployees;

public class GetPayrollConceptEmployeesQueryHandler(
    IGenericRepository<PayrollConceptEmployee> _repository
) : IQueryHandler<GetPayrollConceptEmployeesQuery, Pagination<PayrollConceptEmployeeResponse>>
{
    public async Task<Result<Pagination<PayrollConceptEmployeeResponse>>> Handle(GetPayrollConceptEmployeesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new PayrollConceptEmployeeSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<PayrollConceptEmployeeResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<PayrollConceptEmployeeResponse>(
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
            return Result.Failure<Pagination<PayrollConceptEmployeeResponse>>(PayrollConceptEmployeeErrores.ErrorConsulta);
        }
    }
}
