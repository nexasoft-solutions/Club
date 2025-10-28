using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.PayPeriodTypes;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.PayPeriodTypes.Queries.GetPayPeriodType;

public class GetPayPeriodTypeQueryHandler(
    IGenericRepository<PayPeriodType> _repository
) : IQueryHandler<GetPayPeriodTypeQuery, PayPeriodTypeResponse>
{
    public async Task<Result<PayPeriodTypeResponse>> Handle(GetPayPeriodTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new PayPeriodTypeSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<PayPeriodTypeResponse>(PayPeriodTypeErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<PayPeriodTypeResponse>(PayPeriodTypeErrores.ErrorConsulta);
        }
    }
}
