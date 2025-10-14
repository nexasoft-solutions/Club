using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.DocumentTypes.Commands.CreateDocumentType;

public sealed record CreateDocumentTypeCommand(
    string? Name,
    string? Description,
    string? Serie,
    string CreatedBy
) : ICommand<long>;
