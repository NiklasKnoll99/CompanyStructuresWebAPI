using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CompanyStructuresWebAPI.Interface;
using System.Data;
using System.Data.SqlClient;

namespace CompanyStructuresWebAPI.Helper
{
    public class DbContext : IDbContext
    {
        public IDbConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection("Server=TAPPQA;Database=Training-NK-Company;Trusted_Connection=True;"); // Read it from appsettings
            
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
