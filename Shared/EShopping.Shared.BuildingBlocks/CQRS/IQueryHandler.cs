using MediatR;

namespace EShopping.Shared.BuildingBlocks.CQRS
{
    public interface IQueryHandler<in TQuery, TRes> : IRequestHandler<TQuery, TRes>
        where TQuery : IQuery<TRes>
        where TRes : notnull
    {
    }
}
