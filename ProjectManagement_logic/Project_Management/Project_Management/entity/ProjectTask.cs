using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.entity
{
    public class ProjectTask
    {
        private int taskId;
        private string taskName;
        private int projectId;
        private int? employeeId;
        private string status;
        private DateTime allocationDate;
        private DateTime deadlineDate;

        public ProjectTask() { }

        public ProjectTask(int taskId, string taskName, int projectId, int? employeeId, string status, DateTime allocationDate, DateTime deadlineDate)
        {
            this.taskId = taskId;
            this.taskName = taskName;
            this.projectId = projectId;
            this.employeeId = employeeId;
            this.status = status;
            this.allocationDate = allocationDate;
            this.deadlineDate = deadlineDate;
        }

        public int TaskId { get => taskId; set => taskId = value; }
        public string TaskName { get => taskName; set => taskName = value; }
        public int ProjectId { get => projectId; set => projectId = value; }
        public int? EmployeeId { get => employeeId; set => employeeId = value; }
        public string Status { get => status; set => status = value; }
        public DateTime AllocationDate { get => allocationDate; set => allocationDate = value; }
        public DateTime DeadlineDate { get => deadlineDate; set => deadlineDate = value; }
    }
}
