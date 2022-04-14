using System;
using System.Linq;
using System.Collections.Generic;


namespace ASSIGNMENT4
{
    
    public class Program
    {
        
        IList<Employee> employeeList;
        IList<Salary> salaryList;

        public Program()
        {
            employeeList = new List<Employee>() {
            new Employee(){ EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49},
            new Employee(){ EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32},
            new Employee(){ EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28},
            new Employee(){ EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26},
            new Employee(){ EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20},
            new Employee(){ EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60},
            new Employee(){ EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53}
        };

            salaryList = new List<Salary>() {
            new Salary(){ EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 1, Amount = 500, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 600, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 6, Amount = 400, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly}
        };
    }
    
    public static void Main()
        {
            Program program = new Program();

            program.Task1();

            program.Task2();

            program.Task3();
        }

        public void Task1()
        {
            //Printing total Salary of all the employees with their corresponding names in ascending order of their salary---------------
            var salaryGroup = from salary in salaryList group salary by salary.EmployeeID into salaries select new{
                salaryEmployeeId = salaries.First().EmployeeID,
                TotalSalary = salaries.Sum(salary => salary.Amount)
            };
            var salaryEmployees = from list in employeeList join salary in salaryGroup on list.EmployeeID equals salary.salaryEmployeeId orderby salary.TotalSalary select new{
                Total_salary = salary.TotalSalary,
                firstName = list.EmployeeFirstName,
                lastName = list.EmployeeLastName
           };
           Console.WriteLine("-Salary of all the employees with their corresponding names in ascending order of their salary-");
           foreach (var employee in salaryEmployees){
               Console.WriteLine($"Total salary of {employee.firstName} {employee.lastName} = {employee.Total_salary}");
            }
        }

        public void Task2()
        {
            //Printing Employee details of 2nd oldest employee including his/her total monthly salary.---------------------
            var secondOldestEmployee = (from employees in employeeList orderby employees.Age descending select employees).ElementAt(1);

            var secondOldestEmployeeSalary = (from salary in salaryList where salary.EmployeeID == secondOldestEmployee.EmployeeID where salary.Type == SalaryType.Monthly select salary).First();
            Console.Write($"\n\n--The second oldest employee Details--\nEmployeeID = {secondOldestEmployee.EmployeeID}\nEmployeeFirstName = {secondOldestEmployee.EmployeeFirstName}\nEmployeeLastName = {secondOldestEmployee.EmployeeLastName}\nAge = {secondOldestEmployee.Age}\nTotal monthly salary = {secondOldestEmployeeSalary.Amount}\n\n\n");

        }
        

        public void Task3()
        {
            //Printing means of Monthly, Performance, Bonus salary of employees whose age is greater than 30-----------------
            var employeesAbove30 = from employees in employeeList where employees.Age > 30 join salary in salaryList on employees.EmployeeID equals salary.EmployeeID group salary by salary.Type into salaryGroup select new{
                salaryType = salaryGroup.First().Type,
                meanSalary = salaryGroup.Average(salary => salary.Amount)
            };
            Console.WriteLine("---Mean salary of each salary type of employees whose age is greater than 30---");
            foreach (var employee in employeesAbove30){
                Console.Write($"means of {employee.salaryType} salary = {employee.meanSalary}\n");
            }
        }
    }

    public enum SalaryType
    {
        Monthly,
        Performance,
        Bonus
    }

    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public int Age { get; set; }
    }

    public class Salary
    {
        public int EmployeeID { get; set; }
        public int Amount { get; set; }
        public SalaryType Type { get; set; }
    }
}
