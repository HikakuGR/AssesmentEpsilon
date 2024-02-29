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

        public CustomerController(DatabaseContext databaseContext )
        {
            _databaseContext = databaseContext;
        }
        //[HttpGet]
        //public async Task<ActionResult<List<Customer>>> Get()  => await _databaseContext.Customers.ToListAsync();
        [HttpGet("{skip}/{take}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomers(int skip,int take) {

            //_databaseContext.Database.EnsureCreated();  
            var count = await _databaseContext.Customers.CountAsync();
            var customers = await _databaseContext.Customers!.Skip(skip).Take(take).ToListAsync(); 
            return Ok(new CustomerResponse(customers,count));
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}
