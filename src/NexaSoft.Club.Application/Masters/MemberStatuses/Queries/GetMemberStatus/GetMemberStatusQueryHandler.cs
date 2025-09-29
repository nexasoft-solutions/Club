using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.MemberStatuses;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.MemberStatuses.Queries.GetMemberStatus;

public class GetMemberStatusQueryHandler(
    IGenericRepository<MemberStatus> _repository
) : IQueryHandler<GetMemberStatusQuery, MemberStatusResponse>
{
    public async Task<Result<MemberStatusResponse>> Handle(GetMemberStatusQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new MemberStatusSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<MemberStatusResponse>(MemberStatusErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<MemberStatusResponse>(MemberStatusErrores.ErrorConsulta);
        }
    }
}
