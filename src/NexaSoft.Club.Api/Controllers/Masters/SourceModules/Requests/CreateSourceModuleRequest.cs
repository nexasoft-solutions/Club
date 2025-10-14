namespace NexaSoft.Club.Api.Controllers.Masters.SourceModules.Request;

public sealed record CreateSourceModuleRequest(
    string? Name,
    string? Description,
    string CreatedBy
);
