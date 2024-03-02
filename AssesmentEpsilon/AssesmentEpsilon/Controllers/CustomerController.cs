using AssesmentEpsilon.Services;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssesmentEpsilon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {        
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> Get() => await _customerService.GetAll();


        [HttpGet("{skip}/{take}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomers(int skip, int take)
        {                       
            return  Ok(await _customerService.GetPaged(skip,take));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerByIdAsync(Guid id)
        {
            var result = await _customerService.Get(id);
            if (result == null)
                return NotFound("Customer Not Found");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomerAsync(Guid id)
        {
            await _customerService.Remove(id);   
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> UpdateCustomerAsync(Guid id, Customer customer)
        {
            var result = await _customerService.Update(id, customer);
            if (result == null)
                return NotFound("Customer Not Found");
            return Ok(result);            
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomerAsync(Customer newCustomer)
        {
            return Ok(await _customerService.Create(newCustomer));
        }       
    }
}
