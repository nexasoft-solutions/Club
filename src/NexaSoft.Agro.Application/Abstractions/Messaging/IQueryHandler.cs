using MediatR;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery,TResponse> : IRequestHandler<TQuery,Result<TResponse>>
where TQuery : IQuery<TResponse>
{
    
}