using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.MemberTypes;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.MemberTypes.Queries.GetMemberType;

public class GetMemberTypeQueryHandler(
    IGenericRepository<MemberType> _repository
) : IQueryHandler<GetMemberTypeQuery, MemberTypeResponse>
{
    public async Task<Result<MemberTypeResponse>> Handle(GetMemberTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new MemberTypeSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<MemberTypeResponse>(MemberTypeErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<MemberTypeResponse>(MemberTypeErrores.ErrorConsulta);
        }
    }
}
