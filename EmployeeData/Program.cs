using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProperties;
using System.Configuration;

namespace EmployeeData
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeOperations employee = new EmployeeOperations();

            char ch = 'Y';
            do
            {
                Console.WriteLine("1.Add Employee Details(Id,Name,Salary,Age)\n2.Display All Employee details\n3.Remove Employee By Id (or) Name\n4.Display details by Id (or) Name\n5.Update Details by Id\n6.Exit\nSelect One Option : ");
                int userchoice = Convert.ToInt32(Console.ReadLine());
                switch (userchoice)
                {
                    case 1:
                        employee.AddEmployeeData();
                        break;
                    case 2:
                        employee.GetAllEmployeeData();
                        break;
                    case 3:
                        employee.RemoveEmployee();
                        break;
                    case 4:
                        employee.GetDataByIdOrName();
                        break;
                    case 5:
                        employee.UpdateDetailsById();
                        break;
                    case 6:// Environment.Exit(1);
                        return ;
                    default:
                        Console.WriteLine("Oops!Invalid option!!Enter correct option...");
                        break;
                }
                Console.WriteLine("*************************************************************************************");
            } while (ch == 'Y');
            Console.ReadLine();
        }
    }
}
