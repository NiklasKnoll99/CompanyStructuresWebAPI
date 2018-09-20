using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CompanyStructuresWebAPI.Repository;
using CompanyStructuresWebAPI.Helper;
using CompanyStructuresWebAPI.APIException;
using CompanyStructuresWebAPI.Constant;
using Microsoft.Extensions.Logging;

namespace CompanyStructuresWebAPI.Interface
{
    [Route("companies")]
    public class CompanyController : Microsoft.AspNetCore.Mvc.Controller
    {
        readonly ICompanyRepository _companyRepo;

        Logger _logger;

        public CompanyController(ILoggerFactory loggerFactory, ICompanyRepository companyRepo)
        {
            _companyRepo = companyRepo;
            _logger = new Logger(loggerFactory.CreateLogger<CompanyController>());
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Model.Company> companies = null;

            try
            {
                companies = _companyRepo.GetCompanies();
            }

            catch (RepositoryException rEx)
            {
                _logger.Log(rEx.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            if (companies != null)
            {
                return Ok(companies);
            }

            else
            {
                return NoContent();
            }
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            Model.Company company = null;

            try
            {
                company = _companyRepo.GetCompanyById(Id);
            }

            catch (RepositoryException rEx)
            {
                _logger.Log(rEx.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            if (company != null)
                return Ok(company);

            else
                return NoContent();
        }

        [HttpPost]
        public IActionResult Post([FromBody]Model.Dto.CompanyDto company)
        {
            if (Authenticator.isAuthenticated(Request.Headers["Authorization"]))
            {
                if (company == null)
                    return BadRequest();

                else
                {
                    
                    try
                    {
                        _companyRepo.Create(company);
                    }

                    catch (RepositoryException rEx)
                    {
                        _logger.Log(rEx.Message);
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }

                    return Created("companies", company);
                }
            }

            else
                return Unauthorized();
        }

        [HttpPut]
        public IActionResult Put([FromBody] Model.Company company)
        {
            if (Authenticator.isAuthenticated(Request.Headers["Authorization"]))
            {
                if (company == null)
                    return BadRequest();

                else
                {
                    try
                    {
                        _companyRepo.Update(company);
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
                    result = _companyRepo.Delete(Id);
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
