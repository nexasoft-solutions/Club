namespace NexaSoft.Club.Api.Controllers.Masters.FeeConfigurations.Request;

public sealed record DeleteFeeConfigurationRequest(
   long Id,
   string DeletedBy
);
