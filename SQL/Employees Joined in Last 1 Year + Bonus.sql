-- Assume bonus = 10% of salary for recent joiners
SELECT 
    EmpID,
    Name,
    Salary,
    JoinDate,
    (Salary * 0.10) AS Bonus
FROM Employees
WHERE JoinDate >= DATEADD(YEAR, -1, GETDATE());