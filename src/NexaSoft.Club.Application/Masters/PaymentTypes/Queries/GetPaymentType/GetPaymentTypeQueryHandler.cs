using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.PaymentTypes;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.PaymentTypes.Queries.GetPaymentType;

public class GetPaymentTypeQueryHandler(
    IGenericRepository<PaymentType> _repository
) : IQueryHandler<GetPaymentTypeQuery, PaymentTypeResponse>
{
    public async Task<Result<PaymentTypeResponse>> Handle(GetPaymentTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new PaymentTypeSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<PaymentTypeResponse>(PaymentTypeErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<PaymentTypeResponse>(PaymentTypeErrores.ErrorConsulta);
        }
    }
}
