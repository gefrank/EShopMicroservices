using MediatR;

namespace Ordering.Domain.Abstractions
{
    /// <summary>
    /// Allows domain events to be dispatched through the MediatR pipeline.
    /// </summary>
    public interface IDomainEvent : INotification
    {
        Guid EventId => Guid.NewGuid();
        public DateTime OccurredOn => DateTime.Now;
        public string EventType => GetType().AssemblyQualifiedName;
    }
}
