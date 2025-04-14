using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Project_Management.entity
{
    public class Employee
    {
        private int employeeId;
        private string employeeName;
        private string designation;
        private string gender;
        private decimal salary;
        private int? projectId;
        public Employee() { }

        public Employee(int employeeId, string employeeName, string designation, string gender, decimal salary, int? projectId)
        {
            this.employeeId = employeeId;
            this.employeeName = employeeName;
            this.designation = designation;
            this.gender = gender;
            this.salary = salary;
            this.projectId = projectId;
        }

        public int EmployeeId { get => employeeId; set => employeeId = value; }
        public string EmployeeName { get => employeeName; set => employeeName = value; }
        public string Designation { get => designation; set => designation = value; }
        public string Gender { get => gender; set => gender = value; }
        public decimal Salary { get => salary; set => salary = value; }
        public int? ProjectId { get => projectId; set => projectId = value; }
    }
}
