using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using Dapper;
using CompanyStructuresWebAPI.Interface;

namespace CompanyStructuresWebAPI.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        IDbContext _dbContext;

        public CompanyRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        int AddOrUpdateCompany(Model.Company company)
        {
            var conn = _dbContext.GetConnection();
            int retVal = 0;

            DynamicParameters dParams = new DynamicParameters();
            dParams.Add("@Id", company.Id);
            dParams.Add("@CompanyName", company.CompanyName);
            dParams.Add("@RetVal", retVal, DbType.Int32, ParameterDirection.ReturnValue);

            conn.Execute("spCreateOrUpdateCompany", dParams, null, null, CommandType.StoredProcedure);

            retVal = dParams.Get<int>("@RetVal");

            return retVal;
        }

        public List<Model.Company> GetCompanies()
        {
            var conn = _dbContext.GetConnection();
            string cmd = "SELECT Id, CompanyName, CountryCode, ProvinceName, PostCode, CityName, Street, HouseNumber FROM viCompany";
            List<Model.Company> companies = conn.Query<Model.Company>(cmd).ToList();

            return companies;
        }

        public Model.Company GetCompanyById(int Id)
        {
            var conn = _dbContext.GetConnection();
            string cmd = "SELECT Id, CompanyName, CountryCode, ProvinceName, PostCode, CityName, Street, HouseNumber FROM viCompany WHERE Id = " + Id;
            Model.Company company = conn.QueryFirstOrDefault<Model.Company>(cmd);

            return company;
        }

        public int Create(Model.Dto.CompanyDto company)
        {
            return AddOrUpdateCompany(new Model.Company()
            {
                Id = -1,
                CompanyName = company.CompanyName
            });
        }

        public int Update(Model.Company company)
        {
            return AddOrUpdateCompany(company);
        }

        public int Delete(int Id)
        {
            var conn = _dbContext.GetConnection();
            int retVal = 0;

            DynamicParameters dParams = new DynamicParameters();
            dParams.Add("@Id", Id);
            dParams.Add("@RetVal", null, DbType.Int32, ParameterDirection.ReturnValue, null);

            conn.Execute("spDeleteCompany", dParams, null, null, CommandType.StoredProcedure);

            retVal = dParams.Get<int>("@RetVal");

            return retVal;
        }
    }
}
