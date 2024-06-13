using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OmPrabha.Models
{
    public class EmployeeRegistrations : Common
    {
        public string JoiningDate { get; set; }
        public string PK_AdminID { get; set; }
        [Required]
        [Display(Name = "User Type")]
        public string UserType { get; set; }

        [Required]
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }

        [Required]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Display(Name = "DOB")]
        public string DOB { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Father's Name")]
        public string FathersName { get; set; }

        [Display(Name = "EducationQualification")]
        public string EducationQualififcation { get; set; }

        public string Fk_UserTypeId { get; set; }
        public string Fk_BranchId { get; set; }
        public string CreatedBy { get; set; }
        public string Message { get; set; }

        [NotMapped]
        public List<EmployeeRegistrations> lstemp { get; set; }
        public string LoginId { get; set; }
        
        public DataSet SaveEmpoyeeData()
        {
            SqlParameter[] para = { 
            new SqlParameter("@Name", Name),
            new SqlParameter("@Mobile", Mobile),
            new SqlParameter("@Email", Email),
            new SqlParameter("@Address", Address),
            new SqlParameter("@DOB", DOB),
            new SqlParameter("@Qualification", EducationQualififcation),
            new SqlParameter("@FathName", FathersName),      
            new SqlParameter("@Fk_UserTypeId", Fk_UserTypeId),  
            new SqlParameter("@Fk_BranchId", Fk_BranchId),  
            new SqlParameter("@CreatedBy", CreatedBy),  
            };
            DataSet ds = Connection.ExecuteQuery("EmployeeRegistration", para);
            return ds;
        }

        public DataSet GetEmployeeData()
        {
            SqlParameter[] para = { new SqlParameter("@PK_AdminId", PK_AdminID),
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@LoginId", LoginId) };

            DataSet ds = Connection.ExecuteQuery("GetEmployeeDetails", para);
            return ds;
        }

        public DataSet DeleteEmployee()
        {
            SqlParameter[] para = { new SqlParameter("@PK_AdminId", PK_AdminID),
                                    new SqlParameter("@DeletedBy", AddedBy) };

            DataSet ds = Connection.ExecuteQuery("DeleteEmployee", para);
            return ds;
        }

        public DataSet UpdateEmployee()
        {
            SqlParameter[] para = { new SqlParameter("@PK_AdminID", PK_AdminID),
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@Contact", Mobile),
                                    new SqlParameter("@Email", Email),
                                    new SqlParameter("@Qualification", EducationQualififcation), 
                                    new SqlParameter("@FK_UserTypeID", Fk_UserTypeId), 
                                    new SqlParameter("@UpdatedBy", AddedBy),  
                                    };
            DataSet ds = Connection.ExecuteQuery("UpdateEmployee", para);
            return ds;
        }

    }
}