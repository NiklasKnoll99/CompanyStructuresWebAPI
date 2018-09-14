using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace CompanyStructuresWebAPI.Controller
{
    [Route("companies")]
    public class CompanyController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult GetAll()
        {
            Repository.CompanyRepository companyRepo = Repository.CompanyRepository.GetInstance();

            List<Model.Company> companies = companyRepo.GetCompanies();

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
            Repository.CompanyRepository companyRepo = Repository.CompanyRepository.GetInstance();
            Model.Company company = companyRepo.GetCompanyById(Id);

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
                Repository.CompanyRepository companyRepo = Repository.CompanyRepository.GetInstance();

                // Exception handling
                companyRepo.Create(company);

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
                Repository.CompanyRepository companyRepo = Repository.CompanyRepository.GetInstance();

                // Exception handling
                companyRepo.Update(company);

                return Ok();
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            Repository.CompanyRepository companyRepo = Repository.CompanyRepository.GetInstance();

            // Exception handling
            companyRepo.Delete(Id);

            return Ok();
        }
    }
}
