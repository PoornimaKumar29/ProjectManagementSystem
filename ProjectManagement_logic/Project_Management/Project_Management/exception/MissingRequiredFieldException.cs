using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.exception
{
    public class MissingRequiredFieldException : Exception
    {
        public MissingRequiredFieldException() { }

        public MissingRequiredFieldException(string message) : base(message) { }

        public MissingRequiredFieldException(string message, Exception inner) : base(message, inner) { }
    }

}
