using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
using System.Data;

namespace BAL
{
    public class EmployeeBAL
    {
        EmployeeDAL dal = new EmployeeDAL();
        public bool CheckDuplicateName(string name, int empID)
        {
            return dal.CheckDuplicateName(name, empID);
        }
        public DataTable GetAllEmployees()
        {
            return dal.GetAllEmployees();
        }
        public DataTable GetEmployeeById(int EmpID)
        { return dal.GetEmployeeById(EmpID); }
        public int SaveEmployee(EmployeeModel model)
        {
            return dal.SaveEmployee(model);
        }
        public int UpdateEmployee(int EmpID, EmployeeModel model)
        {
            return dal.UpdateEmployee(EmpID, model);
        }
        public int DeleteEmployee(int EmpID)
        {
            return dal.DeleteEmployee(EmpID);
        }
        public DataTable GetAllStates()
        {
            return dal.GetAllStates();
        }
    }
}
