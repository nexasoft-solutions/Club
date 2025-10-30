using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.IncomeTaxScales;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.IncomeTaxScales.Queries.GetIncomeTaxScales;

public class GetIncomeTaxScalesQueryHandler(
    IGenericRepository<IncomeTaxScale> _repository
) : IQueryHandler<GetIncomeTaxScalesQuery, Pagination<IncomeTaxScaleResponse>>
{
    public async Task<Result<Pagination<IncomeTaxScaleResponse>>> Handle(GetIncomeTaxScalesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new IncomeTaxScaleSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<IncomeTaxScaleResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<IncomeTaxScaleResponse>(
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
            return Result.Failure<Pagination<IncomeTaxScaleResponse>>(IncomeTaxScaleErrores.ErrorConsulta);
        }
    }
}
