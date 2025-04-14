using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.exception
{
    //public class ProjectNotFoundException : Exception
    //{
    //    public ProjectNotFoundException()
    //        : base("Project not found in the database.")
    //    {
    //    }

    //    public ProjectNotFoundException(string message)
    //        : base(message)
    //    {
    //    }
    //}
    public class ProjectNotFoundException : Exception
    {
        public ProjectNotFoundException() { }

        public ProjectNotFoundException(string message) : base(message) { }

        public ProjectNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
