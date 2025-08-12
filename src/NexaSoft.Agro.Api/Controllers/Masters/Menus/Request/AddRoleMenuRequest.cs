namespace NexaSoft.Agro.Api.Controllers.Masters.Menus.Request;

public sealed record  AddRoleMenuRequest
(
    Guid MenuId,
    List<Guid> RoleIds
);