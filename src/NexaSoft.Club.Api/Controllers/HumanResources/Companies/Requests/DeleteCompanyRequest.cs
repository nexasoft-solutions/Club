namespace NexaSoft.Club.Api.Controllers.HumanResources.Companies.Request;

public sealed record DeleteCompanyRequest(
   long Id,
   string DeletedBy
);
