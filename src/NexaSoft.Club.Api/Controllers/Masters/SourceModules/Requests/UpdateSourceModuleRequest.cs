namespace NexaSoft.Club.Api.Controllers.Masters.SourceModules.Request;

public sealed record UpdateSourceModuleRequest(
   long Id,
    string? Name,
    string? Description,
    string UpdatedBy
);
