using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.Companies;

namespace NexaSoft.Club.Application.HumanResources.Companies.Queries.GetCompanies;

public sealed record GetCompaniesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<CompanyResponse>>;
