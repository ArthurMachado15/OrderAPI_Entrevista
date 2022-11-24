using Microsoft.AspNetCore.Mvc;
using OrderAPI.Data.Models;
using OrderAPI.Data.Repositories;
using OrderAPI.DTO;
using System.Net;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAllCustomers()
        {
            try
            {
                IEnumerable<CustomerEntity> CustomerList = await _customerRepository.Get();

                IEnumerable<CustomerDTO> customers = CustomerList.Select(customer => new CustomerDTO()
                {
                    Id = customer.Id,
                    Email = customer.Email,
                    Name = customer.Name
                });

                return StatusCode(200, customers);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro ocorrido: " + e.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomerById(int id) 
        {
            try
            {
                if (await _customerRepository.Get(id) == null)
                    return NotFound("Cliente não encontrado!");

                await _customerRepository.Delete(id);
                return StatusCode(200, "Cliente excluído com sucesso!");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro ocorrido: " + e.Message);
            }
        }
        
        [HttpPut]
        public async Task<ActionResult> UpdateCustomer([FromBody] CustomerDTO customer)
        {
            try
            {
                if (customer.Id == 0)
                    return StatusCode(400, "ID com valor igual a zero!");

                CustomerEntity entity = new CustomerEntity()
                {
                    Id = customer.Id,
                    Email = customer.Email,
                    Name = customer.Name,
                };

                await _customerRepository.Update(entity);
                return StatusCode(200, "Cliente atualizado com sucesso!");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro ocorrido: " + e.Message);
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustumerById(int id) 
        {
            try
            {
                CustomerEntity entity = await _customerRepository.Get(id);

                if (entity == null)
                {
                    return NotFound("Cliente não encontrado!");
                }

                CustomerDTO dtoCustomer = new CustomerDTO()
                {
                    Id = entity.Id,
                    Email = entity.Email,
                    Name = entity.Name,
                };

                return StatusCode(200, dtoCustomer);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro ocorrido: " + e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> CreateNewCustumer([FromBody] CustomerDTO customer) 
        {
            try
            {
                CustomerEntity entity = new CustomerEntity()
                {
                    Email = customer.Email,
                    Name = customer.Name,
                };

                CustomerEntity newCustomer = await _customerRepository.Create(entity);
                customer.Id = newCustomer.Id;

                return StatusCode(200, customer);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro ocorrido: " + e.Message);
            }

        }
    }
}
