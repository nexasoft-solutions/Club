using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.PayrollFormulas;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.PayrollFormulas.Queries.GetPayrollFormula;

public class GetPayrollFormulaQueryHandler(
    IGenericRepository<PayrollFormula> _repository
) : IQueryHandler<GetPayrollFormulaQuery, PayrollFormulaResponse>
{
    public async Task<Result<PayrollFormulaResponse>> Handle(GetPayrollFormulaQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new PayrollFormulaSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<PayrollFormulaResponse>(PayrollFormulaErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<PayrollFormulaResponse>(PayrollFormulaErrores.ErrorConsulta);
        }
    }
}
