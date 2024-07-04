using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OmPrabha.Models
{
    public class ForgotPassword:Common
    {
        public string LoginID { get; set; }
        public string MobileNumber { get; set; }

        public DataSet ValidateData()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginID),
                                  new SqlParameter("@Mobile", MobileNumber)};
            DataSet ds =  Connection.ExecuteQuery("ForgotPassword", para);
            return ds;
        }
    }
}