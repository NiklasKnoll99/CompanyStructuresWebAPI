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

namespace CompanyStructuresWebAPI.Interface
{
    [Route("companies")]
    public class CompanyController : Microsoft.AspNetCore.Mvc.Controller
    {
        readonly ICompanyRepository _companyRepo;

        public CompanyController(ICompanyRepository companyRepo)
        {
            _companyRepo = companyRepo;
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
                switch (rEx.GetExType())
                {
                    case (short)RepositoryException.Type.CONNECTION_EXCEPTION:
                        Console.WriteLine(ExceptionConstants.ConnectionErrorMsg);
                        break;

                    case (short)RepositoryException.Type.READ_EXCEPTION:
                        Console.WriteLine(ExceptionConstants.ReadErrorMsg);
                        break;

                    case (short)RepositoryException.Type.SPROCEDURE_EXECUTION_EXCEPTION:
                        Console.WriteLine(ExceptionConstants.ExecutionErrorMsg);
                        break;
                }
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
            Model.Company company = _companyRepo.GetCompanyById(Id);

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
                    // Exception handling
                    _companyRepo.Create(company);

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
                    // Exception handling
                    _companyRepo.Update(company);

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
                if (_companyRepo.Delete(Id) != -1)
                    return Ok();

                else
                    return NoContent();
            }

            else
                return Unauthorized();
        }
    }
}
