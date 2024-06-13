using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace OmPrabha.Models
{
    public class Topup : Common
    {
        public string LoginID { get; set; }
        public string EPinNo { get; set; }
        public string Status { get; set; }
        public List<SelectListItem> ddlEPin { get; set; }

        public DataSet GetEPinList()
        {
            SqlParameter[] para = { new SqlParameter("@Status", Status),
                                  new SqlParameter("@Fk_UserId", Fk_UserId) };
            DataSet ds = Connection.ExecuteQuery("GetUnusedUsedPinsTraditional", para);
            return ds;
        }

        public DataSet TopupByEpin()
        {
            SqlParameter[] para = { new SqlParameter("@PK_EpinID", EPinNo),
                                  new SqlParameter("@TopupID", Fk_UserId),
                                  new SqlParameter("@CreatedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("TopupByEpinTraditional", para);
            return ds;
        }

    }
}