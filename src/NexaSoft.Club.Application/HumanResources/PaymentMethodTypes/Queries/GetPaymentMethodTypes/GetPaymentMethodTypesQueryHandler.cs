using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.PaymentMethodTypes;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.PaymentMethodTypes.Queries.GetPaymentMethodTypes;

public class GetPaymentMethodTypesQueryHandler(
    IGenericRepository<PaymentMethodType> _repository
) : IQueryHandler<GetPaymentMethodTypesQuery, Pagination<PaymentMethodTypeResponse>>
{
    public async Task<Result<Pagination<PaymentMethodTypeResponse>>> Handle(GetPaymentMethodTypesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new PaymentMethodTypeSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<PaymentMethodTypeResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<PaymentMethodTypeResponse>(
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
            return Result.Failure<Pagination<PaymentMethodTypeResponse>>(PaymentMethodTypeErrores.ErrorConsulta);
        }
    }
}
