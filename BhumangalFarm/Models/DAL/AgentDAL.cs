using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using System.Linq;
using OmPrabha.Models;

namespace OmPrabha.DAL
{
    public class AgentDAL
    {
        public string ProductCode { get; set; }

        public DataTable GetTreeMembers(AgentModel Obj)
        {
            SqlParameter[] para ={
                                    new SqlParameter("@headID",Obj.Fk_UserId)
                                };
            Connection db = new Connection();
            DataSet ds = Connection.ExecuteQuery("GetTreeMembers", para);
            return ds.Tables[0];


        }
        public DataTable GetTreeMembersForNew(AgentModel Obj)
        {
            SqlParameter[] para ={
                                    new SqlParameter("@headID",Obj.Fk_UserId)
                                };
            Connection db = new Connection();
            DataSet ds = Connection.ExecuteQuery("GetTreeMembersForSilver", para);
            return ds.Tables[0];


        }
        public DataTable GetTreeMembersForAdmin(AgentModel Obj)
        {
            SqlParameter[] para ={
                                    new SqlParameter("@headID",Obj.Fk_UserId)
                                };
            Connection db = new Connection();
            DataSet ds = Connection.ExecuteQuery("GetTreeMembersForAdmin", para);
            return ds.Tables[0];


        }
        public DataTable GetUserFirst(AgentModel Obj)
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Fk_UserId",Obj.Fk_UserId)
                                };
            Connection db = new Connection();
            DataSet ds = Connection.ExecuteQuery("GetUserFirst", para);
            return ds.Tables[0];


        }
        public DataTable GetUserFirstForAdmin(AgentModel Obj)
        {
            SqlParameter[] para ={
                                    new SqlParameter("@Fk_UserId",Obj.Fk_UserId)
                                };
            Connection db = new Connection();
            DataSet ds = Connection.ExecuteQuery("GetUserFirstforAdmin", para);
            return ds.Tables[0];


        }
    }
}