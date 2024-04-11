using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OmPrabha.Models
{
    public class Transaction : Common
    {

        public string LoginID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string PAN { get; set; }
        public string SearchBy { get; set; }
        public string Search { get; set; }
        public string EncryptKey { get; set; }
        public string UserID { get; set; }
        public string AssociateName { get; set; }
        public string Contact { get; set; }
        public string PanNo { get; set; }
        public List<Transaction> lstTrad { get; set; }

        public DataSet Login()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@LoginId",LoginID),
                             
                            };
            DataSet ds = Connection.ExecuteQuery("LoginAsAssociate", para);
            return ds;
        }

        public DataSet GetList()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@SearchBy",SearchBy),
                              new SqlParameter("@SearchCriteria",Search),
                            };
            DataSet ds = Connection.ExecuteQuery("GetDetailsForAssociateLogin", para);
            return ds;
        }
        public DataSet GetList2()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@SearchBy",SearchBy),
                              new SqlParameter("@SearchCriteria",Search),
                            };
            DataSet ds = Connection.ExecuteQuery("GetDetailsForCustomerLogin", para);
            return ds;
        }
        public DataSet Login2()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@LoginId",LoginID),

                            };
            DataSet ds = Connection.ExecuteQuery("LoginCustomer", para);
            return ds;
        }
        
    }
}