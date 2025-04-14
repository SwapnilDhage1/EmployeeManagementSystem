using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
using BAL;
using Model;
using Microsoft.Reporting.WebForms;
using System.Web.Http.Cors;

namespace EmployeeManagementSystem2017.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployeeController : ApiController
    {
        EmployeeBAL employeeBAL = new EmployeeBAL();

        

        [HttpGet]
        [Route("api/employee/GetAllEmployees")]
        public IHttpActionResult GetAllEmployees()
        {
            DataTable dt = employeeBAL.GetAllEmployees();
            List<EmployeeModel> lst = new List<EmployeeModel>();

            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new EmployeeModel
                {
                    EmpID = Convert.ToInt32(dr["EmpID"]),
                    Name = dr["Name"].ToString(),
                    Designation = dr["Designation"].ToString(),
                    Date_Of_Birth = Convert.ToDateTime(dr["Date_Of_Birth"]),
                    Date_Of_Joining = Convert.ToDateTime(dr["Date_Of_Joining"]),
                    Salary = Convert.ToDecimal(dr["Salary"]),
                    Gender = dr["Gender"].ToString(),
                    StateID = Convert.ToInt32(dr["StateID"])
                });
            }

            return Ok(lst);
        }

        [HttpGet]
        [Route("api/employee/GetEmployeeById/{EmpID}")]
        public IHttpActionResult GetEmployeeById(int EmpID)
        {
            DataTable dt = employeeBAL.GetEmployeeById(EmpID);
            if (dt.Rows.Count == 0)
                return NotFound();

            var row = dt.Rows[0];
            EmployeeModel employee = new EmployeeModel
            {
                EmpID = Convert.ToInt32(row["EmpID"]),
                Name = row["Name"].ToString(),
                Designation = row["Designation"].ToString(),
                Date_Of_Birth = Convert.ToDateTime(row["Date_Of_Birth"]),
                Date_Of_Joining = Convert.ToDateTime(row["Date_Of_Joining"]),
                Salary = Convert.ToDecimal(row["Salary"]),
                Gender = row["Gender"].ToString(),
                StateID = Convert.ToInt32(row["StateID"])
            };

            return Ok(employee);
        }

        [HttpPost]
        [Route("api/employee/SaveEmployee")]
        public IHttpActionResult SaveEmployee(EmployeeModel model)
        {
            DataTable dt = employeeBAL.GetAllEmployees();
            bool exists = dt.AsEnumerable()
                .Any(row => row.Field<string>("Name") == model.Name);

            if (exists)
            {
                return Content(HttpStatusCode.Conflict, "Employee name already exists.");
            }

            int result = employeeBAL.SaveEmployee(model);
            return Ok(result);
        }

        [HttpPut]
        [Route("api/employee/UpdateEmployee/{EmpID}")]
        public IHttpActionResult UpdateEmployee(int EmpID, EmployeeModel model)
        {
            int res = employeeBAL.UpdateEmployee(EmpID, model);
            return Ok(res);
        }

        [HttpDelete]
        [Route("api/employee/DeleteEmployee/{EmpID}")]
        public IHttpActionResult DeleteEmployee(int EmpID)
        {
            int result = employeeBAL.DeleteEmployee(EmpID);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/employee/GetStates")]
        public IHttpActionResult GetAllStates()
        {
            DataTable dt = employeeBAL.GetAllStates();
            List<State> states = new List<State>();

            foreach (DataRow dr in dt.Rows)
            {
                states.Add(new State
                {
                    StateID = Convert.ToInt32(dr["StateID"]),
                    StateName = dr["StateName"].ToString()
                });
            }

            return Ok(states);
        }

        [HttpPost]
        [Route("api/employee/CheckDuplicateName")]
        public IHttpActionResult CheckDuplicateName([FromBody] CheckNameDto dto)
        {
            try
            {
                bool isDuplicate = employeeBAL.CheckDuplicateName(dto.Name, dto.EmpID);
                return Ok(!isDuplicate);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/employee/GenerateReport")]
        public HttpResponseMessage GenerateReport()
        {
            try
            {
               
                DataTable employeeDt = employeeBAL.GetAllEmployees();
                DataTable stateDt = employeeBAL.GetAllStates();

                
                DataTable reportDt = new DataTable();
                reportDt.Columns.Add("EmpID", typeof(int));
                reportDt.Columns.Add("Name", typeof(string));
                reportDt.Columns.Add("Designation", typeof(string));
                reportDt.Columns.Add("Date_Of_Joining", typeof(DateTime));
                reportDt.Columns.Add("Salary", typeof(decimal));
                reportDt.Columns.Add("Gender", typeof(string));
                reportDt.Columns.Add("StateName", typeof(string));

                foreach (DataRow empRow in employeeDt.Rows)
                {
                    var stateRow = stateDt.AsEnumerable()
                        .FirstOrDefault(s => s.Field<int>("StateID") == empRow.Field<int>("StateID"));

                    reportDt.Rows.Add(
                        empRow["EmpID"],
                        empRow["Name"],
                        empRow["Designation"],
                        empRow["Date_Of_Joining"],
                        empRow["Salary"],
                        empRow["Gender"],
                        stateRow?["StateName"]?.ToString() ?? "Unknown"
                    );
                }

               
                var reportPath = HostingEnvironment.MapPath("~/Reports/EmployeeReport.rdlc");

                
                LocalReport localReport = new LocalReport();
                localReport.ReportPath = reportPath;
                localReport.DataSources.Clear();
                localReport.DataSources.Add(new ReportDataSource("EmployeeDataset", reportDt));

                
                string mimeType;
                string encoding;
                string fileNameExtension;
                string[] streams;
                Warning[] warnings;

                byte[] renderedBytes = localReport.Render(
                    "PDF", 
                    null,  
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings
                );

                // Return the PDF as a response
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(renderedBytes)
                };
                response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = "EmployeeReport.pdf"
                };
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");

                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
    }
