using FunBooksAndVideos.Application.Customers.DTOs;
using FunBooksAndVideos.Application.Products.Interfaces;
using FunBooksAndVideos.Domain.Entities.Customers;
using FunBooksAndVideos.Infrastructure.Persistence.Interfaces;

namespace FunBooksAndVideos.Application.Products.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CustomerResponseDto>> GetAll()
        {
            var customers = await _unitOfWork.Customers.GetAll();
            var dto = customers.Select(customer => new CustomerResponseDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone
            });

            return dto;
        }

        public async Task<Customer?> GetById(Guid id)
        {
            return await _unitOfWork.Customers.GetById(id);
        }

        public async Task<Guid> Create(Customer customer)
        {
            var customerId = await _unitOfWork.Customers.Create(customer);
            await _unitOfWork.SaveChangesAsync();
            return customerId;
        }

        public async Task Update(Customer customer)
        {
            _unitOfWork.Customers.Update(customer);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Delete(Customer customer)
        {
            _unitOfWork.Customers.Delete(customer);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
