
namespace BuildingBlocks.Messaging.Events
{
    /// <summary>
    /// Base class for integration events.
    /// Ensures standard structure for all integration events.
    /// </summary>
    public record IntegrationEvent
    {
        public Guid Id => Guid.NewGuid();
        public DateTime OccuredOn => DateTime.Now;
        public string EventType => GetType().AssemblyQualifiedName;
    }
}
