using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyStructuresWebAPI.Constant
{
    public static class ExceptionConstants
    {
        public const string ConnectionErrorMsg = "Unable to connect to database!";
        public const string ReadErrorMsg = "Unable to read data!";
        public const string ExecutionErrorMsg = "Unable to execute stored procedure!";
    }
}
