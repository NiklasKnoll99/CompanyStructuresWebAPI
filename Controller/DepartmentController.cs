using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using CompanyStructuresWebAPI.Interface;
using CompanyStructuresWebAPI.Helper;
using CompanyStructuresWebAPI.APIException;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace CompanyStructuresWebAPI.Controller
{
    [Route("departments")]
    public class DepartmentController : Microsoft.AspNetCore.Mvc.Controller
    {
        readonly IDepartmentRepository _departmentRepo;

        Logger _logger;

        public DepartmentController(ILoggerFactory loggerFactory, IDepartmentRepository departmentRepo)
        {
            _departmentRepo = departmentRepo;
            _logger = new Logger(loggerFactory.CreateLogger<DepartmentController>());
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Model.Department> departments = null;

            try
            {
                departments = _departmentRepo.GetDepartments();
            }

            catch (RepositoryException rEx)
            {
                _logger.Log(rEx.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

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
            Model.Department department = null;

            try
            {
                department = _departmentRepo.GetDepartmentById(Id);
            }

            catch (RepositoryException rEx)
            {
                _logger.Log(rEx.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

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
                    try
                    {
                        _departmentRepo.Create(department);
                    }

                    catch (RepositoryException rEx)
                    {
                        _logger.Log(rEx.Message);
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }

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
                    try
                    {
                        _departmentRepo.Update(department);
                    }

                    catch (RepositoryException rEx)
                    {
                        _logger.Log(rEx.Message);
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }

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
                int result = 0;

                try
                {
                    result = _departmentRepo.Delete(Id);
                }

                catch (RepositoryException rEx)
                {
                    _logger.Log(rEx.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                if (result != -1)
                    return Ok();

                else
                    return NoContent();
            }

            else
                return Unauthorized();
        }
    }
}
