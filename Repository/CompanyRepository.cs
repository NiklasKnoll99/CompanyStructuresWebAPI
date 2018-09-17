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

        //int AddOrUpdateCompany(Model.Company company)
        //{
        //    SqlConnection conn = new SqlConnection("Server=TAPPQA;Database=Training-NK-Company;Trusted_Connection=True;");
            
        //    try
        //    {
        //        conn.Open();
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    SqlParameter param = new SqlParameter("@RetVal", SqlDbType.Int);
        //    param.Direction = ParameterDirection.ReturnValue;

        //    SqlCommand cmd = new SqlCommand("spCreateOrUpdateCompany", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@Id", company.Id);
        //    cmd.Parameters.AddWithValue("@CompanyName", company.CompanyName);
        //    cmd.Parameters.Add(param);

        //    try
        //    {
        //        cmd.ExecuteNonQuery();
        //        conn.Close();
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return (int)cmd.Parameters["@RetVal"].Value;
        //}

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

        //public int Create(Model.Dto.CompanyDto company)
        //{
        //    try
        //    {
        //        return AddOrUpdateCompany(new Model.Company()
        //        {
        //            Id = -1,
        //            CompanyName = company.CompanyName
        //        });
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public int Update(Model.Company company)
        //{
        //    try
        //    {
        //        return AddOrUpdateCompany(company);
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public int Delete(int Id)
        //{
        //    SqlConnection conn = new SqlConnection("Server=TAPPQA;Database=Training-NK-Company;Trusted_Connection=True;");

        //    try
        //    {
        //        conn.Open();
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    SqlParameter param = new SqlParameter("@RetVal", SqlDbType.Int);
        //    param.Direction = ParameterDirection.ReturnValue;

        //    SqlCommand cmd = new SqlCommand("spDeleteCompany", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@Id", Id);
        //    cmd.Parameters.Add(param);

        //    try
        //    {
        //        cmd.ExecuteNonQuery();
        //        conn.Close();
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return (int)cmd.Parameters["@RetVal"].Value;
        //}
    }
}
