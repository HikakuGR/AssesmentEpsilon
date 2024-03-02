using AssesmentEpsilon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared
{
    public record struct  CustomerResponse(List<Customer> Customers,int Count);

    public class Kati {
        public List<Customer>? Customers { get; set; } = new();
        public int Count { get; set; }  
    }

}
