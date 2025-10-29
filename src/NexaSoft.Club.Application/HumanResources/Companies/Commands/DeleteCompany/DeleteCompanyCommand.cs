using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.Companies.Commands.DeleteCompany;

public sealed record DeleteCompanyCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
