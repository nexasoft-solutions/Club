using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Roles;
using NexaSoft.Club.Domain.Masters.Users;

namespace NexaSoft.Club.Application.Masters.Users.Queries.GetUserRoles;

public class GetUserRolesQueryHandler(
    IUserRoleRepository _userRepository
) : IQueryHandler<GetUserRolesQuery, List<UserRoleResponse>>
{
    public async Task<Result<List<UserRoleResponse>>> Handle(GetUserRolesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var roles = await _userRepository.GetUserRolesAsync(query.UserId, cancellationToken);
            

            return Result.Success(roles);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<List<UserRoleResponse>>(UserErrores.ErrorConsulta);
        }
    }
}

