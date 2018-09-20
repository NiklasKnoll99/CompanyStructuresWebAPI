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

        public readonly string Message;

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
            Message = msg;
        }

        public RepositoryException(Type type)
        {
            _type = (short)type;
        }

        public RepositoryException(string message) : base(message)
        {
            Message = message;
        }

        public short GetExType()
        {
            return _type;
        }
    }
}
