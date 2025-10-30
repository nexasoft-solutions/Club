using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployees;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptEmployees.Queries.GetPayrollConceptEmployee;

public class GetPayrollConceptEmployeeQueryHandler(
    IGenericRepository<PayrollConceptEmployee> _repository
) : IQueryHandler<GetPayrollConceptEmployeeQuery, PayrollConceptEmployeeResponse>
{
    public async Task<Result<PayrollConceptEmployeeResponse>> Handle(GetPayrollConceptEmployeeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new PayrollConceptEmployeeSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<PayrollConceptEmployeeResponse>(PayrollConceptEmployeeErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<PayrollConceptEmployeeResponse>(PayrollConceptEmployeeErrores.ErrorConsulta);
        }
    }
}
