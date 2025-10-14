using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.DocumentTypes.Commands.DeleteDocumentType;

public sealed record DeleteDocumentTypeCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
