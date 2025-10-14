namespace NexaSoft.Club.Api.Controllers.Masters.SourceModules.Request;

public sealed record DeleteSourceModuleRequest(
   long Id,
   string DeletedBy
);
