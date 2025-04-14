

using System;
using System.Collections.Generic;
using Project_Management.dao;
using Project_Management.entity;
using Project_Management.exception;

namespace Project_Management
{
    class Program
    {
        static void Main(string[] args)
        {
            IProjectRepository repo = new ProjectRepositoryImpl();

            Console.WriteLine("Welcome to Project Management System");
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n==============================");
                Console.WriteLine("1. Add Project");
                Console.WriteLine("2. Add Employee");
                Console.WriteLine("3. Add Task");
                Console.WriteLine("4. Assign Project to Employee");
                Console.WriteLine("5. Assign Task to Employee in Project");
                Console.WriteLine("6. View All Projects");
                Console.WriteLine("7. View All Employees");
                Console.WriteLine("8. View All Tasks");
                Console.WriteLine("9. View Tasks Assigned to Employee in a Project");
                Console.WriteLine("10. Delete Employee");
                Console.WriteLine("11. Delete Project");
                Console.WriteLine("12. Delete Task");
                Console.WriteLine("13. Update Project");
                Console.WriteLine("14. Update Employee");
                Console.WriteLine("15. Update Task");
                Console.WriteLine("0. Exit");
                Console.WriteLine("==============================");
                Console.Write(" Choose an option: ");
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.Write("Enter Project Name: ");
                        string pname = Console.ReadLine();
                        Console.Write("Enter Description: ");
                        string desc = Console.ReadLine();
                        Console.Write("Enter Status (started/dev/build/test/deployed): ");
                        string status = Console.ReadLine();

                        Project project = new Project
                        {
                            ProjectName = pname,
                            Description = desc,
                            StartDate = DateTime.Now,
                            Status = status
                        };

                        Console.WriteLine(repo.CreateProject(project) ? "Project added!" : " Failed to add project.");
                        break;

                    case 2:
                        Console.Write("Enter Employee Name: ");
                        string ename = Console.ReadLine();
                        Console.Write("Enter Designation: ");
                        string designation = Console.ReadLine();
                        Console.Write("Enter Gender (Male/Female/Other): ");
                        string gender = Console.ReadLine();
                        Console.Write("Enter Salary: ");
                        decimal salary = Convert.ToDecimal(Console.ReadLine());
                        Console.Write("Enter Project ID to assign (or 0 for none): ");
                        int pId = Convert.ToInt32(Console.ReadLine());

                        Employee emp = new Employee
                        {
                            EmployeeName = ename,
                            Designation = designation,
                            Gender = gender,
                            Salary = salary,
                            ProjectId = pId == 0 ? null : pId
                        };

                        Console.WriteLine(repo.CreateEmployee(emp) ? " Employee added!" : " Failed to add employee.");
                        break;

                    case 3:
                        Console.Write("Enter Task Name: ");
                        string tname = Console.ReadLine();
                        Console.Write("Enter Project ID: ");
                        int projId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Employee ID (or 0 for none): ");
                        int empId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Status (Assigned/Started/Completed): ");
                        string tstatus = Console.ReadLine();
                        Console.Write("Enter Allocation Date (yyyy-MM-dd): ");
                        DateTime allocation = Convert.ToDateTime(Console.ReadLine());

                        Console.Write("Enter Deadline Date (yyyy-MM-dd): ");
                        DateTime deadline = Convert.ToDateTime(Console.ReadLine());

                        ProjectTask task = new ProjectTask
                        {
                            TaskName = tname,
                            ProjectId = projId,
                            EmployeeId = empId == 0 ? null : empId,
                            Status = tstatus,
                            AllocationDate = allocation,
                            DeadlineDate = deadline
                        };

                        Console.WriteLine(repo.CreateTask(task) ? " Task added!" : " Failed to add task.");
                        break;

                    case 4:
                        Console.Write("Enter Project ID: ");
                        int projToAssign = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Employee ID: ");
                        int empToAssign = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine(repo.AssignProjectToEmployee(projToAssign, empToAssign)
                            ? "Project assigned!"
                            : "Failed to assign project.");
                        break;

                    case 5:
                        Console.Write("Enter Task ID: ");
                        int tId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Project ID: ");
                        int prId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Employee ID: ");
                        int eId = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine(repo.AssignTaskInProjectToEmployee(tId, prId, eId)
                            ? " Task assigned!"
                            : " Failed to assign task.");
                        break;

                    case 6:
                        try
                        {
                            var projects = repo.GetAllProjects();
                            Console.WriteLine("\n All Projects:");
                            foreach (var p in projects)
                            {
                                Console.WriteLine($"{p.ProjectId}: {p.ProjectName} - {p.Status}");
                            }
                        }
                        catch (ProjectNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case 7:
                        try
                        {
                            var employees = repo.GetAllEmployees();
                            Console.WriteLine("\n All Employees:");
                            foreach (var e in employees)
                            {
                                Console.WriteLine($"{e.EmployeeId}= {e.EmployeeName}= {e.Designation}=  Rs.{e.Salary}=");
                            }
                        }
                        catch (EmployeeNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case 8:
                        var tasks = repo.GetAllTasks();
                        Console.WriteLine("\nAll Tasks:");
                        foreach (var t in tasks)
                        {
                            Console.WriteLine($"Emp ID: {t.EmployeeId} {t.TaskId}  {t.TaskName}  {t.Status}");
                        }
                        break;


                    case 9:
                        Console.Write("Enter Employee ID: ");
                        int empFilterr = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Project ID: ");
                        int projFilterr = Convert.ToInt32(Console.ReadLine());

                        var empTaskss = repo.GetParticularTasks(empFilterr, projFilterr);
                        if (empTaskss.Count == 0)
                        {
                            Console.WriteLine("❌ No tasks found for the given employee in the selected project.");
                        }
                        else
                        {
                            Console.WriteLine($"\nTasks for Employee ID {empFilterr}  in Project  {projFilterr} :");
                            foreach (var t in empTaskss)
                            {
                                Console.WriteLine($"{t.TaskId}: {t.TaskName} - {t.Status}");
                            }
                        }
                        break;

                    case 10:
                        Console.Write("Enter Employee ID to delete: ");
                        int delEmpId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(repo.DeleteEmployee(delEmpId) ? "Employee deleted!" : "Failed to delete.");
                        break;

                    case 11:
                        Console.Write("Enter Project ID to delete: ");
                        int delProjId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(repo.DeleteProject(delProjId) ? "Project deleted!" : "Failed to delete.");
                        break;
                    case 12:
                        Console.Write("Enter Task ID to delete: ");
                        int delTaskId= Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(repo.DeleteProject(delTaskId) ? "Task deleted!" : "Failed to task delete.");
                        break;
                    case 13:
                        // Update Project
                        Console.Write("Enter Project ID to update: ");
                        int projIdToUpdate = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter new Project Name: ");
                        string updatedProjName = Console.ReadLine();
                        Console.Write("Enter new Description: ");
                        string updatedDesc = Console.ReadLine();
                        Console.Write("Enter new Status: ");
                        string updatedStatus = Console.ReadLine();

                        Project updatedProject = new Project
                        {
                            ProjectId = projIdToUpdate,
                            ProjectName = updatedProjName,
                            Description = updatedDesc,
                            Status = updatedStatus,
                            StartDate = DateTime.Now
                        };

                        Console.WriteLine(repo.UpdateProject(updatedProject) ? "Project updated!" : "Failed to update project.");
                        break;
                    case 14:
                        // Update Employee
                        Console.Write("Enter Employee ID to update: ");
                        int empIdToUpdate = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter new Gender (M/F): ");
                        string updatedGender = Console.ReadLine();

                        Console.Write("Enter new Project ID (or leave blank if none): ");
                        string projIdInput = Console.ReadLine();
                        int? updatedProjId = string.IsNullOrWhiteSpace(projIdInput) ? (int?)null : Convert.ToInt32(projIdInput);

                        Console.Write("Enter new Employee Name: ");
                        string updatedEmpName = Console.ReadLine();
                        Console.Write("Enter new Designation: ");
                        string updatedDesignation = Console.ReadLine();
                        Console.Write("Enter new Salary: ");
                        decimal updatedSalary = Convert.ToDecimal(Console.ReadLine());

                        Employee updatedEmployee = new Employee
                        {
                            EmployeeId = empIdToUpdate,
                            EmployeeName = updatedEmpName,
                            Designation = updatedDesignation,
                            Gender = updatedGender,
                            Salary = updatedSalary,
                            ProjectId = updatedProjId
                        };

                        Console.WriteLine(repo.UpdateEmployee(updatedEmployee) ? "Employee updated!" : "Failed to update employee.");
                        break;
                    case 15:
                        // Update Task
                        Console.Write("Enter Task ID to update: ");
                        int taskIdToUpdate = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Project ID: ");
                        int projIdUpdate = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Employee ID (or leave blank if none): ");
                        string empIdUpd = Console.ReadLine();
                        int? empIdUpdate = string.IsNullOrWhiteSpace(empIdUpd) ? (int?)null : Convert.ToInt32(empIdUpd);
                        Console.Write("Enter new Task Name: ");
                        string updatedTaskName = Console.ReadLine();
                        Console.Write("Enter new Status: ");
                        string updatedTaskStatus = Console.ReadLine();
                        Console.Write("Enter new Allocation Date (yyyy-MM-dd): ");
                        DateTime updatedAllocationDate = Convert.ToDateTime(Console.ReadLine());
                        Console.Write("Enter new Deadline Date (yyyy-MM-dd): ");
                        DateTime updatedDeadlineDate = Convert.ToDateTime(Console.ReadLine());

                        ProjectTask updatedTask = new ProjectTask
                        {
                            TaskId = taskIdToUpdate,
                            ProjectId = projIdUpdate,
                            EmployeeId = empIdUpdate,

                            TaskName = updatedTaskName,
                            Status = updatedTaskStatus,
                            AllocationDate = updatedAllocationDate,
                            DeadlineDate = updatedDeadlineDate
                        };

                        Console.WriteLine(repo.UpdateTask(updatedTask) ? "Task updated!" : "Failed to update task.");
                        break;
                    case 0:
                        running = false;
                        Console.WriteLine(" Exiting... ");
                        break;

                    default:
                        Console.WriteLine("Invalid option! Try again.");
                        break;
                }
            }
        }
    }
}

