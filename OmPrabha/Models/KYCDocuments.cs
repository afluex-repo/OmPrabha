using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace OmPrabha.Models
{
    public class KYCDocuments
    {
        public string PKUserID { get; set; }
        public string AdharNumber { get; set; }
        public string AdharImage { get; set; }
        public string AdharStatus { get; set; }
        public string PanNumber { get; set; }
        public string PanImage { get; set; }
        public string PanStatus { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentImage { get; set; }
        public string DocumentStatus { get; set; }

        public DataSet UploadKYCDocuments()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserID",PKUserID ) ,
                                      new SqlParameter("@AdharNumber", AdharNumber) ,
                                      new SqlParameter("@AdharImage", AdharImage) ,
                                      new SqlParameter("@PanNumber", PanNumber),
                                      new SqlParameter("@PanImage", PanImage) ,
                                      new SqlParameter("@DocumentNumber", DocumentNumber) ,
                                      new SqlParameter("@DocumentImage", DocumentImage)
                                  };
            DataSet ds = Connection.ExecuteQuery("UploadKYC", para);
            return ds;
        }

        public DataSet GetKYCDocuments()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserID",PKUserID )};
            DataSet ds = Connection.ExecuteQuery("GetKYCDocuments", para);
            return ds;
        }

    }
}