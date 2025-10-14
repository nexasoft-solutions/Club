using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.PaymentTypes;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.PaymentTypes.Queries.GetPaymentTypes;

public class GetPaymentTypesQueryHandler(
    IGenericRepository<PaymentType> _repository
) : IQueryHandler<GetPaymentTypesQuery, Pagination<PaymentTypeResponse>>
{
    public async Task<Result<Pagination<PaymentTypeResponse>>> Handle(GetPaymentTypesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new PaymentTypeSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<PaymentTypeResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<PaymentTypeResponse>(
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
            return Result.Failure<Pagination<PaymentTypeResponse>>(PaymentTypeErrores.ErrorConsulta);
        }
    }
}
