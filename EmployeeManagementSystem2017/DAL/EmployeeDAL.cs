
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace DAL
{
  public  class EmployeeDAL
    {


        private string str = @"Data Source=SWAPNIL\SQLEXPRESS; Initial Catalog=New_CompTask; Integrated Security=True";

        public DataTable GetAllEmployees()
        {
            using (SqlConnection conn = new SqlConnection(str))
            {
                SqlCommand cmd = new SqlCommand("GetAllEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetEmployeeById(int EmpID)
        {
            using (SqlConnection conn = new SqlConnection(str))
            {
                SqlCommand cmd = new SqlCommand("GetEmployeeById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpID", EmpID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public int SaveEmployee(EmployeeModel model)
        {
            using (SqlConnection conn = new SqlConnection(str))
            {
                SqlCommand cmd = new SqlCommand("SaveEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Designation", model.Designation);
                cmd.Parameters.AddWithValue("@Date_Of_Birth", model.Date_Of_Birth);
                cmd.Parameters.AddWithValue("@Date_Of_Joining", model.Date_Of_Joining);
                cmd.Parameters.AddWithValue("@Salary", model.Salary);
                cmd.Parameters.AddWithValue("@Gender", model.Gender);
                cmd.Parameters.AddWithValue("@StateID", model.StateID);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                conn.Close();

                return result;
            }
        }

        public int UpdateEmployee(int EmpID, EmployeeModel model)
        {
            using (SqlConnection conn = new SqlConnection(str))
            {
                SqlCommand cmd = new SqlCommand("EditEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpID", EmpID);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Designation", model.Designation);
                cmd.Parameters.AddWithValue("@Date_Of_Birth", model.Date_Of_Birth);
                cmd.Parameters.AddWithValue("@Date_Of_Joining", model.Date_Of_Joining);
                cmd.Parameters.AddWithValue("@Salary", model.Salary);
                cmd.Parameters.AddWithValue("@Gender", model.Gender);
                cmd.Parameters.AddWithValue("@StateID", model.StateID);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                conn.Close();

                return result;
            }
        }

        public int DeleteEmployee(int EmpID)
        {

            SqlConnection conn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("DeleteEmployee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpID", EmpID);

            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();

            return result;

        }

        public DataTable GetAllStates()
        {
            using (SqlConnection conn = new SqlConnection(str))
            {
                SqlCommand cmd = new SqlCommand("SELECT StateID, StateName FROM State", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public bool CheckDuplicateName(string name, int empID)
        {
            using (SqlConnection conn = new SqlConnection(str))
            {
               
                string query = "SELECT COUNT(*) FROM Employees WHERE Name = @Name AND EmpID != @EmpID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@EmpID", empID);

                conn.Open();
                int count = (int)cmd.ExecuteScalar(); 
                conn.Close();

                return count > 0; 
            }
        }

       
    }
}

