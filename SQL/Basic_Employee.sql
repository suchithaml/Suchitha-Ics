-- Version 3: Added JoinDate and extra employees
CREATE TABLE Employees
(
    EmpID    INT PRIMARY KEY,
    Name     VARCHAR(50),
    Salary   DECIMAL(10,2),
    DeptID   INT,
    JoinDate DATE
);

INSERT INTO Employees VALUES (101, 'Arun',    50000, 1, '2023-01-10');
INSERT INTO Employees VALUES (102, 'Bharath', 40000, 2, '2022-06-15');
INSERT INTO Employees VALUES (103, 'Charan',  70000, 1, '2024-03-01');
INSERT INTO Employees VALUES (104, 'Deepak',  65000, 3, '2021-09-20');
INSERT INTO Employees VALUES (105, 'Esha',    55000, 2, '2023-11-05');