using MediatR;

namespace EShopping.Shared.BuildingBlocks.CQRS
{
    public interface IQuery<TRes> : IRequest<TRes>
        where TRes : notnull
    {
    }
}
