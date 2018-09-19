using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using Dapper;
using CompanyStructuresWebAPI.Interface;
using CompanyStructuresWebAPI.APIException;
using CompanyStructuresWebAPI.Constant;

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
            IDbConnection conn = null;

            try
            {
                conn = _dbContext.GetConnection();
            }

            catch (Exception)
            {
                throw new RepositoryException(RepositoryException.Type.CONNECTION_EXCEPTION, ExceptionConstants.ConnectionErrorMsg);
            }

            int retVal = 0;

            DynamicParameters dParams = new DynamicParameters();
            dParams.Add("@Id", company.Id);
            dParams.Add("@CompanyName", company.CompanyName);
            dParams.Add("@RetVal", retVal, DbType.Int32, ParameterDirection.ReturnValue);

            try
            {
                conn.Execute("spCreateOrUpdateCompany", dParams, null, null, CommandType.StoredProcedure);
            }

            catch (Exception)
            {
                throw new RepositoryException(RepositoryException.Type.SPROCEDURE_EXECUTION_EXCEPTION, ExceptionConstants.ExecutionErrorMsg);
            }

            retVal = dParams.Get<int>("@RetVal");

            return retVal;
        }

        public List<Model.Company> GetCompanies()
        {
            IDbConnection conn = null;

            try
            {
                conn = _dbContext.GetConnection();
            }

            catch (Exception)
            {
                throw new RepositoryException(RepositoryException.Type.CONNECTION_EXCEPTION, ExceptionConstants.ConnectionErrorMsg);
            }

            string cmd = "SELECT Id, CompanyName, CountryCode, ProvinceName, PostCode, CityName, Street, HouseNumber FROM viCompany";
            List<Model.Company> companies = null;

            try
            {
                companies = conn.Query<Model.Company>(cmd).ToList();
            }

            catch (Exception)
            {
                throw new RepositoryException(RepositoryException.Type.READ_EXCEPTION, ExceptionConstants.ReadErrorMsg);
            }

            return companies;
        }

        public Model.Company GetCompanyById(int Id)
        {
            IDbConnection conn = null;

            try
            {
                conn = _dbContext.GetConnection();
            }

            catch (Exception)
            {
                throw new RepositoryException(RepositoryException.Type.CONNECTION_EXCEPTION, ExceptionConstants.ConnectionErrorMsg);
            }

            string cmd = "SELECT Id, CompanyName, CountryCode, ProvinceName, PostCode, CityName, Street, HouseNumber FROM viCompany WHERE Id = " + Id;
            Model.Company company = null;

            try
            {
                company = conn.QueryFirstOrDefault<Model.Company>(cmd);
            }

            catch (Exception)
            {
                throw new RepositoryException(RepositoryException.Type.READ_EXCEPTION, ExceptionConstants.ReadErrorMsg);
            }

            return company;
        }

        public int Create(Model.Dto.CompanyDto company)
        {
            try
            {
                return AddOrUpdateCompany(new Model.Company()
                {
                    Id = -1,
                    CompanyName = company.CompanyName
                });
            }

            catch (RepositoryException rEx)
            {
                throw rEx;
            }
        }

        public int Update(Model.Company company)
        {
            try
            {
                return AddOrUpdateCompany(company);
            }

            catch (RepositoryException rEx)
            {
                throw rEx;
            }
        }

        public int Delete(int Id)
        {
            IDbConnection conn = null;

            try
            {
                conn = _dbContext.GetConnection();
            }

            catch (Exception)
            {
                throw new RepositoryException(RepositoryException.Type.CONNECTION_EXCEPTION, ExceptionConstants.ConnectionErrorMsg);
            }

            int retVal = 0;

            DynamicParameters dParams = new DynamicParameters();
            dParams.Add("@Id", Id);
            dParams.Add("@RetVal", null, DbType.Int32, ParameterDirection.ReturnValue, null);

            try
            {
                conn.Execute("spDeleteCompany", dParams, null, null, CommandType.StoredProcedure);
            }

            catch (Exception)
            {
                throw new RepositoryException(RepositoryException.Type.SPROCEDURE_EXECUTION_EXCEPTION, ExceptionConstants.ExecutionErrorMsg);
            }

            retVal = dParams.Get<int>("@RetVal");

            return retVal;
        }
    }
}
