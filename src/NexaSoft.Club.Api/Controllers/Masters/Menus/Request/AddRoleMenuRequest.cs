namespace NexaSoft.Club.Api.Controllers.Masters.Menus.Request;

public sealed record  AddRoleMenuRequest
(
    long MenuId,
    List<long> RoleIds
);