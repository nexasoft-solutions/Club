using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.IncomeTaxScales;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.IncomeTaxScales.Queries.GetIncomeTaxScale;

public class GetIncomeTaxScaleQueryHandler(
    IGenericRepository<IncomeTaxScale> _repository
) : IQueryHandler<GetIncomeTaxScaleQuery, IncomeTaxScaleResponse>
{
    public async Task<Result<IncomeTaxScaleResponse>> Handle(GetIncomeTaxScaleQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new IncomeTaxScaleSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<IncomeTaxScaleResponse>(IncomeTaxScaleErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<IncomeTaxScaleResponse>(IncomeTaxScaleErrores.ErrorConsulta);
        }
    }
}
