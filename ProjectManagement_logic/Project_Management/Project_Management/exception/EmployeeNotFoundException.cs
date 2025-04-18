using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.exception
{
    public class EmployeeNotFoundException : Exception
    {
        // Default constructor with a predefined error message
        public EmployeeNotFoundException()
            : base("Employee not found in the system.")
        {
        }

        // Constructor that allows you to pass a custom message
        public EmployeeNotFoundException(string message)
            : base(message)
        {
        }

        // Constructor that allows for both a custom message and an inner exception
        public EmployeeNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
