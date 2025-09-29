using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Features.FamilyMembers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Features.FamilyMembers.Queries.GetFamilyMember;

public class GetFamilyMemberQueryHandler(
    IGenericRepository<FamilyMember> _repository
) : IQueryHandler<GetFamilyMemberQuery, FamilyMemberResponse>
{
    public async Task<Result<FamilyMemberResponse>> Handle(GetFamilyMemberQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new FamilyMemberSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<FamilyMemberResponse>(FamilyMemberErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<FamilyMemberResponse>(FamilyMemberErrores.ErrorConsulta);
        }
    }
}
