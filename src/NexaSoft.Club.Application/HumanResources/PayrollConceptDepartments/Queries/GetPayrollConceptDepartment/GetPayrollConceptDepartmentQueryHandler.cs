using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptDepartments;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptDepartments.Queries.GetPayrollConceptDepartment;

public class GetPayrollConceptDepartmentQueryHandler(
    IGenericRepository<PayrollConceptDepartment> _repository
) : IQueryHandler<GetPayrollConceptDepartmentQuery, PayrollConceptDepartmentResponse>
{
    public async Task<Result<PayrollConceptDepartmentResponse>> Handle(GetPayrollConceptDepartmentQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new PayrollConceptDepartmentSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<PayrollConceptDepartmentResponse>(PayrollConceptDepartmentErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<PayrollConceptDepartmentResponse>(PayrollConceptDepartmentErrores.ErrorConsulta);
        }
    }
}
