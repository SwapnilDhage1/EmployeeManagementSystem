using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class EmployeeModel
    {
        public int EmpID { get; set; }

        public string Name { get; set; }

        public string Designation { get; set; }

        public DateTime Date_Of_Birth { get; set; }

        public DateTime Date_Of_Joining { get; set; }

        public decimal Salary { get; set; }

        public string Gender { get; set; }

        public int StateID { get; set; }


    }
    public class State
    {
        public int StateID { get; set; }
        public string StateName { get; set; }

    }
    public class CheckNameDto
    {
        public string Name { get; set; }
        public int EmpID { get; set; } // EmpID is used to exclude the current employee from the duplicate check during an update
    }
}
