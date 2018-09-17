using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using CompanyStructuresWebAPI.Interface;

namespace CompanyStructuresWebAPI.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        IDbContext _dbContext;

        public DepartmentRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //int AddOrUpdateDepartment(Model.Department department)
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

        //    SqlCommand cmd = new SqlCommand("spCreateOrUpdateDepartment", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@Id", department.Id);
        //    cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
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

        public List<Model.Department> GetDepartments()
        {
            var conn = _dbContext.GetConnection();
            string cmd = "SELECT Id, DepartmentName, CompanyName FROM viDepartment";
            List<Model.Department> departments = conn.Query<Model.Department>(cmd).ToList();

            return departments;
        }

        public Model.Department GetDepartmentById(int Id)
        {
            var conn = _dbContext.GetConnection();
            string cmd = "SELECT Id, DepartmentName, CompanyName FROM viDepartment WHERE Id = " + Id.ToString();
            Model.Department department = conn.QueryFirstOrDefault<Model.Department>(cmd);

            return department;
        }

        //public int Create(Model.Dto.DepartmentDto department)
        //{
        //    try
        //    {
        //        return AddOrUpdateDepartment(new Model.Department()
        //        {
        //            Id = -1,
        //            DepartmentName = department.DepartmentName
        //        });
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public int Update(Model.Department department)
        //{
        //    try
        //    {
        //        return AddOrUpdateDepartment(department);
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

        //    SqlCommand cmd = new SqlCommand("spDeleteDepartment", conn);
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
