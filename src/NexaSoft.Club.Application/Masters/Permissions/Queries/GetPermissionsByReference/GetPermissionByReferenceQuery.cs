using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Permissions.Queries.GetPermissionsByReference;

public sealed record GetPermissionByReferenceQuery
 : IQuery<List<PermissionReferenceResponse>>;


public sealed record PermissionItemResponse
(
    long Id,
    string Name    
);
    
public sealed record PermissionReferenceResponse
(    
    string Name,
    List<PermissionItemResponse> Items
);