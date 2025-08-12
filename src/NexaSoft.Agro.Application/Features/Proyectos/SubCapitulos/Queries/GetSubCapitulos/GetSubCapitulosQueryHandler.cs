using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Features.Proyectos.SubCapitulos;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Features.Proyectos.SubCapitulos.Queries.GetSubCapitulos;

public class GetSubCapitulosQueryHandler(
    IGenericRepository<SubCapitulo> _repository
) : IQueryHandler<GetSubCapitulosQuery, Pagination<SubCapituloResponse>>
{
    public async Task<Result<Pagination<SubCapituloResponse>>> Handle(GetSubCapitulosQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new SubCapituloSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<SubCapituloResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<SubCapituloResponse>(
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
            return Result.Failure<Pagination<SubCapituloResponse>>(SubCapituloErrores.ErrorConsulta);
        }
    }
}
