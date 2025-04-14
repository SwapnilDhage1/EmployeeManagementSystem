Create database New_CompTask;
Use New_CompTask;

Create Table State(
StateID int identity(1,1) Primary Key ,
StateName Varchar(100)Not Null);

Insert Into State (StateName) values
('Andhra Pradesh'),
('Arunachal Pradesh'),
('Assam'),
('Bihar'),
('Chhattisgarh'),
('Goa'),
('Gujarat'),
('Haryana'),
('Himachal Pradesh'),
('Jharkhand'),
('Karnataka'),
('Kerala'),
('Madhya Pradesh'),
('Maharashtra'),
('Manipur'),
('Meghalaya'),
('Mizoram'),
('Nagaland'),
('Odisha'),
('Punjab'),
('Rajasthan'),
('Sikkim'),
('Tamil Nadu'),
('Telangana'),
('Tripura'),
('Uttar Pradesh'),
('Uttarakhand'),
('West Bengal'),
('Andaman and Nicobar Islands'),
('Chandigarh'),
('Dadra and Nagar Haveli and Daman and Diu'),
('Delhi'),
('Jammu and Kashmir'),
('Ladakh'),
('Lakshadweep'),
('Puducherry');


Create Table Employees(
EmpID Int  Primary Key Identity (1,1),
Name Varchar(50) Not Null,
Designation Varchar(40) Not null,
Date_Of_Birth Date Not null,
Date_Of_Joining Date Not Null,
Salary Decimal (10, 2)Not null,
Gender Varchar (10),
StateId int References State(StateID) On delete cascade);

Insert Into Employees Values
('Swapnil', 'Software Developer', '2002-01-01', '2025-10-10', 50000.00, 'Male', 15);

Go

Create Procedure GetAllEmployee
as
Begin
 Select * from Employees;
 End

 Exec GetAllEmployee
 Go

 Create Proc GetEmployeeByID
 @EmpID Int 
 AS
 Begin
 Select * from Employees Where EmpID=@EmpID;
 End;
 Exec GetEmployeeByID 1
 Go

 Create Proc SaveEmployee
 
 @Name Varchar(50),
 @Designation varchar(40),
 @Date_Of_Birth date,
 @Date_Of_joining date,
 @Salary Decimal (10, 2),
 @Gender Varchar(10),
 @StateID  int 
 as 
 begin 
 Insert Into Employees (Name,Designation,Date_Of_Birth,Date_Of_Joining,Salary,Gender,StateId)Values
 (@Name,@Designation,@Date_Of_Birth,@Date_Of_joining,@Salary,@Gender,@StateID);
 End

 Exec SaveEmployee 'nil', 'Software Developer', '2002-01-01', '2025-10-10', 50000.00, 'Male', 15;
Go

Create Proc  EditEmployee
@EmpID Int ,
 @Name Varchar(50),
 @Designation varchar(40),
 @Date_Of_Birth date,
 @Date_Of_joining date,
 @Salary Decimal (10, 2),
 @Gender Varchar(10),
 @StateID  int 
 as 
 begin 
 Update Employees Set
 Name=@Name,
 Designation=@Designation,
 Date_Of_Birth=@Date_Of_Birth,
 Date_Of_Joining=@Date_Of_joining,
 Salary=@Salary,
 Gender=@Gender,
 StateId=@StateID
 Where EmpID=@EmpID

 End

 Exec EditEmployee 2, 'Nil', ' Full stak developer', '2002-01-01', '2025-10-10', 50000.00, 'Male', 15;
 Go

 Create Proc DeleteEmployee
 @EmpID int 
 As
 Begin 
 Delete Employees Where EmpID=@EmpID
 End
 Exec DeleteEmployee 1;