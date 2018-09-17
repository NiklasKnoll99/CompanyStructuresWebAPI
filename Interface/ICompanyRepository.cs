using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CompanyStructuresWebAPI.Model;
using CompanyStructuresWebAPI.Model.Dto;

namespace CompanyStructuresWebAPI.Interface
{
    public interface ICompanyRepository
    {
        List<Company> GetCompanies();

        Company GetCompanyById(int Id);

        int Delete(int Id);

        int Create(CompanyDto company);

        int Update(Company company);
    }
}
