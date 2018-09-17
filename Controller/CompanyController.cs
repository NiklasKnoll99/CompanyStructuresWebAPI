using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using CompanyStructuresWebAPI.Repository;

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

        public IActionResult GetAll()
        {
            List<Model.Company> companies = _companyRepo.GetCompanies();

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
            if (company == null)
                return BadRequest();

            else
            {
                // Exception handling
                _companyRepo.Create(company);

                return Created("companies", company);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Model.Company company)
        {
            if ((company == null) || (company.CompanyName == null))
                return BadRequest();

            else
            {
                // Exception handling
                _companyRepo.Update(company);

                return Ok();
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            // Exception handling
            if (_companyRepo.Delete(Id) != -1)
                return Ok();

            else
                return NoContent();
        }
    }
}
