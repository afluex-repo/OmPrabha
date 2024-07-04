using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OmPrabha.Models
{
    public class Connection
    {
        public static string connectionString = string.Empty; 

        static Connection()
        {
            try
            {
                //connectionString = "Data Source=101.53.150.222,1440;Initial Catalog=OmPrabhaDB; User Id=sa; Password=Fx1479LVAPbF; Integrated Security=false;";

                connectionString = "Data Source=101.53.150.222,1440;Initial Catalog=OmPrabhaDBTest_1June2024; User Id=sa; Password=Fx1479LVAPbF; Integrated Security=false;";




                //connectionString = "Data Source=103.48.51.111,1232;Initial Catalog= OmPrabhaDB; User Id=sa; Password=hytr895#$!^poi(254;Integrated Security=false;";

            }
            catch (Exception) 
            {
                throw;  
            }
        }
        public static int ExecuteNonQuery(string commandText, params SqlParameter[] commandParameters)
        {
            int k = 0;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(commandParameters);
                    connection.Open();
                    k = command.ExecuteNonQuery();
                }
                return k;
            }
            catch
            {
                return k;
            }
        }
        public static DataSet ExecuteQuery(string commandText, params SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(ds);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Msg");
                dt.Columns.Add("ErrorMessage");

                DataRow dr = dt.NewRow();
                dr["Msg"] = "0";
                dr["ErrorMessage"] = ex.Message;
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);
            }
            return ds;
        }
    }
}



