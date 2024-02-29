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
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> Get()  => await _databaseContext.Customers.ToListAsync();  
        public IActionResult Index()
        {
            return View();
        }
    }
}
