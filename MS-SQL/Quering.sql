GO
--1
SELECT TOP (5)
	 e.EmployeeID
	,e.JobTitle
	,a.AddressID
	,a.AddressText
	FROM Employees AS e
	JOIN Addresses AS a ON a.AddressID = e.AddressID
ORDER BY a.AddressID
--2
SELECT TOP (50)
	e.FirstName
	,e.LastName
	,t.[Name]
	,a.AddressText
	FROM Employees AS e
	JOIN Addresses AS a ON a.AddressID = e.AddressID
	JOIN Towns AS t ON t.TownID = a.TownID
ORDER BY FirstName,LastName
--3
SELECT 
	e.EmployeeID
	,e.FirstName
	,e.LastName
	,d.[Name] AS DepartmentName
	FROM Employees AS e
	JOIN Departments AS d ON d.DepartmentID = e.DepartmentID
	WHERE d.[Name] = 'Sales'
ORDER BY e.EmployeeID
--4
SELECT TOP (5) 
	e.EmployeeID 
	,e.FirstName
	,e.Salary
	,d.[Name] AS DepartmentName
	FROM Employees AS e
	JOIN Departments AS d ON d.DepartmentID = e.DepartmentID
	WHERE e.Salary > 15000
ORDER BY e.DepartmentID
--5
SELECT TOP (3) 
	e.EmployeeID
	,e.FirstName
	FROM Employees AS e
LEFT JOIN EmployeesProjects AS ep ON ep.EmployeeID = e.EmployeeID
	WHERE ep.EmployeeID IS NULL
	ORDER BY e.EmployeeID
--6
SELECT e.FirstName
	,e.LastName
	,e.HireDate
	,d.[Name] AS DeptName
	FROM Employees AS e
	JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
WHERE e.HireDate > '1.1.1999' AND d.[Name] IN('Sales','Finance')
ORDER BY HireDate
--7
SELECT TOP (5) 
	e.EmployeeID
	,e.FirstName
	,p.[Name] AS ProjectName
	FROM Employees AS e
	JOIN EmployeesProjects AS ep ON e.EmployeeID = ep.EmployeeID
	JOIN Projects AS p ON p.ProjectID = ep.ProjectID
WHERE p.StartDate > '08.13.2002' AND p.EndDate IS NULL
ORDER BY e.EmployeeID
--8
SELECT 
	e.EmployeeID
	,e.FirstName
	,CASE
		WHEN DATEPART(YEAR, p.StartDate) >= 2005 THEN NULL
		ELSE p.[Name]
	END AS ProjectName
	FROM Employees AS e
	JOIN EmployeesProjects AS ep ON e.EmployeeID = ep.EmployeeID
	JOIN Projects AS p ON ep.ProjectID = p.ProjectID
WHERE e.EmployeeID = 24
--9
SELECT 
	emp.EmployeeID
	,emp.FirstName
	,emp.ManagerID
	,men.FirstName AS ManagerName
	FROM Employees AS emp
	JOIN Employees AS men ON men.EmployeeID = emp.ManagerID
	WHERE emp.ManagerID IN (3,7)
ORDER BY EmployeeID
--10
SELECT TOP (50)
	e.EmployeeID
	,e.FirstName + ' ' + e.LastName AS EmployeeName
	,mng.FirstName + ' ' + mng.LastName AS ManagerName
	,d.[Name] AS DepartmentName
	FROM Employees AS e
	JOIN Employees AS mng ON mng.EmployeeID = e.ManagerID
	JOIN Departments AS d ON d.DepartmentID = e.DepartmentID
ORDER BY e.EmployeeID
--11
SELECT TOP (1) AVG(Salary) AS AverageSalary
	FROM Employees AS e
	JOIN Departments AS d ON d.DepartmentID = e.DepartmentID
	GROUP BY e.DepartmentID
ORDER BY AverageSalary
GO
GO
--12
--USE Geography
SELECT cc.CountryCode
	,m.MountainRange
	,p.PeakName
	,p.Elevation
	FROM Countries AS cc
	JOIN MountainsCountries AS mc ON mc.CountryCode = cc.CountryCode
	JOIN Mountains AS m ON m.Id = mc.MountainId
	JOIN Peaks AS p ON p.MountainId = m.Id
WHERE cc.CountryCode = 'BG' AND p.Elevation > 2835
ORDER BY p.Elevation DESC
--13
SELECT c.CountryCode, COUNT(*)
	FROM Countries AS c
	JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
WHERE c.CountryCode IN('BG','US','RU')
GROUP BY c.CountryCode
--14
SELECT TOP(5)
	c.CountryName
	,r.RiverName
	FROM Countries AS c
	LEFT JOIN CountriesRivers AS cr ON cr.CountryCode = c.CountryCode
	LEFT JOIN Rivers AS r ON cr.RiverId = r.Id
	LEFT JOIN Continents AS cont ON cont.ContinentCode = c.ContinentCode
WHERE cont.ContinentName = 'Africa'
ORDER BY c.CountryName
--15
--Create a query that selects:
--ContinentCode
--CurrencyCode
--CurrencyUsage
--Find all continents and their most used currency. Filter any currency, which is used in only one country. Sort your results by ContinentCode.
SELECT ContinentCode,CurrencyCode,Total
	FROM 
(
	SELECT ContinentCode
		,CurrencyCode
		,COUNT(CurrencyCode) AS Total
		,DENSE_RANK() OVER(PARTITION BY ContinentCode 
		ORDER BY COUNT(CurrencyCode) DESC) AS Ranked
	FROM Countries
GROUP BY ContinentCode,CurrencyCode
) AS k
WHERE Ranked = 1 AND Total > 1
ORDER BY ContinentCode
--16
SELECT COUNT(*) AS [Count]
	FROM Countries AS c
	LEFT JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
WHERE mc.MountainId IS NULL
--17
--For each country, find the elevation of the highest peak and the length of the longest river,
--sorted by the highest peak elevation (from highest to lowest),
--then by the longest river length (from longest to smallest),
-- then by country name (alphabetically). Display NULL when no data is available in some of the columns. Limit only the first 5 rows.
SELECT TOP(5) c.CountryName
	,MAX(p.Elevation) AS HighestPeakElevation
	,MAX(r.[Length]) AS LongestRiverLength
	FROM Countries AS c
	LEFT JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
	LEFT JOIN Mountains AS m ON mc.MountainId = m.Id
	LEFT JOIN Peaks AS p ON p.MountainId = m.Id 
	LEFT JOIN CountriesRivers AS cr ON c.CountryCode = cr.CountryCode
	LEFT JOIN Rivers AS r ON r.Id = cr.RiverId
GROUP BY c.CountryName
ORDER BY HighestPeakElevation DESC,LongestRiverLength DESC,CountryName
--18
SELECT TOP(5)
	k.Country
	,k.HighestPeakName
	,k.HighestPeakElevation
	,k.Mountain
	FROM
(
SELECT c.CountryName AS Country
	,ISNULL (p.PeakName,'(no highest peak)') AS HighestPeakName
	,ISNULL (MAX(p.Elevation),0) AS HighestPeakElevation
	,ISNULL(m.MountainRange,'(no mountain)') AS Mountain
	,DENSE_RANK() OVER (PARTITION BY c.CountryName ORDER BY MAX(p.Elevation) DESC) AS Ranked
	FROM Countries AS c
	LEFT JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
	LEFT JOIN Mountains AS m ON mc.MountainId = m.Id
	LEFT JOIN Peaks AS p ON p.MountainId = m.Id 
	LEFT JOIN CountriesRivers AS cr ON c.CountryCode = cr.CountryCode
	LEFT JOIN Rivers AS r ON r.Id = cr.RiverId
GROUP BY c.CountryName,p.PeakName,m.MountainRange
) AS k
WHERE Ranked = 1
ORDER BY Country,HighestPeakName

