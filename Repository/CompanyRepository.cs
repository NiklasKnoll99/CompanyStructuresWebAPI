using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;

using Dapper;

namespace CompanyStructuresWebAPI.Repository
{
    public class CompanyRepository
    {
        static CompanyRepository repo;

        public static CompanyRepository GetInstance()
        {
            if (repo == null)
                repo = new CompanyRepository();

            return repo;
        }

        CompanyRepository()
        {
        }

        int AddOrUpdateCompany(Model.Company company)
        {
            SqlConnection conn = new SqlConnection("Server=TAPPQA;Database=Training-NK-Company;Trusted_Connection=True;");
            conn.Open();

            SqlParameter param = new SqlParameter("@RetVal", SqlDbType.Int);
            param.Direction = ParameterDirection.ReturnValue;

            SqlCommand cmd = new SqlCommand("spCreateOrUpdateCompany", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", company.Id);
            cmd.Parameters.AddWithValue("@CompanyName", company.CompanyName);
            cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();

            conn.Close();

            return (int)cmd.Parameters["@RetVal"].Value;
        }

        public List<Model.Company> GetCompanies()
        {
            SqlConnection conn = new SqlConnection("Server=TAPPQA;Database=Training-NK-Company;Trusted_Connection=True;");
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT Id, CompanyName, CountryCode, ProvinceName, PostCode, CityName, Street, HouseNumber FROM viCompany", conn);
            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            if (adapter.Fill(table) > 0)
            {
                conn.Close();

                List<Model.Company> companies = null;
                companies = conn.Query<Model.Company>(cmd.CommandText).ToList();

                return companies;
            }

            else
                return null;
        }

        public Model.Company GetCompanyById(int Id)
        {
            SqlConnection conn = new SqlConnection("Server=TAPPQA;Database=Training-NK-Company;Trusted_Connection=True;");
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT Id, CompanyName, CountryCode, ProvinceName, PostCode, CityName, Street, HouseNumber FROM viCompany WHERE Id = " + Id.ToString(), conn);
            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            if (adapter.Fill(table) > 0)
            {
                conn.Close();

                DynamicParameters dParams = new DynamicParameters();
                dParams.Add("@Id", Id);

                return conn.QueryFirstOrDefault<Model.Company>(cmd.CommandText, dParams);
            }

            else
                return null;
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
            SqlConnection conn = new SqlConnection("Server=TAPPQA;Database=Training-NK-Company;Trusted_Connection=True;");
            conn.Open();

            SqlParameter param = new SqlParameter("@RetVal", SqlDbType.Int);
            param.Direction = ParameterDirection.ReturnValue;

            SqlCommand cmd = new SqlCommand("spDeleteCompany", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();

            conn.Close();

            return (int)cmd.Parameters["@RetVal"].Value;
        }
    }
}
