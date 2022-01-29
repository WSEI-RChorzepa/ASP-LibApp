using AutoMapper;
using LibApp.Application.Core.Contracts.Persistence;
using LibApp.Application.Core.Dtos;
using LibApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.API.Controllers
{
    public class CustomersController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(IMapper mapper, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomsers()
        {
            var entities = await _customerRepository.BrowseAsync();

            if (!entities.Any())
                return Ok(new List<CustomerDto>());

            var customers = _mapper.Map<List<CustomerDto>>(entities);

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomser(int id)
        {
            var entity = await _customerRepository.GetAsync(id);

            if(entity == null)
                return NotFound();

            var customer = _mapper.Map<CustomerDto>(entity);

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto customerDto)
        {
            var model = _mapper.Map<Customer>(customerDto);

            if (!TryValidateModel(model, nameof(Customer)))
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _customerRepository.CreateAsync(model);
            
            return CreatedAtAction(nameof(GetCustomser), new { id = model.Id }, model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDto customerDto)
        {
            var customerInDb = await _customerRepository.GetAsync(customerDto.Id);

            if (customerInDb == null)
                return NotFound();

            _mapper.Map(customerDto, customerInDb);

            if (!TryValidateModel(customerInDb, nameof(Customer)))
                return BadRequest(ModelState);
            
            await _customerRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var entity = await _customerRepository.GetAsync(id);

            if (entity == null)
                return NotFound();

            await _customerRepository.DeleteAsync(id);

            return Ok();
        }
    }
}
