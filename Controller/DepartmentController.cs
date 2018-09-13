using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace CompanyStructuresWebAPI.Controller
{
    [Route("departments")]
    public class DepartmentController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult GetAll()
        {
            Repository.DepartmentRepository departmentRepo = new Repository.DepartmentRepository();

            List<Model.Department> departments = departmentRepo.GetDepartments();

            if (departments != null)
            {
                return Ok(new JsonResult(departments));
            }

            else
            {
                return NoContent();
            }
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            Repository.DepartmentRepository departmentRepo = new Repository.DepartmentRepository();
            Model.Department department = departmentRepo.GetDepartmentById(Id);

            if (department != null)
                return Ok(department);

            else
                return NoContent();
        }
    }
}
