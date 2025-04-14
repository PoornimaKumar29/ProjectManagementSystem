using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.exception
{
    public class TaskNotFoundException : Exception
    {
        public TaskNotFoundException() { }

        public TaskNotFoundException(string message) : base(message) { }

        public TaskNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
