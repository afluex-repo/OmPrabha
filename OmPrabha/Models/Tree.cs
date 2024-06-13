using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace OmPrabha.Models
{
    public class Tree : Common
    {
        public string LoginId { get;  set; }
        public string RootAgentCode { get;  set; }

        public DataSet GetLevelTreeData()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@AgentCode", LoginId),
                                      new SqlParameter("@RootAgentCode", RootAgentCode),
                                    
            };

            DataSet ds = Connection.ExecuteQuery("LevelTree", para);
            return ds;
        }
    }
}