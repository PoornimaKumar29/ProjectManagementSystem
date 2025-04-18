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
                        throw new Exception("Database connection failed.");

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

        }

        public bool CreateEmployee(Employee emp)
        {
            if (string.IsNullOrEmpty(emp.EmployeeName) ||
        string.IsNullOrEmpty(emp.Designation) ||
        string.IsNullOrEmpty(emp.Gender) ||
        emp.Salary <= 0 ||
        emp.ProjectId == null)
            {
                throw new InvalidEntryException("Employee details are missing or invalid.");
            }
            try
            {

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
            catch (SqlException)
            {
                throw new EmployeeNotFoundException("Employee or Project not found.");
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
                        throw new Exception("Database connection failed.");
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

            catch (ProjectNotFoundException ex)
            {
                throw ex;
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
                        throw new DatabaseConnectionException("Database connection failed.");
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
            catch (EmployeeNotFoundException ex)
            {
                throw ex;
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
                if (tasks.Count == 0)
                {
                    throw new TaskNotFoundException("No Tasks found in the database.");
                }

              
                return tasks;
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while connecting to the database.", ex);
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


                    if (result == 0)
                    {
                        throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found.");
                    }

                    return result > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while executing the update operation in the database.", ex);
            }
            catch (EmployeeNotFoundException ex)
            {
                throw new EmployeeNotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while assigning project to the employee.", ex);
            }
        }

        public bool AssignTaskInProjectToEmployee(int taskId, int projectId, int employeeId)
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

                    string query = "UPDATE Tasks SET Employee_Id = @empId WHERE Task_ID = @taskId AND Project_Id = @projId AND Employee_Id IS NULL";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@empId", employeeId);
                    cmd.Parameters.AddWithValue("@taskId", taskId);
                    cmd.Parameters.AddWithValue("@projId", projectId);

                    int result = cmd.ExecuteNonQuery();

                    if (result == 0)
                    {
                        throw new TaskNotFoundException($"Task with ID {taskId} in project {projectId}");
                    }

                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while connecting to the database.", ex);
            }
            catch (TaskNotFoundException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while assigning the task.", ex);
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

                    if (result == 0)
                    {
                        throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found.");
                    }

                    return true; 
                }
            }
           
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return false;
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

                    if (result == 0)
                    {
                        throw new ProjectNotFoundException($"Project with ID {projectId} not found.");
                    }

                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while connecting to the database.", ex);
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
                        throw new TaskNotFoundException($"Task with ID {taskId} not found.");
                    }

                    return true; 
                }
            }
            catch (TaskNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (SqlException ex)
            {
                
                Console.WriteLine($"Database error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return false;
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

                    if (result == 0)
                    {
                        throw new EmployeeNotFoundException($"Employee with ID {emp.EmployeeId} not found.");
                    }

                    return true; 
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return false;
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

                    if (result == 0)
                    {
                        throw new ProjectNotFoundException($"Project with ID {project.ProjectId} not found or no changes were made.");
                    }

                    return result > 0; 
                }
            }
            catch (SqlException ex)
            {
               
                throw new DatabaseConnectionException("Error while connecting to the database.", ex);
            }
            catch (ProjectNotFoundException ex)
            {
                
                throw ex;  
            }
            catch (Exception ex)
            {
               
                throw new ApplicationException("An unexpected error occurred while updating the project.", ex);
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

                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Update Task
                        string updateTaskQuery = @"
                    UPDATE Tasks 
                    SET Task_Name = @taskName, Project_Id = @projId, Employee_Id = @empId, Status = @status, Allocation_Date = @allocationDate, 
                        Deadline_Date = @deadlineDate 
                    WHERE Task_ID = @taskId";

                        SqlCommand updateTaskCmd = new SqlCommand(updateTaskQuery, conn, transaction);
                        updateTaskCmd.Parameters.AddWithValue("@taskName", task.TaskName);
                        updateTaskCmd.Parameters.AddWithValue("@projId", task.ProjectId);
                        updateTaskCmd.Parameters.AddWithValue("@empId", task.EmployeeId ?? (object)DBNull.Value);
                        updateTaskCmd.Parameters.AddWithValue("@status", task.Status);
                        updateTaskCmd.Parameters.AddWithValue("@allocationDate", task.AllocationDate);
                        updateTaskCmd.Parameters.AddWithValue("@deadlineDate", task.DeadlineDate);
                        updateTaskCmd.Parameters.AddWithValue("@taskId", task.TaskId);

                        int result = updateTaskCmd.ExecuteNonQuery();

                        if (result == 0)
                        {
                            // Task not found in the database
                            throw new TaskNotFoundException($"Task with ID {task.TaskId} not found.");
                        }

                        // Update Employee if EmployeeId is provided
                        if (task.EmployeeId.HasValue)
                        {
                            string updateEmpQuery = "UPDATE Employee SET Project_Id = @projId WHERE Employee_Id = @empId";
                            SqlCommand updateEmpCmd = new SqlCommand(updateEmpQuery, conn, transaction);
                            updateEmpCmd.Parameters.AddWithValue("@projId", task.ProjectId);
                            updateEmpCmd.Parameters.AddWithValue("@empId", task.EmployeeId);
                            updateEmpCmd.ExecuteNonQuery();
                        }

                        // Commit the transaction if everything succeeds
                        transaction.Commit();
                        return result > 0;
                    }
                    catch (SqlException ex)
                    {
                        // Rollback the transaction if there is an error
                        transaction.Rollback();
                        throw new DatabaseConnectionException("Error while executing the update task operation.", ex);
                    }
                    catch (TaskNotFoundException ex)
                    {
                        // Specifically catch TaskNotFoundException
                        transaction.Rollback();
                        throw new TaskNotFoundException($"Error: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        // Catch all other exceptions
                        transaction.Rollback();
                        throw new ApplicationException("An unexpected error occurred during the task update process.", ex);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while connecting to the database.", ex);
            }
        }

        public List<ProjectTask> GetParticularTasks(int empId)
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
                    string query = "SELECT * FROM Tasks WHERE Employee_Id = @empId ";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@empId", empId);

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
          
        }

    }
}

