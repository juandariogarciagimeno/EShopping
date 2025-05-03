namespace EShopping.Ordering.Domain.Events;
public record OrderUpdatedEvent(Order Order) : IDomainEvent;