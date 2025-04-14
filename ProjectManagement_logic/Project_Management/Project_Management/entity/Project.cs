using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.entity
{
    public class Project
    {
        private int projectId;
        private string projectName;
        private string description;
        private DateTime startDate;
        private string status;
        public Project() { }

        public Project(int projectId, string projectName, string description, DateTime startDate, string status)
        {
            this.projectId = projectId;
            this.projectName = projectName;
            this.description = description;
            this.startDate = startDate;
            this.status = status;
        }

        public int ProjectId { get => projectId; set => projectId = value; }
        public string ProjectName { get => projectName; set => projectName = value; }
        public string Description { get => description; set => description = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public string Status { get => status; set => status = value; }
    }
}
