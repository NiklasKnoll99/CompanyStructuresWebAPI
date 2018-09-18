using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CompanyStructuresWebAPI.APIException
{
    public class RepositoryException : Exception
    {
        short _type;
        string _msg;

        public enum Type
        {
            CONNECTION_EXCEPTION,
            SPROCEDURE_EXECUTION_EXCEPTION,
            READ_EXCEPTION,
            UNKNOWN
        }

        public RepositoryException(Type type, string msg)
        {
            _type = (short)type;
            _msg = msg;
        }

        public RepositoryException(Type type)
        {
            _type = (short)type;
        }

        public RepositoryException(string message) : base(message)
        {
            _msg = message;
        }

        public short GetExType()
        {
            return _type;
        }
    }
}
