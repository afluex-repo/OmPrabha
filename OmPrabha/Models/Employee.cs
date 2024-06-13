using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OmPrabha.Models
{
    public class Employee : Common
    {
        public string Status { get; set; } 
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string JoiningDate { get; set; }
        public string Fk_UserTypeId { get; set; }
        public string EncryptKey { get; set; }
        public string Password { get; set; }
        public string UserTypeID { get; set; }
        public string UserTypeName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<Employee> EmpList { get; set; }
        public string Salary { get; set; }
        public string LoginId { get; set; }
        public string ProfilePic { get;  set; }
        public string Present { get; set; }
        public string Absent { get; set; }
        public string Salarydate { get; set; }
        public string Remark { get; set; }
        public DataSet UserTypeList()
        {

            DataSet ds = Connection.ExecuteQuery("UserTypeList");
            return ds;
        }
        public DataSet GetList()
        {
            SqlParameter[] para = { 
                                      
                                       new SqlParameter("@PK_AdminId", UserID) ,
                                     new SqlParameter("@Name", Name) ,
                                     new SqlParameter("@Fk_UserTypeId ", UserTypeID),
                                     new SqlParameter("@FromDate ", FromDate),
                                     new SqlParameter("@ToDate ", ToDate)
                                  };
            DataSet ds = Connection.ExecuteQuery("EmployeeList", para);
            return ds;
        }
        public DataSet EmployeeReg()
        {
            SqlParameter[] para = { 
                                      new SqlParameter("@Name", Name) ,
                                      new SqlParameter("@Mobile", Mobile) ,
                                      new SqlParameter("@Email", Email) ,
                                      new SqlParameter("@JoiningDate", JoiningDate) ,
                                      new SqlParameter("@Fk_UserTypeId", UserTypeID) ,
                                      new SqlParameter("@CreatedBy", AddedBy) ,
                                        new SqlParameter("@Salary", Salary) ,

                                  };
            DataSet ds = Connection.ExecuteQuery("EmployeeRegistration", para);
            return ds;
        }
        public DataSet UpdateEmployee()
        {
            SqlParameter[] para = { 
                                      new SqlParameter("@ID", UserID) ,
                                      new SqlParameter("@Name", Name) ,
                                      new SqlParameter("@Mobile", Mobile) ,
                                      new SqlParameter("@Email", Email) ,
                                      new SqlParameter("@JoiningDate", JoiningDate) ,
                                      new SqlParameter("@Fk_UserTypeId", UserTypeID) ,
                                      new SqlParameter("@UpdatedBy", AddedBy) ,
                                        new SqlParameter("@Salary", Salary) ,
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateEmployee", para);
            return ds;
        }


        public DataSet DeleteEmployee()
        {
            SqlParameter[] para = { 
                                        new SqlParameter("@PK_UserId", UserID) ,
                                       
                                      
                                      new SqlParameter("@DeletedBy", AddedBy) ,

                                  };
            DataSet ds = Connection.ExecuteQuery("DeleteEmployee", para);
            return ds;
        }

        public DataSet MarkPresent()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@FK_EmployeeID", UserID) ,
                                          new SqlParameter("@Status", Status) ,
                                            new SqlParameter("@AttendanceDate", ToDate) ,
                                      new SqlParameter("@AddedBy", AddedBy) ,

                                  };
            DataSet ds = Connection.ExecuteQuery("MarkPresentEmployee", para);
            return ds;
        }
        public DataSet AttendanceReport()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Name", Name) ,
                                        new SqlParameter("@Status", Status) ,
                                        new SqlParameter("@FromDate", FromDate) ,
                                        new SqlParameter("@ToDate", ToDate) ,

                                  };
            DataSet ds = Connection.ExecuteQuery("EmployeeAttendanceReport", para);
            return ds;
        }
        public DataSet AttendanceSummaryReport()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Name", Name) ,
                                        new SqlParameter("@Status", Status) ,
                                        new SqlParameter("@FromDate", FromDate) ,
                                        new SqlParameter("@ToDate", ToDate) ,

                                  };
            DataSet ds = Connection.ExecuteQuery("AttendanceSummary", para);
            return ds;
        }
        
        public DataSet GetAdminDetails()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@PK_AdminId", UserID) ,
                                       
                                  };
            DataSet ds = Connection.ExecuteQuery("GetAdminDetails", para);
            return ds;
        }
        public DataSet UpdateAdminDetails()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@PK_AdminId", UserID) ,
                                        new SqlParameter("@JoiningDate", JoiningDate) ,
                                        new SqlParameter("@Contact", Mobile) ,
                                        new SqlParameter("@Email", Email) ,
                                        new SqlParameter("@ProfilePic", ProfilePic) ,
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateAdminProfile", para);
            return ds;
        }
        public DataSet EmployeeSalary()
        {
            SqlParameter[] para = {
                                  
                                      new SqlParameter("@Remarks", Remark) ,
                                      new SqlParameter("@SalaryDate", Salarydate),
                                      new SqlParameter("@FK_EmpId", Fk_UserId),
                                      new SqlParameter("@CreatedBy", AddedBy) ,
                                        new SqlParameter("@Salary", Salary) ,
                                  };
            DataSet ds = Connection.ExecuteQuery("EmployeeSalary", para);
            return ds;
        }
        public DataSet GetEmpSalaryList()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@PK_EmpId", UserID) ,
                                     new SqlParameter("@Name", Name),
                                     new SqlParameter("@FromDate ", FromDate),
                                     new SqlParameter("@ToDate ", ToDate)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetEmpSalaryList", para);
            return ds;
        }
        
    }
}
 