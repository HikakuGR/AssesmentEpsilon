﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared
{
    public class Employee : IPerson
    {
        public string Name { get; set; } = "Employee Name";
    }
}

