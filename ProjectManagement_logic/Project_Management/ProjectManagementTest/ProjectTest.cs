<<<<<<< HEAD
﻿
=======

>>>>>>> 659932d8ba7d9f04365aac4895065f55c557287d
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
                ProjectId = 6, // Make sure this project exists in test DB
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
            // Arrang
            int empId = 2; // Ensure valid test employee ID
            int projectId = 5; // Ensure valid test project ID

            // Act
            var tasks = _repository.GetParticularTasks(empId);

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
