using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Features.Proyectos.Capitulos;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Features.Proyectos.Capitulos.Queries.GetCapitulos;

public class GetCapitulosQueryHandler(
    IGenericRepository<Capitulo> _repository
) : IQueryHandler<GetCapitulosQuery, Pagination<CapituloResponse>>
{
    public async Task<Result<Pagination<CapituloResponse>>> Handle(GetCapitulosQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new CapituloSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<CapituloResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<CapituloResponse>(
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
            return Result.Failure<Pagination<CapituloResponse>>(CapituloErrores.ErrorConsulta);
        }
    }
}
