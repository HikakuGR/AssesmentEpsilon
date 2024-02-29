using AssesmentEpsilon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared
{
    public record struct  CustomerResponse(List<Customer> Customers,int Count);
    
}
