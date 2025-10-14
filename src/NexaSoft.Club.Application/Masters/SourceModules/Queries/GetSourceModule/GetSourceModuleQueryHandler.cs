using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.SourceModules;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.SourceModules.Queries.GetSourceModule;

public class GetSourceModuleQueryHandler(
    IGenericRepository<SourceModule> _repository
) : IQueryHandler<GetSourceModuleQuery, SourceModuleResponse>
{
    public async Task<Result<SourceModuleResponse>> Handle(GetSourceModuleQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new SourceModuleSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<SourceModuleResponse>(SourceModuleErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<SourceModuleResponse>(SourceModuleErrores.ErrorConsulta);
        }
    }
}
