CREATE TABLE Departments (
    DeptID INT PRIMARY KEY,
    DeptName VARCHAR(50)
);

CREATE TABLE Employees (
    EmpID INT PRIMARY KEY,
    Name VARCHAR(50),
    DeptID INT,
    Salary INT,
    JoinDate DATE,
    FOREIGN KEY (DeptID) REFERENCES Departments(DeptID)
);

INSERT INTO Departments VALUES
(1, 'IT'),
(2, 'HR'),
(3, 'Finance');

INSERT INTO Employees VALUES
(101, 'Arun', 1, 50000, '2023-01-10'),
(102, 'Bharath', 2, 40000, '2022-06-15'),
(103, 'Charan', 1, 70000, '2024-03-01'),
(104, 'Deepak', 3, 65000, '2021-09-20');

-- Get employees with department name and salary > 45000
SELECT 
    e.EmpID,
    e.Name,
    d.DeptName,
    e.Salary
FROM Employees e
INNER JOIN Departments d ON e.DeptID = d.DeptID
WHERE e.Salary > 45000
ORDER BY e.Salary DESC;