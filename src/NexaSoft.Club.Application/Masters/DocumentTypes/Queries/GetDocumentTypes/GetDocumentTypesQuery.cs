using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.DocumentTypes;

namespace NexaSoft.Club.Application.Masters.DocumentTypes.Queries.GetDocumentTypes;

public sealed record GetDocumentTypesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<DocumentTypeResponse>>;
