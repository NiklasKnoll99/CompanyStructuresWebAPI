using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CompanyStructuresWebAPI.Model;

namespace CompanyStructuresWebAPI.Interface
{
    public interface ICompanyRepository
    {
        List<Company> GetCompanies();

        Company GetCompanyById(int Id);
    }
}
