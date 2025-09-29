using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Features.Members.Queries.GetMember;

public class GetMemberQueryHandler(
    IGenericRepository<Member> _repository
) : IQueryHandler<GetMemberQuery, MemberResponse>
{
    public async Task<Result<MemberResponse>> Handle(GetMemberQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new MemberSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<MemberResponse>(MemberErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<MemberResponse>(MemberErrores.ErrorConsulta);
        }
    }
}
