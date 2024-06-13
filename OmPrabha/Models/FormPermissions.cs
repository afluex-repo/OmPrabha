using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace OmPrabha.Models
{
    public class FormPermissions:Common
    {
        Common objDFP = new Common();
        DataSet DsPer = new DataSet();

        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }

        public FormPermissions(string FormName)
        {
            string UserID = (HttpContext.Current.Session["Pk_AdminId"] != null) ? HttpContext.Current.Session["Pk_AdminId"].ToString() : "-1";
            bool IsPermissions = (System.Configuration.ConfigurationSettings.AppSettings["Permission"].ToString() != "No");

            //if((!Sessions.UserType.ToLower().Contains("crm-admin")) && (!IsPermissions ||  Sessions.UserType.ToLower().Contains("admin") || Sessions.UserType.ToLower().Contains("super") || Sessions.UserType.ToLower().Contains("agent") || Sessions.UserType.ToLower().Contains("investor")))
            //{
            //    IsView = true;
            //    IsAdd = true;
            //    IsUpdate = true;
            //    IsDelete = true;
            //}
            //else if (Sessions.PermissionDBSet != null && Sessions.PermissionDBSet.Rows.Count > 0)
            //{
            //    DataTable DtPermission = Sessions.PermissionDBSet;

            //    if (DtPermission.Select("FormName = '" + FormName + "'").Length > 0)
            //    {
            //        IsView = (DtPermission.Select("FormName = '" + FormName + "' and FormView = 1").Length > 0);
            //        IsAdd = (DtPermission.Select("FormName = '" + FormName + "' and FormSave = 1").Length > 0);
            //        IsUpdate = (DtPermission.Select("FormName = '" + FormName + "' and FormUpdate = 1").Length > 0);
            //        IsDelete = (DtPermission.Select("FormName = '" + FormName + "' and FormDelete = 1").Length > 0);
            //    }
            //    else
            //    {
            //        IsView = false;
            //        IsAdd = false;
            //        IsUpdate = false;
            //        IsDelete = false;
            //    }
            //}
            //else
            //{
            //    DsPer = objDFP.FormPermissions(FormName, UserID);
            //    if (DsPer != null && DsPer.Tables.Count > 0 && DsPer.Tables[0].Rows.Count > 0)
            //    {
            //        string[] strP = DsPer.Tables[0].Rows[0]["UserAccessPermission"].ToString().Trim().Split(',');
            //        IsView = (strP[0] == "Y");
            //        IsAdd = (strP[1] == "Y"); ;
            //        IsUpdate = (strP[2] == "Y");
            //        IsDelete = (strP[3] == "Y");
            //    }
            //    else
            //    {
            //        IsView = false;
            //        IsAdd = false;
            //        IsUpdate = false;
            //        IsDelete = false;
            //    }
        
            //}

           
        }
        
    }
}
