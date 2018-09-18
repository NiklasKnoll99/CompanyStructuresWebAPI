using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using CompanyStructuresWebAPI.Interface;
using CompanyStructuresWebAPI.Helper;

namespace CompanyStructuresWebAPI.Controller
{
    [Route("departments")]
    public class DepartmentController : Microsoft.AspNetCore.Mvc.Controller
    {
        readonly IDepartmentRepository _departmentRepo;

        public DepartmentController(IDepartmentRepository departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Model.Department> departments = _departmentRepo.GetDepartments();

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
            Model.Department department = _departmentRepo.GetDepartmentById(Id);

            if (department != null)
                return Ok(department);

            else
                return NoContent();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Model.Dto.DepartmentDto department)
        {
            if (Authenticator.isAuthenticated(Request.Headers["Authorization"]))
            {
                if (department == null)
                    return BadRequest();

                else
                {
                    // Exception handling
                    _departmentRepo.Create(department);

                    return Created("departments", department);
                }
            }

            else
                return Unauthorized();
        }

        [HttpPut]
        public IActionResult Put([FromBody] Model.Department department)
        {
            if (Authenticator.isAuthenticated(Request.Headers["Authorization"]))
            {
                if ((department == null) || (department.DepartmentName == null))
                    return BadRequest();

                else
                {
                    // Exception handling
                    _departmentRepo.Update(department);

                    return Ok();
                }
            }

            else
                return Unauthorized();
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            if (Authenticator.isAuthenticated(Request.Headers["Authorization"]))
            {
                // Exception handling
                if (_departmentRepo.Delete(Id) != -1)
                    return Ok();

                else
                    return NoContent();
            }

            else
                return Unauthorized();
        }
    }
}
