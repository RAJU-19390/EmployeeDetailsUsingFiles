using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeData
{
   public interface IEmployee
    {
        void AddEmployeeData();
            void GetAllEmployeeData();
            void RemoveEmployee();
            void GetDataByIdOrName();
            void UpdateDetailsById();
    }
}
