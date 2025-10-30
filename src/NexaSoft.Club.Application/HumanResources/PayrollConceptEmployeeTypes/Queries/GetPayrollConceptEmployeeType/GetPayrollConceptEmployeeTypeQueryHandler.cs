using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployeeTypes;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptEmployeeTypes.Queries.GetPayrollConceptEmployeeType;

public class GetPayrollConceptEmployeeTypeQueryHandler(
    IGenericRepository<PayrollConceptEmployeeType> _repository
) : IQueryHandler<GetPayrollConceptEmployeeTypeQuery, PayrollConceptEmployeeTypeResponse>
{
    public async Task<Result<PayrollConceptEmployeeTypeResponse>> Handle(GetPayrollConceptEmployeeTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new PayrollConceptEmployeeTypeSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<PayrollConceptEmployeeTypeResponse>(PayrollConceptEmployeeTypeErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<PayrollConceptEmployeeTypeResponse>(PayrollConceptEmployeeTypeErrores.ErrorConsulta);
        }
    }
}
