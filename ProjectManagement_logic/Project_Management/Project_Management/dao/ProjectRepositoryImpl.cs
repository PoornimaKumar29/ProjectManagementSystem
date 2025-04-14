using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Project_Management.entity;
using Project_Management.util;
using Project_Management.exception;

namespace Project_Management.dao
{
    public class ProjectRepositoryImpl : IProjectRepository

    {
        public bool CreateProject(Project project)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    if (conn == null)
                    {
                        throw new DatabaseConnectionException("Database connection failed.");
                    }

                    conn.Open();
                    string query = "INSERT INTO Projects (Project_Name, Description, Start_Date, Status) " +
                                   "VALUES (@name, @description, @startdate, @status)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", project.ProjectName);
                    cmd.Parameters.AddWithValue("@description", project.Description);
                    cmd.Parameters.AddWithValue("@startdate", project.StartDate);
                    cmd.Parameters.AddWithValue("@status", project.Status);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while connecting to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidEntryException("Error occurred while creating the project.", ex);
            }
        }

        public bool CreateEmployee(Employee emp)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(emp.EmployeeName) ||
            string.IsNullOrWhiteSpace(emp.Designation) ||
            string.IsNullOrWhiteSpace(emp.Gender) ||
            emp.Salary <= 0)
                {
                    throw new InvalidEntryException("One or more employee details are missing or invalid.");
                }

                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    if (conn == null)
                    {
                        throw new DatabaseConnectionException("Database connection failed.");
                    }

                    conn.Open();
                    string query = "INSERT INTO Employee (Employee_Name, Designation, Gender, Salary, Project_Id) " +
                                   "VALUES (@name, @designation, @gender, @salary, @projId)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", emp.EmployeeName);
                    cmd.Parameters.AddWithValue("@designation", emp.Designation);
                    cmd.Parameters.AddWithValue("@gender", emp.Gender);
                    cmd.Parameters.AddWithValue("@salary", emp.Salary);
                    cmd.Parameters.AddWithValue("@projId", emp.ProjectId ?? (object)DBNull.Value);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while connecting to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidEntryException("Error occurred while creating the employee.", ex);
            }
        }

        public bool CreateTask(ProjectTask task)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    if (conn == null)
                    {
                        throw new DatabaseConnectionException("Database connection failed.");
                    }

                    conn.Open();
                    string query = "INSERT INTO Tasks (Task_Name, Project_Id, Employee_Id, Status, Allocation_Date, Deadline_Date) " +
                                   "VALUES (@taskName, @projId, @empId, @status, @allocationDate, @deadlineDate)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@taskName", task.TaskName);
                    cmd.Parameters.AddWithValue("@projId", task.ProjectId);
                    cmd.Parameters.AddWithValue("@empId", task.EmployeeId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@status", task.Status);
                    cmd.Parameters.AddWithValue("@allocationDate", task.AllocationDate);
                    cmd.Parameters.AddWithValue("@deadlineDate", task.DeadlineDate);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while connecting to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidEntryException("Error occurred while creating the task.", ex);
            }
        }

        public List<Project> GetAllProjects()
        {
            try
            {
                List<Project> projects = new List<Project>();

                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    if (conn == null)
                    {
                        throw new DatabaseConnectionException("Database connection failed.");
                    }

                    conn.Open();
                    string query = "SELECT * FROM Projects";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        projects.Add(new Project
                        {
                            ProjectId = Convert.ToInt32(reader["Project_Id"]),
                            ProjectName = reader["Project_Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            StartDate = Convert.ToDateTime(reader["Start_Date"]),
                            Status = reader["Status"].ToString()
                        });
                    }
                }

                if (projects.Count == 0)
                {
                    throw new ProjectNotFoundException("No projects found in the database.");
                }

                return projects;
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while connecting to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidEntryException("Error occurred while fetching the projects.", ex);
            }
        }

        public List<Employee> GetAllEmployees()
        {
            try
            {
                List<Employee> employees = new List<Employee>();

                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    if (conn == null)
                    {
                        throw new DatabaseConnectionException("Database connection failed.");
                    }

                    conn.Open();
                    string query = "SELECT * FROM Employee";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            EmployeeId = Convert.ToInt32(reader["Employee_Id"]),
                            EmployeeName = reader["Employee_Name"].ToString(),
                            Designation = reader["Designation"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            Salary = Convert.ToDecimal(reader["Salary"]),
                            ProjectId = reader["Project_Id"] != DBNull.Value ? Convert.ToInt32(reader["Project_Id"]) : (int?)null
                        });
                    }
                }

                if (employees.Count == 0)
                {
                    throw new EmployeeNotFoundException("No employees found in the database.");
                }

                return employees;
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while connecting to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidEntryException("Error occurred while fetching the employees.", ex);
            }
        }

        public List<ProjectTask> GetAllTasks()
        {
            try
            {
                List<ProjectTask> tasks = new List<ProjectTask>();

                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    if (conn == null)
                    {
                        throw new DatabaseConnectionException("Database connection failed.");
                    }

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Tasks", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        tasks.Add(new ProjectTask
                        {
                            TaskId = Convert.ToInt32(reader["Task_ID"]),
                            TaskName = reader["Task_Name"].ToString(),
                            ProjectId = Convert.ToInt32(reader["Project_Id"]),
                            EmployeeId = reader["Employee_Id"] != DBNull.Value ? Convert.ToInt32(reader["Employee_Id"]) : (int?)null,
                            Status = reader["Status"].ToString(),
                            AllocationDate = Convert.ToDateTime(reader["Allocation_Date"]),
                            DeadlineDate = Convert.ToDateTime(reader["Deadline_Date"])
                        });
                    }
                }

                return tasks;
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while connecting to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidEntryException("Error occurred while fetching the tasks.", ex);
            }
        }

        public bool AssignProjectToEmployee(int employeeId, int projectId)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    if (conn == null)
                    {
                        throw new DatabaseConnectionException("Database connection failed.");
                    }

                    conn.Open();
                    string query = "UPDATE Employee SET Project_Id = @projId WHERE Employee_Id = @empId";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@projId", projectId);
                    cmd.Parameters.AddWithValue("@empId", employeeId);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while connecting to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidEntryException("Error occurred while assigning the project to the employee.", ex);
            }
        }

        public bool AssignTaskInProjectToEmployee(int taskId, int projectId,int employeeId)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    if (conn == null)
                    {
                        throw new DatabaseConnectionException("Database connection failed.");
                    }

                    conn.Open();
                    string query = "UPDATE Tasks SET Employee_Id = @empId WHERE Task_ID = @taskId";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@empId", employeeId);
                    cmd.Parameters.AddWithValue("@taskId", taskId);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while connecting to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidEntryException("Error occurred while assigning the task to the employee.", ex);
            }
        }

        public bool DeleteEmployee(int employeeId)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    if (conn == null)
                    {
                        throw new DatabaseConnectionException("Database connection failed.");
                    }

                    conn.Open();
                    string query = "DELETE FROM Employee WHERE Employee_Id = @empId";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@empId", employeeId);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while connecting to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidEntryException("Error occurred while deleting the employee.", ex);
            }
        }

        public bool DeleteProject(int projectId)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM Projects WHERE Project_Id = @projId";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@projId", projectId);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while connecting to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidEntryException("Error occurred while deleting the employee.", ex);
            }
        }

        public bool DeleteTask(int taskId)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    if (conn == null)
                    {
                        throw new DatabaseConnectionException("Database connection failed.");
                    }

                    conn.Open();
                    string query = "DELETE FROM Tasks WHERE Task_Id = @taskId";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@taskId", taskId);
                    int result = cmd.ExecuteNonQuery();

                    if (result == 0)
                    {
                        Console.WriteLine($"No task found with Task_ID = {taskId}");
                    }
                    return result > 0;
                }
            }
           
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while connecting to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidEntryException("Error occurred while deleting the task.", ex);
            }

        }

        public bool UpdateEmployee(Employee emp)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    if (conn == null)
                    {
                        throw new DatabaseConnectionException("Database connection failed.");
                    }

                    conn.Open();
                    string query = "UPDATE Employee SET Employee_Name = @name, Designation = @designation, Gender = @gender, Salary = @salary, Project_Id = @projId WHERE Employee_Id = @empId";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", emp.EmployeeName);
                    cmd.Parameters.AddWithValue("@designation", emp.Designation);
                    cmd.Parameters.AddWithValue("@gender", emp.Gender);
                    cmd.Parameters.AddWithValue("@salary", emp.Salary);
                    cmd.Parameters.AddWithValue("@projId", emp.ProjectId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@empId", emp.EmployeeId);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while connecting to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidEntryException("Error occurred while updating the employee details.", ex);
            }
        }
        public bool UpdateProject(Project project)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    if (conn == null)
                    {
                        throw new DatabaseConnectionException("Database connection failed.");
                    }

                    conn.Open();
                    string query = "UPDATE Projects SET Project_Name = @name, Description = @description, Start_Date = @startdate, Status = @status WHERE Project_Id = @projId";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", project.ProjectName);
                    cmd.Parameters.AddWithValue("@description", project.Description);
                    cmd.Parameters.AddWithValue("@startdate", project.StartDate);
                    cmd.Parameters.AddWithValue("@status", project.Status);
                    cmd.Parameters.AddWithValue("@projId", project.ProjectId);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while connecting to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidEntryException("Error occurred while updating the project details.", ex);
            }
        }

        public bool UpdateTask(ProjectTask task)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    if (conn == null)
                    {
                        throw new DatabaseConnectionException("Database connection failed.");
                    }

                    conn.Open();
                    string query = "UPDATE Tasks SET Task_Name = @taskName, Project_Id = @projId, Employee_Id = @empId, Status = @status, Allocation_Date = @allocationDate, Deadline_Date = @deadlineDate WHERE Task_ID = @taskId";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@taskName", task.TaskName);
                    cmd.Parameters.AddWithValue("@projId", task.ProjectId);
                    cmd.Parameters.AddWithValue("@empId", task.EmployeeId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@status", task.Status);
                    cmd.Parameters.AddWithValue("@allocationDate", task.AllocationDate);
                    cmd.Parameters.AddWithValue("@deadlineDate", task.DeadlineDate);
                    cmd.Parameters.AddWithValue("@taskId", task.TaskId);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while connecting to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidEntryException("Error occurred while updating the task details.", ex);
            }
        }



        public List<ProjectTask> GetParticularTasks(int empId, int projectId)
        {
            List<ProjectTask> tasks = new List<ProjectTask>();

            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    if (conn == null)
                    {
                        throw new DatabaseConnectionException("Database connection failed.");
                    }

                    conn.Open();
                    string query = "SELECT * FROM Tasks WHERE Employee_Id = @empId AND Project_Id = @projId";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@empId", empId);
                    cmd.Parameters.AddWithValue("@projId", projectId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        tasks.Add(new ProjectTask
                        {
                            TaskId = Convert.ToInt32(reader["Task_ID"]),
                            TaskName = reader["Task_Name"].ToString(),
                            ProjectId = Convert.ToInt32(reader["Project_Id"]),
                            EmployeeId = Convert.ToInt32(reader["Employee_Id"]),
                            Status = reader["Status"].ToString(),
                            AllocationDate = Convert.ToDateTime(reader["Allocation_Date"]),
                            DeadlineDate = Convert.ToDateTime(reader["Deadline_Date"])
                        });
                    }
                }

                if (tasks.Count == 0)
                {
                    throw new ProjectNotFoundException("No tasks found for the given employee and project.");
                }

                return tasks;
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while connecting to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidEntryException("Error occurred while fetching the tasks for the employee and project.", ex);
            }
        }

    }
}

