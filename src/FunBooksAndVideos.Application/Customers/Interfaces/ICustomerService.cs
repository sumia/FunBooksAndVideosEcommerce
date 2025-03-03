using FunBooksAndVideos.Application.Customers.DTOs;
using FunBooksAndVideos.Domain.Entities.Customers;

namespace FunBooksAndVideos.Application.Products.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerResponseDto>> GetAll();
        Task<Customer?> GetById(Guid id);
        Task<Guid> Create(Customer customer);
        Task Update(Customer customer);
        Task Delete(Customer customer);
    }
}
