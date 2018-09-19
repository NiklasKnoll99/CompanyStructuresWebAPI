using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CompanyStructuresWebAPI.Interface;
using System.Data;
using System.Data.SqlClient;
using CompanyStructuresWebAPI.Model;
using Microsoft.Extensions.Options;

namespace CompanyStructuresWebAPI.Helper
{
    public class DbContext : IDbContext
    {
        private readonly ConnectionStrings _dbConnStrings;

        public DbContext(IOptions<ConnectionStrings> options)
        {
            _dbConnStrings = options.Value;
        }

        public IDbConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(_dbConnStrings.CompanyStructure); // Read it from appsettings
            
            try
            {
                conn.Open();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return conn;
        }
    }
}
