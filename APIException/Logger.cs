using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyStructuresWebAPI.APIException
{
    public class Logger
    {
        ILogger _logger;

        public Logger(ILogger logger)
        {
            _logger = logger;
        }

        public void Log(string msg)
        {
            Console.WriteLine(msg);
            _logger.LogError(msg);
        }
    }
}
