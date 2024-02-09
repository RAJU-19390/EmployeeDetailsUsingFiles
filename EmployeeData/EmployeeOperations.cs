using System;
using System.Collections.Generic;
using EmployeeProperties;
using System.IO;
using System.Linq;
using System.Configuration;

namespace EmployeeData
{
    public class EmployeeOperations : IEmployee
    {
        private string FilePath = ConfigurationManager.AppSettings["MyFilePath"];
        public List<Employee> employees;
        public EmployeeOperations() 
        {  
           employees = new List<Employee>(); 
        }
        public void AddEmployeeData()
        {
           while(true)
            {
                Console.WriteLine("Enter Employee ID : ");
                int eid = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Employee Name : ");
                string ename = Console.ReadLine();
                Console.WriteLine("Enter Employee Salary : ");
                float esalary = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine("Enter Employee Age : ");
                int eage = Convert.ToInt32(Console.ReadLine());
                Employee newemployee = new Employee{EmployeeId=eid,EmployeeName=ename,EmployeeSalary=esalary,EmployeeAge=eage};
                employees.Add(newemployee);
                using (StreamWriter writer = new StreamWriter(FilePath, true))
                {
                    writer.WriteLine($"{newemployee.EmployeeId},{newemployee.EmployeeName},{newemployee.EmployeeSalary},{newemployee.EmployeeAge}");
                    Console.WriteLine($"{newemployee.EmployeeName} added to file successfully.");
                }
                Console.Write("Do you want to add another employee? (Y/N): ");
                string userInput = Console.ReadLine().Trim();

                if (userInput.ToUpper() != "Y")
                {
                    break;
                }
            }
        }
        public List<Employee> ReadFromFile()
        {
            List<Employee> EmployeeDataFromFile = new List<Employee>();
            try
            {
                using (StreamReader reader = new StreamReader(FilePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] words = line.Split(',');

                        int eid = int.Parse(words[0]);
                        string ename = words[1];
                        float esalary = float.Parse(words[2]);
                        int eage = int.Parse(words[3]);

                       Employee employee = new Employee { EmployeeId = eid, EmployeeName = ename, EmployeeSalary = esalary, EmployeeAge = eage };
                        EmployeeDataFromFile.Add(employee);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading from file: {ex.Message}");
            }

            return EmployeeDataFromFile;
        }
        public void GetAllEmployeeData()
        {
            if (File.Exists(FilePath))
            {
                employees = ReadFromFile();
                if (employees.Count > 0)
                {
                    foreach (var employee in employees)
                    {
                        Console.WriteLine($"Employee ID: {employee.EmployeeId} | Employee Name: {employee.EmployeeName} | Employee Salary: {employee.EmployeeSalary} | Employee Age: {employee.EmployeeAge}");
                    }
                }
                else
                {
                    Console.WriteLine("No employee details found.");
                }
            }
        }
        public void RemoveEmployee()
        {
            Console.WriteLine("1.Remove Employee by Id\n2.Remove Employee by Name\nSelect One Option : ");
            int userchoice = Convert.ToInt32(Console.ReadLine());
            switch (userchoice)
            {
                case 1:
                    Console.WriteLine("Enter employee id to remove :");
                    int idtoremove = Convert.ToInt32(Console.ReadLine());
                    Employee removebyid = employees.Find(p => p.EmployeeId == idtoremove);
                    if (removebyid != null)
                    {
                        employees.Remove(removebyid);
                        Console.WriteLine($"Employee {removebyid.EmployeeName} removed.");
                        WriteToFile(employees);
                    }
                    else
                    {
                        Console.WriteLine("Employee is not found.");
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter employee name to remove :");
                    string nametoremove = Console.ReadLine();
                    List<Employee> removebyname = employees.Where(p => p.EmployeeName.Equals(nametoremove, StringComparison.OrdinalIgnoreCase)).ToList();
                    if (removebyname.Count > 0)
                    {
                        employees.RemoveAll(p => p.EmployeeName.Equals(nametoremove, StringComparison.OrdinalIgnoreCase));

                        foreach (var removedEmployee in removebyname)
                        {
                            Console.WriteLine($"Employee {removedEmployee.EmployeeName} removed.");
                        }
                        WriteToFile(employees);
                    }
                    else
                    {
                        Console.WriteLine("Employee not found.");
                    }
                    break;
                default:
                    Console.WriteLine("Oops!Invalid option!!Enter correct option...");
                    break;
            }
        }

        public void GetDataByIdOrName()
        {
            Console.WriteLine("1.Display Employee details by Id\n2.Display Employee details by Name\nSelect One Option : ");
            int userchoice = Convert.ToInt32(Console.ReadLine());
            switch(userchoice)
            {
                case 1:
                    Console.WriteLine("Enter employee id to get details :");
                    int idtoshow = Convert.ToInt32(Console.ReadLine());
                    Employee employeebyid = employees.Find(p => p.EmployeeId == idtoshow);
                    if (employeebyid != null)
                    {
                        Console.WriteLine($"Employee ID: {employeebyid.EmployeeId} | Employee Name: {employeebyid.EmployeeName} | Employee Salary: {employeebyid.EmployeeSalary} | Employee Age: {employeebyid.EmployeeAge}");
                    }
                    else
                    {
                        Console.WriteLine("Employee is not found.");
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter employee name to get details :");
                    string nametoshow = Console.ReadLine();
                    List<Employee> employeebyname = employees.Where(p => p.EmployeeName.Equals(nametoshow, StringComparison.OrdinalIgnoreCase)).ToList();

                    if (employeebyname.Count > 0)
                    {
                        foreach (var employee in employeebyname)
                        {
                            Console.WriteLine($"Employee ID: {employee.EmployeeId} | Employee Name: {employee.EmployeeName} | Employee Salary: {employee.EmployeeSalary} | Employee Age: {employee.EmployeeAge}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Employee not found.");
                    }
                    break;
                default:
                    Console.WriteLine("Oops!Invalid option!!Enter correct option...");
                    break;
            }
        }

        public void WriteToFile(List<Employee> employeedata)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    foreach (var employee in employeedata)
                    {
                        writer.WriteLine($"{employee.EmployeeId},{employee.EmployeeName},{employee.EmployeeSalary},{employee.EmployeeAge}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
            }
        }
        public void UpdateDetailsById()
        {
            Console.Write("Enter Employee ID to update details: ");
            int IdToUpdate = Convert.ToInt32(Console.ReadLine());
            List<Employee> EmployeeDataFromFile = ReadFromFile();
            Employee EmployeeToUpdate = EmployeeDataFromFile.Find(t => (t.EmployeeId == IdToUpdate));
            if (EmployeeToUpdate != null)
            {
                Console.Write("Enter new employee name: ");
                string newname = Console.ReadLine();
                Console.Write("Enter new employee salary: ");
                double newsalary = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter new employee age: ");
                int newage = Convert.ToInt32(Console.ReadLine());
                EmployeeToUpdate.EmployeeName = newname;
                EmployeeToUpdate.EmployeeSalary = newsalary;
                EmployeeToUpdate.EmployeeAge = newage;
                WriteToFile(EmployeeDataFromFile);
                Console.WriteLine($"Employee ID : {EmployeeToUpdate.EmployeeId} details updated successfully!");
            }
            else
            {
                Console.WriteLine("The Given ID is not found in Employee database.");
            }
        }
    }
}
