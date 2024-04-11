using OmPrabha.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;


namespace OmPrabha
{
    public static class Sessions
    {
        #region PK_UserID
        public static string PK_UserID
        {
            get
            {
                string PKID = "0";
                if (HttpContext.Current.Session["Pk_AdminId"] != null)
                    PKID = HttpContext.Current.Session["Pk_AdminId"].ToString().ToLower();
                else if (HttpContext.Current.Session["MemID"] != null)
                    PKID = HttpContext.Current.Session["MemID"].ToString().ToLower();

                return PKID;
            }
            set { PK_UserID = value; }
        }
        #endregion

        #region UserType
        public static string UserType
        {
            get { return (HttpContext.Current.Session["UserTypeName"] != null) ? HttpContext.Current.Session["UserTypeName"].ToString() : ""; }
            set { HttpContext.Current.Session["UserTypeName"] = value; }
        }
        #endregion

        #region IsCRMUser
        public static bool IsCRMUser
        {
            get { return (HttpContext.Current.Session["IsCRMUserValue"] != null && HttpContext.Current.Session["IsCRMUserValue"].ToString() == "1"); }
        }
        #endregion

        #region IsCRMUserValue
        public static string IsCRMUserValue
        {
            get { return (HttpContext.Current.Session["IsCRMUserValue"] != null) ? HttpContext.Current.Session["IsCRMUserValue"].ToString() : ""; }
            set { HttpContext.Current.Session["IsCRMUserValue"] = value; }
        }
        #endregion

        #region BranchID
        public static string BranchID
        {
            get { return (HttpContext.Current.Session["BranchID"] != null) ? HttpContext.Current.Session["BranchID"].ToString() : "0"; }
            set { HttpContext.Current.Session["BranchID"] = value; }
        }
        #endregion

        #region BranchCode
        public static string BranchCode
        {
            get { return (HttpContext.Current.Session["BranchCode"] != null) ? HttpContext.Current.Session["BranchCode"].ToString() : ""; }
            set { HttpContext.Current.Session["BranchCode"] = value; }
        }
        #endregion

        #region BranchName
        public static string BranchName
        {
            get { return (HttpContext.Current.Session["BranchName"] != null) ? HttpContext.Current.Session["BranchName"].ToString() : ""; }
            set { HttpContext.Current.Session["BranchName"] = value; }
        }
        #endregion

        #region MasterPage
        public static string MasterPage
        {
            get
            {
                string UserType = (HttpContext.Current.Session["UserType"] != null) ? HttpContext.Current.Session["UserType"].ToString().ToLower() : "Anonymous";
                bool IsSearchedMember = (HttpContext.Current.Session["SearchLoginId"] != null);
                UserType = UserType + (IsSearchedMember ? "-searched" : "");

                string RetMasterPage = "";

                if (IsCRMUser && IsSearchedMember == false) RetMasterPage = "~/CRMAdmin/BeyondAdmin.Master";//BeyondAdmin   CRMAdmin
                else
                {
                    switch (UserType)
                    {

                        case "crm-admin-searched":
                            RetMasterPage = "~/Admin/BeyondUtsav VikasSearchMaster.master";
                            break;

                        case "crm-superadmin-searched":
                            RetMasterPage = "~/Admin/BeyondUtsav VikasSearchMaster.master";
                            break;

                        case "admin-searched":
                            RetMasterPage = "~/Admin/BeyondUtsav VikasSearchMaster.master";
                            break;
                        case "super-searched":
                            RetMasterPage = "~/Admin/BeyondUtsav VikasSearchMaster.master";
                            break;
                        case "manager-searched":
                            RetMasterPage = "~/Admin/BeyondUtsav VikasSearchMaster.master";
                            break;
                        case "operation-searched":
                            RetMasterPage = "~/Admin/BeyondUtsav VikasSearchMaster.master";
                            break;
                        case "super":
                            RetMasterPage = "~/Admin/BeyondUtsav VikasAdminMaster.Master";
                            break;
                        case "adminUtsav Vikas":
                            RetMasterPage = "~/HomeMasterPage.Master";
                            break;
                        case "level-admin":
                            RetMasterPage = "~/levelAdmin/BeyondUtsav VikasAdminMaster.Master";
                            break;
                        case "crm-admin":
                            RetMasterPage = "~/CRMAdmin/CRMAdmin.Master";//"~/Admin/AdminMasterPage.Master";
                            break;
                        case "operator":
                            RetMasterPage = "~/Account/AccountMaster.Master";
                            break;
                        case "operation":
                            RetMasterPage = "~/Account/AccountMaster.Master";
                            break;
                        case "account":
                            RetMasterPage = "~/Account/AccountMaster.Master";
                            break;
                        case "technical-operator":
                            RetMasterPage = "~/Account/AccountMaster.Master";
                            break;
                        case "manager":
                            RetMasterPage = "~/Account/AccountMaster.Master";
                            break;
                        case "agent":
                            RetMasterPage = "~/Utsav Vikas/Agent/BeyondUserMaster.Master";
                            break;
                        case "traditional-agent":
                            //RetMasterPage = "~/TAgent/UserMaster.Master";
                            RetMasterPage = "~/TAgent/BeyondTagentAdmin.Master";
                            break;
                        case "customer":
                            RetMasterPage = "~/Customer/CustomerMaster.Master";
                            break;
                        case "levelagent":
                            RetMasterPage = "~/LevelAgent/BeyondUserMaster.Master";
                            break;
                        default:
                            RetMasterPage = "~/HomeMasterPage.Master";
                            break;
                    }
                }

                return RetMasterPage;
            }
            set { MasterPage = value; }
        }
        #endregion

        #region WelcomePage
        public static string WelcomePage
        {
            get
            {
                string UserType = (HttpContext.Current.Session["UserType"] != null) ? HttpContext.Current.Session["UserType"].ToString().ToLower() : "Anonymous";
                bool IsSearchedMember = (HttpContext.Current.Session["SearchLoginId"] != null);
                UserType = UserType + (IsSearchedMember ? "-searched" : "");

                string RetWelcomePage = "";

                if (IsCRMUser) RetWelcomePage = "~/CRMAdmin/CRMDashboard.aspx";
                else
                {
                    switch (UserType)
                    {
                        case "admin-searched":
                            RetWelcomePage = "~/Admin/SearchMemberMaster.master";
                            break;
                        case "crm-admin-searched":
                            RetWelcomePage = "~/Admin/SearchMaster.aspx";
                            break;
                        case "super-searched":
                            RetWelcomePage = "~/Admin/SearchMaster.aspx";
                            break;
                        case "manager-searched":
                            RetWelcomePage = "~/Admin/SearchMaster.aspx";
                            break;
                        case "operation-searched":
                            RetWelcomePage = "~/Admin/SearchMaster.aspx";
                            break;
                        case "super":
                            RetWelcomePage = "~/Admin/AdminMainMenu.aspx";
                            break;
                        case "admin":
                            RetWelcomePage = "~/Admin/AdminUtsav VikasMasterPage.Master";
                            break;
                        case "crm-admin":
                            RetWelcomePage = "~/Admin/AdminMasterPage.Master";
                            break;
                        case "operator":
                            RetWelcomePage = "~/Account/AccountWelcomePage.aspx";
                            break;
                        case "operation":
                            RetWelcomePage = "~/Account/AccountMaster.Master";
                            break;
                        case "account":
                            RetWelcomePage = "~/Account/AccountWelcomePage.aspx";
                            break;
                        case "technical-operator":
                            RetWelcomePage = "~/Account/AccountWelcomePage.aspx";
                            break;
                        case "manager":
                            RetWelcomePage = "~/Account/AccountWelcomePage.aspx";
                            break;
                        case "agent":
                            RetWelcomePage = "~/Agent/UserPanel.aspx";
                            break;
                        case "traditional-agent":
                            RetWelcomePage = "~/TAgent/AgentWelcomePage.aspx";
                            break;
                        case "investor":
                            RetWelcomePage = "~/Investor/InvestorWelcomePage.aspx";
                            break;
                        default:
                            RetWelcomePage = "~/UserLogin.aspx";
                            break;
                    }
                }
                return RetWelcomePage;
            }
            set { WelcomePage = value; }
        }
        #endregion

        #region SessionState
        public static bool SessionState
        {
            get
            {
                return (HttpContext.Current.Session["UserType"] != null && (HttpContext.Current.Session["AdminLoginId"] != null || HttpContext.Current.Session["LoginID"] != null));
            }
            set { SessionState = value; }
        }
        #endregion

        #region DefaultPage
        public static string DefaultPage
        {
            get
            {
                return "~/Login.aspx";
            }
            set { DefaultPage = value; }
        }
        #endregion

        #region NoPermissionPage
        public static string NoPermissionPage
        {
            get
            {
                return "/Home";
            }
            set { NoPermissionPage = value; }
        }
        #endregion

        #region LogoutPage
        public static string LogoutPage
        {
            get
            {
                return "~/Logout.aspx";
            }
            set { LogoutPage = value; }
        }
        #endregion

        #region PermissionDBSet
        public static DataTable PermissionDBSet
        {
            get
            {
                DataTable PDt = new DataTable();
                if (HttpContext.Current.Session["PermissionDBSet"] != null)
                    PDt = (DataTable)HttpContext.Current.Session["PermissionDBSet"];
                else if (HttpContext.Current.Session["PermissionDBSet"] != null)
                    PDt = (DataTable)HttpContext.Current.Session["PermissionDBSet"];

                return PDt;
            }
            set { HttpContext.Current.Session["PermissionDBSet"] = value; }
        }
        #endregion

        #region IsViewPermit
        public static bool IsViewPermit(string FormName)
        {
            Common objDFP = new Common();
            DataSet DsPer = new DataSet();

            bool IsView = false;

            string UserID = (HttpContext.Current.Session["Pk_Pk_AdminId"] != null) ? HttpContext.Current.Session["Pk_AdminId"].ToString() : "-1";
            bool IsPermissions = (System.Configuration.ConfigurationSettings.AppSettings["Permission"].ToString() != "No");

            if (!IsPermissions || Sessions.UserType.ToLower().Contains("crm-admin") || Sessions.UserType.ToLower().Contains("admin") || Sessions.UserType.ToLower().Contains("super") || Sessions.UserType.ToLower().Contains("agent") || Sessions.UserType.ToLower().Contains("investor"))
            {
                IsView = true;
            }
            else if (Sessions.PermissionDBSet != null && Sessions.PermissionDBSet.Rows.Count > 0)
            {
                DataTable DtPermission = Sessions.PermissionDBSet;

                if (DtPermission.Select("FormName = '" + FormName + "'").Length > 0)
                {
                    IsView = (DtPermission.Select("FormName = '" + FormName + "' and FormView = 1").Length > 0);
                }
                else
                {
                    IsView = false;
                }
            }
            else
            {
                DsPer = objDFP.FormPermissions(FormName, UserID);
                if (DsPer != null && DsPer.Tables.Count > 0 && DsPer.Tables[0].Rows.Count > 0)
                {
                    string[] strP = DsPer.Tables[0].Rows[0]["UserAccessPermission"].ToString().Trim().Split(',');
                    IsView = (strP[0] == "Y");
                }
                else
                {
                    IsView = false;
                }
            }

            return IsView;
        }
        #endregion

        #region IsTabViewPermit
        public static bool IsTabViewPermit(string FormType)
        {
            Common objDFP = new Common();
            DataSet DsPer = new DataSet();

            bool IsView = false;

            string UserID = (HttpContext.Current.Session["Pk_AdminId"] != null) ? HttpContext.Current.Session["Pk_AdminId"].ToString() : "-1";
            bool IsPermissions = (System.Configuration.ConfigurationSettings.AppSettings["Permission"].ToString() != "No");

            if (!IsPermissions || Sessions.UserType.ToLower().Contains("crm-admin") || Sessions.UserType.ToLower().Contains("admin") || Sessions.UserType.ToLower().Contains("super") || Sessions.UserType.ToLower().Contains("agent") || Sessions.UserType.ToLower().Contains("investor"))
            {
                IsView = true;
            }
            else if (Sessions.PermissionDBSet != null && Sessions.PermissionDBSet.Rows.Count > 0)
            {
                DataTable DtPermission = Sessions.PermissionDBSet;

                if (DtPermission.Select("FormType = '" + FormType + "'").Length > 0)
                {
                    IsView = (DtPermission.Select("FormType = '" + FormType + "' and FormView = 1").Length > 0);
                }
                else
                {
                    IsView = false;
                }
            }
            else
            {
                //DsPer = objDFP.FormPermissions(FormName, UserID);
                //if (DsPer != null && DsPer.Tables.Count > 0 && DsPer.Tables[0].Rows.Count > 0)
                //{
                //    string[] strP = DsPer.Tables[0].Rows[0]["UserAccessPermission"].ToString().Trim().Split(',');
                //    IsView = (strP[0] == "Y");
                //}
                //else
                //{
                IsView = false;
                //}
            }

            return IsView;
        }
        #endregion

        #region SelectedLoginID
        public static string SelectedLoginID
        {
            get { return (HttpContext.Current.Session["SelectedLoginID"] != null) ? HttpContext.Current.Session["SelectedLoginID"].ToString() : ""; }
            set { HttpContext.Current.Session["SelectedLoginID"] = value; }
        }
        #endregion
    }
}
