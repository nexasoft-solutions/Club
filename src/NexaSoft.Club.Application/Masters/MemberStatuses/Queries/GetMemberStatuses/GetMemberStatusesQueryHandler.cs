using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.MemberStatuses;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.MemberStatuses.Queries.GetMemberStatuses;

public class GetMemberStatusesQueryHandler(
    IGenericRepository<MemberStatus> _repository
) : IQueryHandler<GetMemberStatusesQuery, Pagination<MemberStatusResponse>>
{
    public async Task<Result<Pagination<MemberStatusResponse>>> Handle(GetMemberStatusesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new MemberStatusSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<MemberStatusResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<MemberStatusResponse>(
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
            return Result.Failure<Pagination<MemberStatusResponse>>(MemberStatusErrores.ErrorConsulta);
        }
    }
}
