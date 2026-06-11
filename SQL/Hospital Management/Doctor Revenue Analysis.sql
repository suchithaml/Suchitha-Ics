-- Doctor Revenue Analysis

WITH MonthlyRevenue AS
(
    SELECT
        d.DoctorID,
        d.DoctorName,
        YEAR(a.AppointmentDate) AS VisitYear,
        MONTH(a.AppointmentDate) AS VisitMonth,
        COUNT(a.AppointmentID) AS TotalAppointments,
        SUM(a.ConsultationFee) AS TotalRevenue
    FROM Doctors d
    INNER JOIN Appointments a
        ON d.DoctorID = a.DoctorID
    WHERE a.Status = 'Completed'
    GROUP BY
        d.DoctorID,
        d.DoctorName,
        YEAR(a.AppointmentDate),
        MONTH(a.AppointmentDate)
)
SELECT
    DoctorID,
    DoctorName,
    VisitYear,
    VisitMonth,
    TotalAppointments,
    TotalRevenue,
    RANK() OVER
    (
        PARTITION BY VisitYear, VisitMonth
        ORDER BY TotalRevenue DESC
    ) AS RevenueRank
FROM MonthlyRevenue
ORDER BY VisitYear DESC, VisitMonth DESC, RevenueRank;