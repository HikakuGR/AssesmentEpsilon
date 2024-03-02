using BlazorApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AssesmentEpsilon.Services
{
    public interface ICustomerService
    {
        Task<Customer> Create(Customer newCustomer);
        Task<Customer> Get(Guid id);
        Task<List<Customer>> GetAll();
        Task<CustomerResponse> GetPaged(int skip, int take);
        Task Remove(Guid id);
        Task<ActionResult<Customer>> Update(Guid id, Customer customer);
    }
}
