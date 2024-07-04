using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OmPrabha.Models
{
    public class Setting:Common
    {
        public string LoginId { get; set; }
        public string Password { get; set; }

        #region ChangePassword

        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string UpdatedBy { get; set; }

        public DataSet UpdatePassword()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@OldPassword", Password) ,
                                      new SqlParameter("@NewPassword", NewPassword) ,
                                      new SqlParameter("@UpdatedBy", UpdatedBy) 
                                  };
            DataSet ds = Connection.ExecuteQuery("ChangePassword", para);
            return ds;
        }
        public DataSet UpdateAssociatePassword()
        {
            SqlParameter[] para = {
                                       new SqlParameter("@OldPassword", Password) ,
                                          new SqlParameter("@LoginId", LoginId) ,
                                      new SqlParameter("@NewPassword", NewPassword) ,
                                      new SqlParameter("@UpdatedBy", UpdatedBy) 
                                  };
            DataSet ds = Connection.ExecuteQuery("ChangeAssociatePasswordByAdmin", para);
            return ds;
        }

        #endregion 

        public DataSet Check()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginID", LoginId) 
                                      
                                  };
            DataSet ds = Connection.ExecuteQuery("CheckLoginIDForChangePassword", para);
            return ds;
        }
        
    }
}