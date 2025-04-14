using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.exception
{
    public class InvalidSqlQueryException : Exception
    {
        public InvalidSqlQueryException() { }

        public InvalidSqlQueryException(string message) : base(message) { }

        public InvalidSqlQueryException(string message, Exception inner) : base(message, inner) { }
    }
}
