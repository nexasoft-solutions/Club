using MediatR;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Abstractions.Messaging;


// Manejador de comando que retorna un Result (sin tipo genérico).
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{ }

// Manejador de comando que retorna un Result<TResponse> (con tipo genérico).
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{ }


