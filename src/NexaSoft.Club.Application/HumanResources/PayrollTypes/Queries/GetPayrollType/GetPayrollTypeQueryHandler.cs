using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.PayrollTypes;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.PayrollTypes.Queries.GetPayrollType;

public class GetPayrollTypeQueryHandler(
    IGenericRepository<PayrollType> _repository
) : IQueryHandler<GetPayrollTypeQuery, PayrollTypeResponse>
{
    public async Task<Result<PayrollTypeResponse>> Handle(GetPayrollTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new PayrollTypeSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<PayrollTypeResponse>(PayrollTypeErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<PayrollTypeResponse>(PayrollTypeErrores.ErrorConsulta);
        }
    }
}
