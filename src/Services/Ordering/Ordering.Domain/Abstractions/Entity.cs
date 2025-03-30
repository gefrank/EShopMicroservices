
namespace Ordering.Domain.Abstractions
{
    /// <summary>
    /// The Entity<T> abstract class provides a base implementation for entities in the domain, ensuring that all entities have 
    /// common properties such as Id, CreatedAt, CreatedBy, LastModified, and LastModifiedBy. By implementing the IEntity<T> interface, 
    /// it enforces a contract that any derived class must adhere to.
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Entity<T> : IEntity<T>
    {
        public T Id { get; set; } = default!;
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
