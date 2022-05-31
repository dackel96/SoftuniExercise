--1
CREATE TABLE [Passports]
(
	[PassportID] INT PRIMARY KEY IDENTITY(101,1)
	,[PassportNumber] CHAR(8)
)
CREATE TABLE [Persons]
(
	[PersonId] INT PRIMARY KEY IDENTITY
	,[FirstName] VARCHAR(25)
	,[Salary] DECIMAL(10,2)
	,[PassportID] INT UNIQUE FOREIGN KEY REFERENCES [Passports]([PassportID])
)
INSERT INTO [Passports] ([PassportNumber])
	VALUES
	 ('N34FG21B')
	,('K65LO4R7')
	,('ZE657QP2')
INSERT INTO [Persons] ([FirstName],[Salary],[PassportID])
	VALUES
	('Roberto',43300.00,102)
	,('Tom',56100.00,103)
	,('Yana',60200.00,101)
--2
CREATE TABLE [Manufacturers]
(
	[ManufacturerID] INT PRIMARY KEY IDENTITY
	,[Name] VARCHAR(10)
	,[EstablishedOn] DATETIME
)
CREATE TABLE [Models]
(
	[ModelID] INT PRIMARY KEY IDENTITY(101,1)
	,[Name] VARCHAR(25)
	,[ManufacturerID] INT FOREIGN KEY REFERENCES [Manufacturers]([ManufacturerID])
)
INSERT INTO [Manufacturers] ([Name],[EstablishedOn])
	VALUES
	('BMW',07/03/1916)
	,('Tesla',01/01/2003)
	,('Lada',01/05/1966)
INSERT INTO [Models] ([Name],[ManufacturerID])
	VALUES
	('X1',1)
	,('i6',1)
	,('Model S',2)
	,('Model X',2)
	,('Model 3',2)
	,('Nova',3)
--3
CREATE TABLE [Students]	
(
	 [StudentID] INT PRIMARY KEY IDENTITY
	,[Name] VARCHAR(20)
)
CREATE TABLE [Exams]
(
	[ExamID] INT PRIMARY KEY IDENTITY(101,1)
	,[Name] VARCHAR(20)
)
CREATE TABLE [StudentsExams]
(
	[StudentID] INT
	,[ExamID] INT
	CONSTRAINT PK_Students_Exams PRIMARY KEY ([StudentID],[ExamID])
	,CONSTRAINT FK_STUDENTS FOREIGN KEY ([StudentID]) REFERENCES [Students]([StudentID])
	,CONSTRAINT FK_EXAMS FOREIGN KEY ([ExamID]) REFERENCES [Exams]([ExamID])
)
INSERT INTO [Students]
	VALUES
	('Mila')
	,('Toni')
	,('Ron')
INSERT INTO [Exams]
	VALUES
	('SpringMVC')
	,('Neo4j')
	,('Oracle 11g')
INSERT INTO [StudentsExams]
	VALUES
	(1,101)
	,(1,102)
	,(2,101)
	,(3,103)
	,(2,102)
	,(2,103)
--4
CREATE TABLE [Teachers]
(
	[TeacherID] INT PRIMARY KEY IDENTITY(101,1)
	,[Name] VARCHAR(25)
	,[ManagerID] INT FOREIGN KEY REFERENCES [Teachers]([TeacherID])
)
INSERT INTO [Teachers]
	VALUES
	('John',NULL)
	,('Maya',106)
	,('Silvia',106)
	,('Ted',105)
	,('Mark',101)
	,('Greta',101)
--5
CREATE TABLE [Cities]
(
	[CityID] INT PRIMARY KEY IDENTITY
	,[Name] VARCHAR(50)
)
CREATE TABLE [ItemTypes]
(
	[ItemTypeID] INT PRIMARY KEY IDENTITY
	,[Name] VARCHAR(50)
)
CREATE TABLE [Customers]
(
	[CustomerID] INT PRIMARY KEY IDENTITY
	,[Name] VARCHAR(50)
	,[Birthday] DATE
	,[CityID] INT FOREIGN KEY REFERENCES [Cities]([CityID])
)
CREATE TABLE [Orders]
(
	[OrderID] INT PRIMARY KEY IDENTITY
	,[CustomerID] INT FOREIGN KEY REFERENCES [Customers]([CustomerID])
)
CREATE TABLE [Items]
(
	[ItemID] INT PRIMARY KEY IDENTITY
	,[Name] VARCHAR(50)
	,[ItemTypeID] INT FOREIGN KEY REFERENCES [ItemTypes]([ItemTypeID])
)
CREATE TABLE [OrderItems]
(
	[OrderID] INT REFERENCES [Orders]([OrderID])
	,[ItemID] INT REFERENCES [Items]([ItemID])
	CONSTRAINT PK_Order_Item PRIMARY KEY ([OrderID],[ItemID])
)
--6
