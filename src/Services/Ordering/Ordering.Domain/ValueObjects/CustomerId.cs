
namespace Ordering.Domain.ValueObjects
{
    public record CustomerId
    {
        public Guid Value { get; }
        private CustomerId(Guid value) => Value = value;

        /// <summary>
        /// Of ensures that the CustomerId is created with a valid Guid. not empty.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="DomainException"></exception>
        public static CustomerId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("CustomerId cannot be empty.");
            }

            return new CustomerId(value);
        }
    }
}
