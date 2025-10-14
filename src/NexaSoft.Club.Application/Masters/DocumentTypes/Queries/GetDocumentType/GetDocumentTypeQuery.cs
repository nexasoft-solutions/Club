using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.DocumentTypes;

namespace NexaSoft.Club.Application.Masters.DocumentTypes.Queries.GetDocumentType;

public sealed record GetDocumentTypeQuery(
    long Id
) : IQuery<DocumentTypeResponse>;
