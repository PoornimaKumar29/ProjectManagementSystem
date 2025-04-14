-----------------------------------------------------🔹 Insert Data into Project Table------------------------------------------------------------


insert into Projects (Project_Name, Description, Start_Date, Status) values
('E-Commerce App', 'Online shopping platform', '2023-01-10', 'started'),
('HR Portal', 'Employee management system', '2023-02-15', 'dev'),
('Inventory System', 'Warehouse tracking system', '2023-03-20', 'build'),
('Chat Application', 'Real-time messaging', '2023-04-01', 'test'),
('Banking System', 'Online banking and transactions', '2023-06-01', 'dev');
-----------------------------------------------------🔹 Insert Data into Employee Table------------------------------------------------------------


insert into Employee (Employee_Name, Designation, Gender, Salary, Project_Id) values
('Alice', 'Developer', 'Female', 55000.00, 1),
('Bob', 'Tester', 'Male', 48000.00, 1),
('Charlie', 'Project Manager', 'Male', 75000.00, 2),
('Diana', 'Developer', 'Female', 60000.00, 3),
('Ethan', 'Tester', 'Male', 45000.00, 3);


-----------------------------------------------------🔹 Insert Data into Tasks Table------------------------------------------------------------

insert into Tasks (Task_Name, Project_Id, Employee_Id, Status) values
('Login Page', 1, 1, 'Completed'),
('Add to Cart', 5, 1, 'Started'),
('Unit Testing', 1, 2, 'Assigned'),
('Employee CRUD', 4, 3, 'Started'),
('Database Schema', 3, 4, 'Completed');
