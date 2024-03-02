using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared
{
    public class Manager:IPerson
    {
        public string? Name { get; set; }="ManagerName"
    }
}
