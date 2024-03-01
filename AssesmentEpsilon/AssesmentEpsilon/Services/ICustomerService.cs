namespace AssesmentEpsilon.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAll();
    }
}
