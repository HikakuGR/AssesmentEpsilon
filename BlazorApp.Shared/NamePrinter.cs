using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared
{
    public class NamePrinter
    {
        public void PrintName(IPerson person)
        {
            Console.WriteLine(person.Name );
        }
    }
}
