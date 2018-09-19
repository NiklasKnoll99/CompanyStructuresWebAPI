using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using CompanyStructuresWebAPI.Interface;
using CompanyStructuresWebAPI.APIException;
using CompanyStructuresWebAPI.Constant;

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
            dParams.Add("@Id", department.Id);
            dParams.Add("@DepartmentName", department.DepartmentName);
            dParams.Add("@RetVal", retVal, DbType.Int32, ParameterDirection.ReturnValue);

            try
            {
                conn.Execute("spCreateOrUpdateDepartment", dParams, null, null, CommandType.StoredProcedure);
            }

            catch (Exception)
            {
                throw new RepositoryException(RepositoryException.Type.SPROCEDURE_EXECUTION_EXCEPTION, ExceptionConstants.ExecutionErrorMsg);
            }

            retVal = dParams.Get<int>("@RetVal");

            return retVal;
        }

        public List<Model.Department> GetDepartments()
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

            string cmd = "SELECT Id, DepartmentName, CompanyName FROM viDepartment";
            List<Model.Department> departments = null;

            try
            {
                departments = conn.Query<Model.Department>(cmd).ToList();
            }

            catch (Exception)
            {
                throw new RepositoryException(RepositoryException.Type.READ_EXCEPTION, ExceptionConstants.ReadErrorMsg);
            }

            return departments;
        }

        public Model.Department GetDepartmentById(int Id)
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

            string cmd = "SELECT Id, DepartmentName, CompanyName FROM viDepartment WHERE Id = " + Id.ToString();
            Model.Department department = null;

            try
            {
                department = conn.QueryFirstOrDefault<Model.Department>(cmd);
            }

            catch (Exception)
            {
                throw new RepositoryException(RepositoryException.Type.READ_EXCEPTION, ExceptionConstants.ReadErrorMsg);
            }

            return department;
        }

        public int Create(Model.Dto.DepartmentDto department)
        {
            try
            {
                return AddOrUpdateDepartment(new Model.Department()
                {
                    Id = -1,
                    DepartmentName = department.DepartmentName
                });
            }

            catch (RepositoryException rEx)
            {
                throw rEx;
            }
        }

        public int Update(Model.Department department)
        {
            try
            {
                return AddOrUpdateDepartment(department);
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
            dParams.Add("@RetVal", retVal, DbType.Int32, ParameterDirection.ReturnValue);

            try
            {
                conn.Execute("spDeleteDepartment", dParams, null, null, CommandType.StoredProcedure);
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
