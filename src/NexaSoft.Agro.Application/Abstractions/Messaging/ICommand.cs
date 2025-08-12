using MediatR;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result> , IBaseCommand
{
    
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>> , IBaseCommand
{

}

public interface IBaseCommand
{

}
