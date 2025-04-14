using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_Management.entity;

namespace Project_Management.dao
{
    public interface IProjectRepository
    {
        // Create operations
        bool CreateEmployee(Employee employee);
        bool CreateProject(Project project);
        bool CreateTask(ProjectTask task);

        // Assign operations
        bool AssignProjectToEmployee(int projectId, int employeeId);
        bool AssignTaskInProjectToEmployee(int taskId, int projectId, int employeeId);

        // Delete operations
        bool DeleteEmployee(int employeeId);
        bool DeleteProject(int projectId);
        bool DeleteTask(int taskId);

        // Update operations
        bool UpdateEmployee(Employee employee);   // Update employee details
        bool UpdateProject(Project project);       // Update project details
        bool UpdateTask(ProjectTask task);

        // View operations
        List<Project> GetAllProjects();
        List<Employee> GetAllEmployees();

        // Get tasks assigned to employee in a specific project
        List<ProjectTask> GetAllTasks();
        List<ProjectTask> GetParticularTasks(int empId, int projectId);


    }
}
