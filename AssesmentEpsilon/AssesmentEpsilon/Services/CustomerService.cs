
using BlazorApp.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssesmentEpsilon.Services
{    

    public class CustomerService : ICustomerService
    {
        private readonly DatabaseContext _databaseContext;
        public CustomerService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task<List<Customer>> GetAllAsync()
        {
            var query = from b in _databaseContext.Customers                        
                        select b;
            return await query.ToListAsync();
            //return await _databaseContext.Customers.;
        }
        
        public async Task<Customer> Get(Guid id)
        {
            Customer? result = await _databaseContext.Customers.FindAsync(id);
            return result;
        }


        public async Task<CustomerResponse> GetPaged(int skip, int take)
        {
            var count = await _databaseContext.Customers.CountAsync();
            var customers = await _databaseContext.Customers!.Skip(skip).Take(take).ToListAsync();
            return (new CustomerResponse(customers, count));

        }

        public async Task<Customer> CreateAsync(Customer newCustomer)
        {
            await _databaseContext.AddAsync(newCustomer);
            await _databaseContext.SaveChangesAsync();
            return newCustomer;
        }        
        public async Task<ActionResult<Customer>> Update(Guid id, Customer customer)
        {

            var dbCustomer = await _databaseContext.Customers.FindAsync(id);
            if (dbCustomer == null)
                return null;

            dbCustomer.ContactName = customer.ContactName;
            dbCustomer.CompanyName = customer.CompanyName;
            dbCustomer.Phone = customer.Phone;
            dbCustomer.City = customer.City;
            dbCustomer.Address = customer.Address;
            dbCustomer.Country = customer.Country;
            dbCustomer.PostalCode = customer.PostalCode;
            dbCustomer.Region = customer.Region;

            await _databaseContext.SaveChangesAsync();

            return dbCustomer;
        }

        public async Task Remove(Guid id)
        {
            var result = await _databaseContext.Customers.FindAsync(id);
            if (result != null)
                _databaseContext.Remove(result);
            await _databaseContext.SaveChangesAsync();
        }

        public int GetCustomerCount()
        {
            var a = _databaseContext.Customers.Count();
            return a;
        }
    }
}
