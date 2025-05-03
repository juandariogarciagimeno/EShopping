using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EShopping.Ordering.Infrastructure.Data.Interceptors;

public class DomainEventDispatcherInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchEvents(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        DispatchEvents(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void DispatchEvents(DbContext? context)
    {
        if (context == null) return;
        var entries = context.ChangeTracker.Entries<IAggregate>().Where(a => a.Entity.DomainEvents.Any()).Select(a => a.Entity).ToList();
        var domainEvents = entries.SelectMany(a => a.DomainEvents).ToList();

        entries.ForEach(x => x.ClearDomainEvents());

        domainEvents.ForEach(async x => await mediator.Publish(x));
    }
}
