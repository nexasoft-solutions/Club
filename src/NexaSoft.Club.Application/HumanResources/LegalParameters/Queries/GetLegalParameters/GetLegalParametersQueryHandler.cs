using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.LegalParameters;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.LegalParameters.Queries.GetLegalParameters;

public class GetLegalParametersQueryHandler(
    IGenericRepository<LegalParameter> _repository
) : IQueryHandler<GetLegalParametersQuery, Pagination<LegalParameterResponse>>
{
    public async Task<Result<Pagination<LegalParameterResponse>>> Handle(GetLegalParametersQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new LegalParameterSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<LegalParameterResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<LegalParameterResponse>(
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
            return Result.Failure<Pagination<LegalParameterResponse>>(LegalParameterErrores.ErrorConsulta);
        }
    }
}
