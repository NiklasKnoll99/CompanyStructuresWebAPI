using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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

        bool _Check(string Username, string Password)
        {
            if ((Username == "KnarfRetlawReiemniets") && (Password == "MyPw"))
                return true;

            else
                return false;
        }

        string[] _GetHeaderData(HttpRequest req)
        {
            string authorizationKey = Request.Headers["Authorization"].ToString();

            if (authorizationKey != "")
            {
                authorizationKey = authorizationKey.Remove(0, 6);

                string decodedKey = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authorizationKey));

                string[] authData = decodedKey.Split(":");

                return authData;
            }

            else
                return null;
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
            string[] authData = _GetHeaderData(Request);

            if ((authData != null) && (_Check(authData[0], authData[1])))
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
