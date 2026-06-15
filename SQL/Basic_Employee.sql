-- Version 1: Basic Employee Table
CREATE TABLE Employees
(
    EmpID   INT PRIMARY KEY,
    Name    VARCHAR(50),
    Salary  DECIMAL(10,2)
);

INSERT INTO Employees VALUES (101, 'Arun',   50000);
INSERT INTO Employees VALUES (102, 'Bharath', 40000);