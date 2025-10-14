using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.DocumentTypes.Commands.UpdateDocumentType;

public sealed record UpdateDocumentTypeCommand(
    long Id,
    string? Name,
    string? Description,
    string? Serie,
    string UpdatedBy
) : ICommand<bool>;
