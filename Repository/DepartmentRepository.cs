using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;

using Microsoft.AspNetCore.Mvc;

namespace CompanyStructuresWebAPI.Repository
{
    [Route("departments")]
    public class DepartmentRepository : Microsoft.AspNetCore.Mvc.Controller
    {
        public List<Model.Department> GetDepartments()
        {
            SqlConnection conn = new SqlConnection("Server=TAPPQA;Database=Training-NK-Company;Trusted_Connection=True;");
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT Id, DepartmentName, CompanyName FROM viDepartment", conn);
            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            if (adapter.Fill(table) > 0)
            {
                conn.Close();

                List<Model.Department> departments = new List<Model.Department>();

                for (short i = 0; i < table.Rows.Count; i++)
                {
                    departments.Add(new Model.Department()
                    {
                        Id = (int)table.Rows[i][0],
                        DepartmentName = (string)table.Rows[i][1],
                        CompanyName = (string)table.Rows[i][2]
                    });
                }

                return departments;
            }

            else
                return null;
        }

        [HttpGet("{Id}")]
        public Model.Department GetDepartmentById(int Id)
        {
            SqlConnection conn = new SqlConnection("Server=TAPPQA;Database=Training-NK-Company;Trusted_Connection=True;");
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT Id, DepartmentName, CompanyName FROM viDepartment WHERE Id = " + Id.ToString(), conn);
            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            if (adapter.Fill(table) > 0)
            {
                conn.Close();

                Model.Department department = new Model.Department()
                {
                    Id = (int)table.Rows[0][0],
                    DepartmentName = (string)table.Rows[0][1],
                    CompanyName = (string)table.Rows[0][2]
                };

                return department;
            }

            else
                return null;
        }
    }
}
