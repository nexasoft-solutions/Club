using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.PayrollFormulas;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.PayrollFormulas.Queries.GetPayrollFormulas;

public class GetPayrollFormulasQueryHandler(
    IGenericRepository<PayrollFormula> _repository
) : IQueryHandler<GetPayrollFormulasQuery, Pagination<PayrollFormulaResponse>>
{
    public async Task<Result<Pagination<PayrollFormulaResponse>>> Handle(GetPayrollFormulasQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new PayrollFormulaSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<PayrollFormulaResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<PayrollFormulaResponse>(
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
            return Result.Failure<Pagination<PayrollFormulaResponse>>(PayrollFormulaErrores.ErrorConsulta);
        }
    }
}
