using Asp.Versioning;
using FunBooksAndVideos.API.Models;
using FunBooksAndVideos.Application.Customers.DTOs;
using FunBooksAndVideos.Application.Memberships.DTOs;
using FunBooksAndVideos.Application.Memberships.Interfaces;
using FunBooksAndVideos.Application.Products.Interfaces;
using FunBooksAndVideos.Domain.Entities.Customers;
using Microsoft.AspNetCore.Mvc;

namespace FunBooksAndVideos.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/customers")]
    [ApiVersion(1.0)]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        private readonly IMembershipService _membershipService;

        public CustomerController(ILogger<CustomerController> logger, 
            ICustomerService customerService,
            IMembershipService membershipService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _membershipService = membershipService ?? throw new ArgumentNullException(nameof(membershipService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> GetAll()
        {
            var customers = await _customerService.GetAll();
            return Ok(customers);
        }

        /// <summary>
        /// Retrieves a customer by their ID.
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>Customer details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerResponseDto>> GetById(Guid id)
        {
            var customer = await _customerService.GetById(id);
            if (customer == null)
                return NotFound($"Customer with ID {id} not found.");

            var customerResponseDto = new CustomerResponseDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone
            };

            return Ok(customerResponseDto);
        }


        /// <summary>
        /// Retrieves memberships of a customer by their ID.
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>List of memberships associated with the customer</returns>
        [HttpGet("{id}/memberships")]
        [ProducesResponseType(typeof(MembershipResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<MembershipResponseDto>> GetMembershipsByCustomerId(Guid id)
        {
            var dto = await _membershipService.GetMembershipsByCustomerId(id);
            if (!dto.Memberships.Any())
                return NotFound($"No memberships found for customer with ID {id}.");

            return Ok(dto);
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="dto">Customer details</param>
        /// <returns>The created customer's ID</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<CustomerResponseDto>> Create([FromBody] CustomerRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                _logger.LogWarning("Invalid request model: {Errors}", string.Join(", ", errors));
                return BadRequest(new ErrorResponse("Validation failed", errors));
            }

            var customer = new Customer
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone
            };

            var customerId = await _customerService.Create(customer);
            return CreatedAtAction(nameof(GetById), new { id = customerId }, customerId);
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <param name="customerRequestDto">Updated customer details</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Update(Guid id, [FromBody] CustomerRequestDto customerRequestDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                _logger.LogWarning("Invalid request model: {Errors}", string.Join(", ", errors));
                return BadRequest(new ErrorResponse("Validation failed", errors));
            }

            var customer = await _customerService.GetById(id);
            if (customer == null)
                return NotFound($"Customer with ID {id} not found.");

            customer.FirstName = customerRequestDto.FirstName;
            customer.LastName = customerRequestDto.LastName;
            customer.Email = customerRequestDto.Email;
            customer.Phone = customerRequestDto.Phone;

            await _customerService.Update(customer);
            return NoContent();
        }

        /// <summary>
        /// Deletes a customer by ID.
        /// </summary>
        /// <param name="id">Customer ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var customer = await _customerService.GetById(id);
            if (customer == null)
                return NotFound($"Customer with ID {id} not found.");

            await _customerService.Delete(customer);
            return NoContent();
        }
    }
}
