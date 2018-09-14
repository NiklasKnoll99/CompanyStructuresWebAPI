using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;

using Microsoft.AspNetCore.Mvc;
using Dapper;

namespace CompanyStructuresWebAPI.Repository
{
    public class DepartmentRepository : Microsoft.AspNetCore.Mvc.Controller
    {
        static DepartmentRepository repo;

        public static DepartmentRepository GetInstance()
        {
            if (repo == null)
                repo = new DepartmentRepository();

            return repo;
        }

        DepartmentRepository()
        {
        }

        int AddOrUpdateDepartment(Model.Department department)
        {
            SqlConnection conn = new SqlConnection("Server=TAPPQA;Database=Training-NK-Company;Trusted_Connection=True;");
            conn.Open();

            SqlParameter param = new SqlParameter("@RetVal", SqlDbType.Int);
            param.Direction = ParameterDirection.ReturnValue;

            SqlCommand cmd = new SqlCommand("spCreateOrUpdateDepartment", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", department.Id);
            cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
            cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();

            conn.Close();

            return (int)cmd.Parameters["@RetVal"].Value;
        }

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

                List<Model.Department> departments = null;
                departments = conn.Query<Model.Department>(cmd.CommandText).ToList();

                return departments;
            }

            else
                return null;
        }

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

                DynamicParameters dParams = new DynamicParameters();
                dParams.Add("@Id", Id);

                return conn.QueryFirstOrDefault<Model.Department>(cmd.CommandText, dParams);
            }

            else
                return null;
        }

        public int Create(Model.Dto.DepartmentDto department)
        {
            return AddOrUpdateDepartment(new Model.Department()
            {
                Id = -1,
                DepartmentName = department.DepartmentName
            });
        }

        public int Update(Model.Department department)
        {
            return AddOrUpdateDepartment(department);
        }

        public int Delete(int Id)
        {
            SqlConnection conn = new SqlConnection("Server=TAPPQA;Database=Training-NK-Company;Trusted_Connection=True;");
            conn.Open();

            SqlParameter param = new SqlParameter("@RetVal", SqlDbType.Int);
            param.Direction = ParameterDirection.ReturnValue;

            SqlCommand cmd = new SqlCommand("spDeleteDepartment", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();

            conn.Close();

            return (int)cmd.Parameters["@RetVal"].Value;
        }
    }
}
