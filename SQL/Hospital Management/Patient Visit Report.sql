-- Patient Visit Report

SELECT
    p.PatientID,
    p.FirstName + ' ' + p.LastName AS PatientName,
    d.DoctorName,
    dep.DepartmentName,
    a.AppointmentDate,
    a.Status,
    a.ConsultationFee
FROM Patients p
INNER JOIN Appointments a
    ON p.PatientID = a.PatientID
INNER JOIN Doctors d
    ON a.DoctorID = d.DoctorID
INNER JOIN Departments dep
    ON d.DepartmentID = dep.DepartmentID
WHERE a.AppointmentDate >= DATEADD(MONTH, -3, GETDATE())
ORDER BY a.AppointmentDate DESC;