-- Department Performance Dashboard

WITH DepartmentStats AS
(
    SELECT
        dep.DepartmentID,
        dep.DepartmentName,
        COUNT(DISTINCT d.DoctorID) AS TotalDoctors,
        COUNT(a.AppointmentID) AS TotalAppointments,
        SUM(ISNULL(a.ConsultationFee,0)) AS Revenue,
        AVG(ISNULL(a.ConsultationFee,0)) AS AvgConsultationFee
    FROM Departments dep
    LEFT JOIN Doctors d
        ON dep.DepartmentID = d.DepartmentID
    LEFT JOIN Appointments a
        ON d.DoctorID = a.DoctorID
    GROUP BY
        dep.DepartmentID,
        dep.DepartmentName
),
TopDepartment AS
(
    SELECT
        *,
        DENSE_RANK() OVER
        (
            ORDER BY Revenue DESC
        ) AS RevenueRank
    FROM DepartmentStats
)
SELECT
    DepartmentID,
    DepartmentName,
    TotalDoctors,
    TotalAppointments,
    Revenue,
    AvgConsultationFee,
    RevenueRank
FROM TopDepartment
ORDER BY RevenueRank;