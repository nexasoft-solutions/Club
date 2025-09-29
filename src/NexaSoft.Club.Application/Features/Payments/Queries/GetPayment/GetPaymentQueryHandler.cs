using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Features.Payments;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Features.Payments.Queries.GetPayment;

public class GetPaymentQueryHandler(
    IGenericRepository<Payment> _repository
) : IQueryHandler<GetPaymentQuery, PaymentResponse>
{
    public async Task<Result<PaymentResponse>> Handle(GetPaymentQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new PaymentSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<PaymentResponse>(PaymentErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<PaymentResponse>(PaymentErrores.ErrorConsulta);
        }
    }
}
