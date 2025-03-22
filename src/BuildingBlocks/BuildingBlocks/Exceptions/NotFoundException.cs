
namespace BuildingBlocks.Exceptions
{
    /// <summary>
    /// Custom exception class for handling not found scenarios in the application.
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) 
        { 
        }

        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
        {

        }
    }
}
