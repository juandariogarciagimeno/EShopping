using MediatR;

namespace EShopping.Shared.BuildingBlocks.CQRS
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Unit>
        where TCommand : ICommand   
    {
    }

    public interface ICommandHandler<in TCommand, TRes> : IRequestHandler<TCommand, TRes>
        where TCommand : ICommand<TRes>
        where TRes : notnull
    {
    }
}
