using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.exception
{
    public class InvalidEntryException : Exception
    {
        public InvalidEntryException() { }

        public InvalidEntryException(string message) : base(message) { }

        public InvalidEntryException(string message, Exception inner) : base(message, inner) { }
    }
}
