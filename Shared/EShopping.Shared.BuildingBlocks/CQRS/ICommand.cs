using MediatR;

namespace EShopping.Shared.BuildingBlocks.CQRS
{
    public interface ICommand : IRequest<Unit>
    {
    }

    public interface ICommand<out TRes> : IRequest<TRes>
    {
    }
}
