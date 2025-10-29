using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.Companies;

namespace NexaSoft.Club.Application.HumanResources.Companies.Queries.GetCompany;

public sealed record GetCompanyQuery(
    long Id
) : IQuery<CompanyResponse>;
