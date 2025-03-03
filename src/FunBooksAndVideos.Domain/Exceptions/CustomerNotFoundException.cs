using FunBooksAndVideos.Domain.Exceptions;

namespace FunBooksAndVideos.Application.Features.Products.Exceptions
{
    public class CustomerNotFoundException : DomainException
    {
        public CustomerNotFoundException(Guid customerId)
           : base($"Customer with ID {customerId} was not found.") { }

        public CustomerNotFoundException(string message) : base(message) { }
    }
}
