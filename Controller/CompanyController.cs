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
            Repository.CompanyRepository companyRepo = new Repository.CompanyRepository();

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
            Repository.CompanyRepository companyRepo = new Repository.CompanyRepository();
            Model.Company company = companyRepo.GetCompanyById(Id);

            if (company != null)
                return Ok(company);

            else
                return NoContent();
        }
    }
}
