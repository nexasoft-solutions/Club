using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Features.Proyectos.Responsables;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Features.Proyectos.Responsables.Queries.GetResponsables;

public class GetResponsablesQueryHandler(
    IGenericRepository<Responsable> _repository
) : IQueryHandler<GetResponsablesQuery, Pagination<ResponsableResponse>>
{
    public async Task<Result<Pagination<ResponsableResponse>>> Handle(GetResponsablesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new ResponsableSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<ResponsableResponse>(spec, cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<ResponsableResponse>(
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
            return Result.Failure<Pagination<ResponsableResponse>>(ResponsableErrores.ErrorConsulta);
        }
    }
}
