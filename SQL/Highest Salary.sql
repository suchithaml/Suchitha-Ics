-- Find highest salary in each department
SELECT 
    DeptID,
    MAX(Salary) AS HighestSalary
FROM Employees
GROUP BY DeptID;

-- Better readable version with department name
SELECT 
    d.DeptName,
    MAX(e.Salary) AS HighestSalary
FROM Employees e
JOIN Departments d ON e.DeptID = d.DeptID
GROUP BY d.DeptName;