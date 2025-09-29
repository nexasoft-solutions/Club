using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Features.Members.Queries.GetMembers;

public class GetMembersQueryHandler(
    IGenericRepository<Member> _repository
) : IQueryHandler<GetMembersQuery, Pagination<MemberResponse>>
{
    public async Task<Result<Pagination<MemberResponse>>> Handle(GetMembersQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new MemberSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<MemberResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<MemberResponse>(
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
            return Result.Failure<Pagination<MemberResponse>>(MemberErrores.ErrorConsulta);
        }
    }
}
