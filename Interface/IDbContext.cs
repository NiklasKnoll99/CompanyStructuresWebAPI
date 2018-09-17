using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;

namespace CompanyStructuresWebAPI.Interface
{
    public interface IDbContext
    {
        IDbConnection GetConnection();
    }
}
