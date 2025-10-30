using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptDepartments;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptDepartments.Queries.GetPayrollConceptDepartments;

public class GetPayrollConceptDepartmentsQueryHandler(
    IGenericRepository<PayrollConceptDepartment> _repository
) : IQueryHandler<GetPayrollConceptDepartmentsQuery, Pagination<PayrollConceptDepartmentResponse>>
{
    public async Task<Result<Pagination<PayrollConceptDepartmentResponse>>> Handle(GetPayrollConceptDepartmentsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new PayrollConceptDepartmentSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<PayrollConceptDepartmentResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<PayrollConceptDepartmentResponse>(
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
            return Result.Failure<Pagination<PayrollConceptDepartmentResponse>>(PayrollConceptDepartmentErrores.ErrorConsulta);
        }
    }
}
