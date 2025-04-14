create database project_management
use project_management

---------------------------------------------Create ProjectTable----------------------------------------------------------
create table Projects(
Project_Id int identity(1,1) primary key,
Project_Name varchar(100) not null,
Description varchar(100),
Start_Date date,
Status varchar(100) 
check(Status in ('started', 'dev', 'build', 'test', 'deployed')) not null)
drop table Projects
-------------------------------------------------------Create EmployeeTable------------------------------------------

create table Employee(
Employee_Id int identity(1,1) primary key,
Employee_Name varchar(100) not null,
Designation varchar(50) not null,
Gender varchar(100) check(Gender in ('Male','Female','Other')),
Salary decimal(10,2),
 Project_Id int foreign key(Project_Id) references Projects(Project_Id) on delete set null)
drop table employee
--------------------------------------------------------Create Task table-------------------------------------------
create table Tasks(
Task_ID int identity(1,1) primary key ,
Task_Name varchar(100) not null,
Project_Id int foreign key(Project_Id) references Projects(Project_Id) on delete cascade,
Employee_Id int foreign key(Employee_Id) references Employee(Employee_Id) on delete set null,
Status varchar(100) check(Status in ('Assigned','Started','Completed'))
)

ALTER TABLE Tasks ADD Allocation_Date DATE, Deadline_Date DATE;

UPDATE Tasks SET Allocation_Date = '2025-04-03', Deadline_Date = '2025-04-09' WHERE Task_ID = 5;
UPDATE Tasks SET Allocation_Date = '2025-04-03', Deadline_Date = '2025-04-10' WHERE Task_ID = 9;

UPDATE Tasks SET Allocation_Date = '2025-04-08', Deadline_Date = '2025-04-17' WHERE Task_ID = 6;
UPDATE Tasks SET Allocation_Date = '2025-04-08', Deadline_Date = '2025-04-18' WHERE Task_ID = 8;
UPDATE Tasks SET Allocation_Date = '2025-04-13', Deadline_Date = '2025-04-23' WHERE Task_ID = 7;

drop table Tasks


