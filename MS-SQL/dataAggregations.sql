GO
--1
SELECT COUNT(*) AS [Count]
	FROM WizzardDeposits
--2
SELECT MAX(MagicWandSize) AS LongestMagicWand
	FROM WizzardDeposits
--3
SELECT DepositGroup
	,MAX(MagicWandSize) AS LongestMagicWand
	FROM WizzardDeposits
GROUP BY DepositGroup
--4
SELECT TOP(2) DepositGroup
	FROM WizzardDeposits
GROUP BY DepositGroup
ORDER BY AVG(MagicWandSize)
--5
SELECT DepositGroup
	,SUM(DepositAmount) AS TotalSum
	FROM WizzardDeposits
GROUP BY DepositGroup
--6
SELECT DepositGroup
	,SUM(DepositAmount) AS TotalSum
	FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup
--7
SELECT DepositGroup
	,SUM(DepositAmount) AS TotalSum
	FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup
	HAVING SUM(DepositAmount) < 150000
ORDER BY TotalSum DESC
--8
SELECT DepositGroup
	,MagicWandCreator
	,MIN(DepositCharge) AS MinDepositCharge
	FROM WizzardDeposits
GROUP BY MagicWandCreator,DepositGroup
ORDER BY MagicWandCreator,DepositGroup
--9
SELECT Result.AgeGroup, COUNT(Result.AgeGroup)
	FROM (
	SELECT CASE 
		WHEN Age BETWEEN 0 AND 10 THEN '[0-10]'
		WHEN Age BETWEEN 11 AND 20 THEN '[11-20]'
		WHEN Age BETWEEN 21 AND 30 THEN '[21-30]'
		WHEN Age BETWEEN 31 AND 40 THEN '[31-40]'
		WHEN Age BETWEEN 41 AND 50 THEN '[41-50]'
		WHEN Age BETWEEN 51 AND 60 THEN '[51-60]'
		WHEN Age >= 61 THEN '[61+]'
	END AS AgeGroup
	FROM WizzardDeposits) AS Result
GROUP BY Result.AgeGroup
--10
SELECT LEFT(FirstName,1) AS FirstLetter
	FROM WizzardDeposits
WHERE DepositGroup = 'Troll Chest'
GROUP BY LEFT(FirstName,1)
--11
SELECT DepositGroup
	,IsDepositExpired
	,AVG(DepositInterest) AS AverageInterest
	FROM WizzardDeposits
	WHERE DepositStartDate > '01/01/1985'
	GROUP BY DepositGroup,IsDepositExpired
ORDER BY DepositGroup DESC,IsDepositExpired
--12
SELECT SUM(Guest.DepositAmount - Host.DepositAmount) AS [Difference]
	FROM WizzardDeposits AS Host
	JOIN WizzardDeposits AS Guest ON Guest.Id +1 = Host.Id
GO
GO
--13
USE SoftUni
SELECT DepartmentID
	,SUM(Salary)
	FROM Employees
GROUP BY DepartmentID
ORDER BY DepartmentID

--14
--Select the minimum salary from the employees for departments with ID (2, 5, 7) but only for those, hired after 01/01/2000.
SELECT DepartmentID
	,MIN(Salary) AS MinimumSalary
	FROM Employees
	WHERE DepartmentID IN (2,5,7) AND HireDate > '01/01/2000'
GROUP BY DepartmentID
--15

SELECT * INTO EmployeesAvgSalaryTable
	FROM Employees
	WHERE Salary > 30000

DELETE FROM EmployeesAvgSalaryTable
	WHERE ManagerID = 42

UPDATE EmployeesAvgSalaryTable
	SET Salary += 5000
	WHERE DepartmentID = 1

SELECT DepartmentID
	,AVG(Salary)
	FROM EmployeesAvgSalaryTable
GROUP BY DepartmentID
--16
SELECT DepartmentID
	,MAX(Salary) AS MaxSalary
	FROM Employees
GROUP BY DepartmentID
HAVING MAX(Salary) NOT BETWEEN 30000 AND 70000
--17
SELECT COUNT(*) AS [Count]
	FROM Employees 
WHERE ManagerID IS NULL
--18
SELECT DepartmentID
	,MAX(Salary)
	FROM Employees
GROUP BY DepartmentID
--19
SELECT DISTINCT Result.DepartmentID,Result.Salary
	FROM(SELECT DepartmentID
	,Salary
	,DENSE_RANK() OVER(PARTITION BY DepartmentID ORDER BY Salary DESC) AS [Ranked]
	FROM Employees) AS Result
WHERE Result.Ranked = 3
--20
SELECT TOP (10) FirstName,LastName,DepartmentID
	FROM Employees AS emplo
	WHERE Salary > (SELECT AVG(Salary)
						FROM Employees
						WHERE DepartmentID = emplo.DepartmentID
						GROUP BY DepartmentID)
ORDER BY DepartmentID