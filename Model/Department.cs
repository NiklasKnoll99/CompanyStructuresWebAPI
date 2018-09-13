using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyStructuresWebAPI.Model
{
    public class Department
    {
        public int Id { get; set; }

        public string DepartmentName { get; set; }

        public string CompanyName { get; set; }
    }
}
