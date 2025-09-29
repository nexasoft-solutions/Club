using MediatR;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result> , IBaseCommand
{
    
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>> , IBaseCommand
{

}

public interface IBaseCommand
{

}
