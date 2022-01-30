using AutoMapper;
using LibApp.Application.Core.Contracts.Persistence;
using LibApp.Application.Core.Dtos;
using LibApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.WebUI.Controllers.Api
{
    public class CustomersController : BaseApiController
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(IMapper mapper, ICustomerRepository customerRepository):base(mapper)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomsers()
        {
            var entities = await _customerRepository.BrowseAsync();

            if (!entities.Any())
                return Ok(new List<CustomerDto>());

            var customers = Mapper.Map<List<CustomerDto>>(entities);

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomser(int id)
        {
            var entity = await _customerRepository.GetAsync(id);

            if (entity == null)
                return NotFound();

            var customer = Mapper.Map<CustomerDto>(entity);

            return Ok(customer);
        }


        [HttpPut]
        [Authorize(Policy ="EditAcesss")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDto customerDto)
        {
            var customerInDb = await _customerRepository.GetAsync(customerDto.Id);

            if (customerInDb == null)
                return NotFound();

            Mapper.Map(customerDto, customerInDb);

            if (!TryValidateModel(customerInDb, nameof(Customer)))
                return BadRequest(ModelState);

            await _customerRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "EditAccess")]
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
