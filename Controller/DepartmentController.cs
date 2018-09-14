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
                return Ok(departments);
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

        [HttpPost("newdepartment")]
        public IActionResult Post([FromBody] Model.Dto.DepartmentDto department)
        {
            if (department == null)
                return BadRequest();

            else
            {
                Repository.DepartmentRepository departmentRepo = new Repository.DepartmentRepository();

                // Check for successful company construction
                departmentRepo.Create(department);

                return Created("newdepartment", department);
            }
        }
    }
}
