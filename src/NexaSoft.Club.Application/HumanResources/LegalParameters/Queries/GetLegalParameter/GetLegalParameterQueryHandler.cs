using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.LegalParameters;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.LegalParameters.Queries.GetLegalParameter;

public class GetLegalParameterQueryHandler(
    IGenericRepository<LegalParameter> _repository
) : IQueryHandler<GetLegalParameterQuery, LegalParameterResponse>
{
    public async Task<Result<LegalParameterResponse>> Handle(GetLegalParameterQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new LegalParameterSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<LegalParameterResponse>(LegalParameterErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<LegalParameterResponse>(LegalParameterErrores.ErrorConsulta);
        }
    }
}
