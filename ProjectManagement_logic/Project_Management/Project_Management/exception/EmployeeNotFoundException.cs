using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.exception
{
    //public class EmployeeNotFoundException : Exception
    //{
    //    public EmployeeNotFoundException()
    //        : base("Employee not found in the database.")
    //    {
    //    }

    //    public EmployeeNotFoundException(string message)
    //        : base(message)
    //    {
    //    }
    //}
    public class EmployeeNotFoundException : Exception
    {
        public EmployeeNotFoundException() { }

        public EmployeeNotFoundException(string message) : base(message) { }

        public EmployeeNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
