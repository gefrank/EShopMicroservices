using BuildingBlocks.Exceptions;

namespace Catalog.API.Exceptions
{
    /// <summary>
    /// Exception thrown when a requested product cannot be found in the catalog.
    /// Inherits from NotFoundException and automatically formats error messages
    /// with product ID information for consistent error handling and client feedback.
    /// </summary>  
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid Id) : base("Product", Id)
        {

        }
    }
}
