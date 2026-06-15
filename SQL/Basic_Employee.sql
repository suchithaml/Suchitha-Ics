-- Version 2: Added Department column
CREATE TABLE Employees
(
    EmpID    INT PRIMARY KEY,
    Name     VARCHAR(50),
    Salary   DECIMAL(10,2),
    DeptID   INT
);

INSERT INTO Employees VALUES (101, 'Arun',    50000, 1);
INSERT INTO Employees VALUES (102, 'Bharath', 40000, 2);
INSERT INTO Employees VALUES (103, 'Charan',  70000, 1);