using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace OmPrabha.Models
{
    public class Website : Common
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }

        public DataSet SaveContact()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Name",Name),
                new SqlParameter("@Email",Email),
                new SqlParameter("@Mobile",Mobile),
                new SqlParameter("@Address",Address)
            };
            DataSet ds = Connection.ExecuteQuery("SaveContact",para);
            return ds;
        }
        
    }
}