using BlazorApp.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssesmentEpsilon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public CustomerController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> Get() => await _databaseContext.Customers.ToListAsync();


        [HttpGet("{skip}/{take}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomers(int skip, int take)
        {

            //_databaseContext.Database.EnsureCreated();  
            var count = await _databaseContext.Customers.CountAsync();
            var customers = await _databaseContext.Customers!.Skip(skip).Take(take).ToListAsync();
            return Ok(new CustomerResponse(customers, count));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerByIdAsync(Guid id)
        {
            var result = await _databaseContext.Customers.FindAsync(id);
            if (result == null)
                return NotFound("Customer Not Found");

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomerAsync(Guid id)
        {
            var result = await _databaseContext.Customers.FindAsync(id);
            if (result == null)
                return NotFound("Customer Not Found");

            _databaseContext.Remove(result);
            await _databaseContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> UpdateCustomerAsync(Guid id, Customer customer)
        {
            var dbCustomer = await _databaseContext.Customers.FindAsync(id);
            if (dbCustomer == null)
                return NotFound("Customer Not Found");

            dbCustomer.ContactName = customer.ContactName;
            dbCustomer.CompanyName = customer.CompanyName;
            dbCustomer.Phone = customer.Phone;
            dbCustomer.City = customer.City;
            dbCustomer.Address = customer.Address;
            dbCustomer.Country = customer.Country;
            dbCustomer.PostalCode = customer.PostalCode;
            dbCustomer.Region = customer.Region;

            await _databaseContext.SaveChangesAsync();

            return Ok(dbCustomer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomerAsync(Customer newCustomer)
        {
            _databaseContext.Add(newCustomer);
            await _databaseContext.SaveChangesAsync();

            return Ok(newCustomer);
        }       
    }
}
