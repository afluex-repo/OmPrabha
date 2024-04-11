using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OmPrabha.Models
{
    public class Permisssions
    {
        public string Fk_UserTypeId { get; set; }
        public string Fk_UserId { get; set; }
        public string Fk_FormTypeId { get; set; }
        public string Fk_FormId { get; set; }
        public string FormView { get; set; }
        public string FormSave { get; set; }
        public string FormUpdate { get; set; }
        public string FormDelete { get; set; }
        public string FormName { get; set; }

        public bool IsSaveValue { get; set; }
        public bool IsUpdateValue { get; set; }
        public bool IsSelectValue { get; set; }
        public bool IsDeleteValue { get; set; }
        public string SelectedValue { get; set; }
        public string CreatedBy { get; set; }
        public DataTable UserTypeFormPermisssion { get; set; }
        public List<Permisssions> lstpermission = new List<Permisssions>();
        
        public DataSet GetFormPermission()
        {
            SqlParameter[] para = { 
                                      new SqlParameter("@fk_userid", Fk_UserId),
                                      new SqlParameter("@Pk_FormTypeId", Fk_FormTypeId) };
            DataSet ds = Connection.ExecuteQuery("GetPemissionData", para);
            return ds;
        }

        public DataSet SavePermisssion()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@UserTypeFormPermisssion", UserTypeFormPermisssion), 
                                      new SqlParameter("@CreatedBy", CreatedBy),
                                      new SqlParameter("@Fk_UserId", Fk_UserId),
                                      new SqlParameter("@Fk_FormTypeId", Fk_FormTypeId)
                                  };
            DataSet ds = Connection.ExecuteQuery("SetFormPermission", para);
            return ds;
        }
       
    }
}