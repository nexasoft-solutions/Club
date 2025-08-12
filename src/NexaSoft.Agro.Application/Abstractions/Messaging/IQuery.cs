using MediatR;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
    
}