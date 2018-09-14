using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;

namespace CompanyStructuresWebAPI.Repository
{
    public class CompanyRepository
    {
        void AddOrUpdateCompany(Model.Company company)
        {
            SqlConnection conn = new SqlConnection("Server=TAPPQA;Database=Training-NK-Company;Trusted_Connection=True;");
            conn.Open();

            SqlCommand cmd = new SqlCommand("spCreateOrUpdateCompany", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", company.Id);
            cmd.Parameters.AddWithValue("@CompanyName", company.CompanyName);
            cmd.ExecuteNonQuery();
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

                List<Model.Company> companies = new List<Model.Company>();

                for (short i = 0; i < table.Rows.Count; i++)
                {
                    companies.Add(new Model.Company()
                    {
                        Id = (int)table.Rows[i][0],
                        CompanyName = (string)table.Rows[i][1],
                        CountryCode = (string)table.Rows[i][2],
                        ProvinceName = (string)table.Rows[i][3],
                        PostCode = (string)table.Rows[i][4],
                        CityName = (string)table.Rows[i][5],
                        Street = (string)table.Rows[i][6],
                        HouseNumber = (short)table.Rows[i][7]
                    });
                }

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

                Model.Company company = new Model.Company()
                {
                    Id = (int)table.Rows[0][0],
                    CompanyName = (string)table.Rows[0][1],
                    CountryCode = (string)table.Rows[0][2],
                    ProvinceName = (string)table.Rows[0][3],
                    PostCode = (string)table.Rows[0][4],
                    CityName = (string)table.Rows[0][5],
                    Street = (string)table.Rows[0][6],
                    HouseNumber = (short)table.Rows[0][7]
                };

                return company;
            }

            else
                return null;
        }

        public void Create(Model.Dto.CompanyDto company)
        {
            AddOrUpdateCompany(new Model.Company()
            {
                Id = -1,
                CompanyName = company.CompanyName
            });
        }

        public void Update(Model.Company company)
        {
            AddOrUpdateCompany(company);
        }
    }
}
