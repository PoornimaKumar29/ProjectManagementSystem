//using Project_Management.dao;
//using Project_Management.entity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using NUnit.Framework;

//namespace ProjectManagementTest
//{
//    class ProjectTest
//    {

//        [TestFixture]
//        public class ProjectRepositoryTests
//        {
//            private ProjectRepositoryImpl repo;

//            [SetUp]
//            public void Setup()
//            {
//                repo = new ProjectRepositoryImpl();
//            }

//            // 1. Test: Employee creation
//            [Test]
//            public void CreateEmployee_ShouldReturnTrue_WhenValidEmployeeGiven()
//            {
//                var employee = new Employee
//                {
//                    EmployeeName = "John Doe",
//                    Designation = "Developer",
//                    Gender = "Male",
//                    Salary = 50000,
//                    ProjectId = 1 // Make sure project with ID 1 exists
//                };

//                bool result = repo.CreateEmployee(employee);

//                Assert.IsTrue(result, "Employee should be created successfully.");
//            }

//            // 2. Test: Task creation
//            [Test]
//            public void CreateTask_ShouldReturnTrue_WhenValidTaskGiven()
//            {
//                var task = new ProjectTask
//                {
//                    TaskName = "Exeption handling",
//                    AllocationDate = new DateTime(2025, 04,03),
//                    DeadlineDate = DateTime.Now.AddDays(7),
//                    Status = "Started",
//                    ProjectId = 4// Make sure project with ID 1 exists
//                };

//                bool result = repo.CreateTask(task);

//                Assert.IsTrue(result, "Task should be created successfully.");
//            }

//            // 3. Test: Search projects & tasks assigned to employee
//            [Test]
//            public void SearchProjectsAndTasks_ShouldReturnData_WhenEmployeeIdIsValid()
//            {
//                int validEmployeeId = 1; // Use an existing employee ID with tasks assigned

//                var result = repo.SearchProjectsAndTasks(validEmployeeId);

//                Assert.IsNotNull(result, "Result should not be null.");
//                Assert.IsTrue(result.Count > 0, "Projects and tasks should be returned.");
//            }

//            // 4. Test: Exception when invalid employee ID
//            [Test]
//            public void SearchProjectsAndTasks_ShouldThrowException_WhenEmployeeIdIsInvalid()
//            {
//                int invalidEmployeeId = -1;

//                var ex = Assert.Throws<Exception>(() => repo.SearchProjectsAndTasks(invalidEmployeeId));

//                Assert.That(ex.Message, Is.EqualTo("No project and tasks found for this employee."));
//            }
//        }
//    }


//}
using NUnit.Framework;
using Project_Management.dao;
using Project_Management.entity;
using Project_Management.exception;
using System;
using System.Collections.Generic;

namespace ProjectSystem.Tests
{
    [TestFixture]
    public class ProjectRepositoryTests
    {
        private IProjectRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new ProjectRepositoryImpl();
        }

        [Test]
        public void CreateEmployee_ShouldAddEmployeeSuccessfully()
        {
            // Arrange
            var employee = new Employee
            {
                EmployeeName = "Alice Johnson",
                Designation = "Software Developer",
                Gender = "Female",
                Salary = 60000,
                ProjectId = 6
            };

            // Act
            bool result = _repository.CreateEmployee(employee);

            // Assert
            Assert.IsTrue(result, "Employee creation should be successful.");
        }

        [Test]
        public void CreateTask_ShouldAddTaskSuccessfully()
        {
            // Arrange
            var task = new ProjectTask
            {
                TaskName = "UI Interface",
                ProjectId = 3, // Make sure this project exists in test DB
                EmployeeId = null,
                Status = "Assigned",
                AllocationDate = DateTime.Now,
                DeadlineDate = DateTime.Now.AddDays(5)
            };

            // Act
            bool result = _repository.CreateTask(task);

            // Assert
            Assert.IsTrue(result, "Task creation should be successful.");
        }

        [Test]
        public void GetParticularTasks_ShouldReturn_TasksForEmployeeInProject()
        {
            // Arrange
            int empId = 1; // Ensure valid test employee ID
            int projectId = 1; // Ensure valid test project ID

            // Act
            var tasks = _repository.GetParticularTasks(empId, projectId);

            // Assert
            Assert.IsNotNull(tasks);
            Assert.IsInstanceOf<List<ProjectTask>>(tasks);
            Assert.IsTrue(tasks.Count >= 0, "Should return task list, even if empty.");
        }

        [Test]
        public void CreateEmployee_ShouldThrowInvalidEntryException_ForMissingDetails()
        {
            // Arrange
            var employee = new Employee
            {
                EmployeeName = null, // Invalid
                Designation = null,
                Gender = null,
                Salary = 0,
                ProjectId = null
            };

            // Act & Assert
            Assert.Throws<InvalidEntryException>(() => _repository.CreateEmployee(employee));
        }
    }
}
