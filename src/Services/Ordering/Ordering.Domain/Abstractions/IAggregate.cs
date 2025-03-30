

namespace Ordering.Domain.Abstractions
{
    /// <summary>
    /// Represents an aggregate root in Domain-Driven Design (DDD) with a generic type parameter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAggregate<T> : IAggregate, IEntity<T>
    {
        
    }

    /// <summary>
    /// Represents an aggregate root in Domain-Driven Design (DDD).
    /// This is a special kind of entity that can handle domain events.
    /// </summary>
    public interface IAggregate : IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        IDomainEvent[] ClearDomainEvents();
    }
}
