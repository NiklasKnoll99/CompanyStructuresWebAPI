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

        int AddOrUpdateDepartment(Model.Department department)
        {
            var conn = _dbContext.GetConnection();
            int retVal = 0;

            DynamicParameters dParams = new DynamicParameters();
            dParams.Add("@Id", department.Id);
            dParams.Add("@DepartmentName", department.DepartmentName);
            dParams.Add("@RetVal", retVal, DbType.Int32, ParameterDirection.ReturnValue);

            conn.Execute("spCreateOrUpdateDepartment", dParams, null, null, CommandType.StoredProcedure);

            retVal = dParams.Get<int>("@RetVal");

            return retVal;
        }

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
            var conn = _dbContext.GetConnection();
            int retVal = 0;

            DynamicParameters dParams = new DynamicParameters();
            dParams.Add("@Id", Id);
            dParams.Add("@RetVal", retVal, DbType.Int32, ParameterDirection.ReturnValue);

            conn.Execute("spDeleteDepartment", dParams, null, null, CommandType.StoredProcedure);

            retVal = dParams.Get<int>("@RetVal");

            return retVal;
        }
    }
}
