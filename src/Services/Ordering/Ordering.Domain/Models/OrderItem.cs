
namespace Ordering.Domain.Models
{
    public class OrderItem: Entity<OrderItemId>
    {
        /// <summary>
        /// This is an example of primative obession, we are swapping this out for Strongly Typed Ids.
        /// OrderItem(Guid orderId, Guid productId, int quantity, decimal price)
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <param name="price"></param>
        internal OrderItem(OrderId orderId, ProductId productId, int quantity, decimal price)
        {        
            Id = OrderItemId.Of(Guid.NewGuid()); 
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public OrderId OrderId { get; private set; } = default!;
        public ProductId ProductId { get; private set; } = default!;
        public int Quantity { get; private set; } = default!;
        public decimal Price { get; private set; } = default!;
    }
}
