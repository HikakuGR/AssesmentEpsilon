
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
        public async Task<List<Customer>> GetAll()
        {
           var customers = await _databaseContext.Customers.ToListAsync();
            return customers;
        }
    }
}
