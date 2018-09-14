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
            Repository.DepartmentRepository departmentRepo = Repository.DepartmentRepository.GetInstance();

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
            Repository.DepartmentRepository departmentRepo = Repository.DepartmentRepository.GetInstance();
            Model.Department department = departmentRepo.GetDepartmentById(Id);

            if (department != null)
                return Ok(department);

            else
                return NoContent();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Model.Dto.DepartmentDto department)
        {
            if (department == null)
                return BadRequest();

            else
            {
                Repository.DepartmentRepository departmentRepo = Repository.DepartmentRepository.GetInstance();

                // Exception handling
                departmentRepo.Create(department);

                return Created("departments", department);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Model.Department department)
        {
            if ((department == null) || (department.DepartmentName == null))
                return BadRequest();

            else
            {
                Repository.DepartmentRepository departmentRepo = Repository.DepartmentRepository.GetInstance();

                // Exception handling
                departmentRepo.Update(department);

                return Ok();
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            Repository.DepartmentRepository departmentRepo = Repository.DepartmentRepository.GetInstance();

            // Exception handling
            departmentRepo.Delete(Id);

            return Ok();
        }
    }
}
