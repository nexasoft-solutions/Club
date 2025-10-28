using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.PaymentMethodTypes;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.PaymentMethodTypes.Queries.GetPaymentMethodType;

public class GetPaymentMethodTypeQueryHandler(
    IGenericRepository<PaymentMethodType> _repository
) : IQueryHandler<GetPaymentMethodTypeQuery, PaymentMethodTypeResponse>
{
    public async Task<Result<PaymentMethodTypeResponse>> Handle(GetPaymentMethodTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new PaymentMethodTypeSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<PaymentMethodTypeResponse>(PaymentMethodTypeErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<PaymentMethodTypeResponse>(PaymentMethodTypeErrores.ErrorConsulta);
        }
    }
}
