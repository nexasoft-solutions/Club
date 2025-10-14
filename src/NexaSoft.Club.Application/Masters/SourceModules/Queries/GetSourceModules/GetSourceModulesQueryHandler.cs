using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.SourceModules;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.SourceModules.Queries.GetSourceModules;

public class GetSourceModulesQueryHandler(
    IGenericRepository<SourceModule> _repository
) : IQueryHandler<GetSourceModulesQuery, Pagination<SourceModuleResponse>>
{
    public async Task<Result<Pagination<SourceModuleResponse>>> Handle(GetSourceModulesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new SourceModuleSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<SourceModuleResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<SourceModuleResponse>(
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
            return Result.Failure<Pagination<SourceModuleResponse>>(SourceModuleErrores.ErrorConsulta);
        }
    }
}
