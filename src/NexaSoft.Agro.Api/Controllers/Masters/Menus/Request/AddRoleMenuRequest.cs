namespace NexaSoft.Agro.Api.Controllers.Masters.Menus.Request;

public sealed record  AddRoleMenuRequest
(
    long MenuId,
    List<long> RoleIds
);