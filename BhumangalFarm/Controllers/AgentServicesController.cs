using OmPrabha.DAL;
using OmPrabha.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace OmPrabha.Controllers
{
    public class AgentServicesController : AdminBaseController
    {
        public JsonResult GetGeneology(string memID)
        {
            AgentModel model = new AgentModel();
            model.Fk_UserId = memID;
            AgentDAL obj = new AgentDAL();
            DataTable dt = obj.GetTreeMembers(model);
            List<AgentModel> tree = new List<AgentModel>();
            foreach (DataRow dr in dt.Rows)
            {
                tree.Add(new AgentModel
                {
                    Fk_UserId = dr["MemberID"].ToString(),
                    Leg = dr["Leg"].ToString(),
                    Status = dr["Status"].ToString(),
                    MemberName = dr["MemberName"].ToString(),
                    ParentId = dr["ParentId"].ToString(),
                    LoginId = dr["LoginId"].ToString(),
                    ParentLoginId = dr["ParentLoginId"].ToString(),
                    CssClass = dr["cssStatus"].ToString(),
                    SelfBusiness = dr["PackageName"].ToString(),
                    Href = dr["href"].ToString(),
                    JoiningDate = string.IsNullOrEmpty(dr["JoiningDate"].ToString()) ? "N/A" : Convert.ToDateTime(dr["JoiningDate"]).ToString("dd-MMM, yyyy"),
                    Spillby = dr["Spillby"].ToString(),
                    AllLeg1 = dr["AllLeg1"].ToString(),
                    AllLeg2 = dr["AllLeg2"].ToString(),

                    PermanentLeg1 = dr["PermanentLeg1"].ToString(),
                    PermanentLeg2 = dr["PermanentLeg2"].ToString(),

                    InactiveLeft = dr["InactiveLeft"].ToString(),
                    InactiveRight = dr["InactiveRight"].ToString(),

                    PCountLeg1 = dr["PCountLeg1"].ToString(),
                    PCountLeg2 = dr["PCountLeg2"].ToString(),

                    PaidLeg1 = dr["PaidLeg1"].ToString(),
                    PaidLeg2 = dr["PaidLeg2"].ToString(),

                    BalanceLeft = dr["BalanceLeft"].ToString(),
                    BalanceRight = dr["BalanceRight"].ToString(),

                    ProductName = dr["PackageName"].ToString(),
                    Gender = dr["Gender"].ToString(),
                    ProfilePic = dr["ProfilePic"].ToString(),
                    ActivationDate = dr["ActivationDate"].ToString(),
                    AllBusinessLeft = dr["AllBusinessLeft"].ToString(),
                    AllBusinessRight = dr["AllBusinessRight"].ToString(),
                    ProductLeftBusiness = dr["ProductBusinessLeft"].ToString(),
                    ProductRightBusiness = dr["ProductBusinessRight"].ToString(),
                });




            }
            return Json(tree, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetGeneologyForAdmin(string memID)
        {
            AgentModel model = new AgentModel();
            model.Fk_UserId = memID;
            AgentDAL obj = new AgentDAL();
            DataTable dt = obj.GetTreeMembersForAdmin(model);
            List<AgentModel> tree = new List<AgentModel>();
            foreach (DataRow dr in dt.Rows)
            {
                tree.Add(new AgentModel
                {
                    Fk_UserId = dr["MemberID"].ToString(),
                    Leg = dr["Leg"].ToString(),
                    Status = dr["Status"].ToString(),
                    MemberName = dr["MemberName"].ToString(),
                    ParentId = dr["ParentId"].ToString(),
                    LoginId = dr["LoginId"].ToString(),
                    ParentLoginId = dr["ParentLoginId"].ToString(),
                    CssClass = dr["cssStatus"].ToString(),
                    SelfBusiness = dr["PackageName"].ToString(),
                    Href = dr["href"].ToString(),
                    JoiningDate = string.IsNullOrEmpty(dr["ActivationDate"].ToString()) ? "N/A" : Convert.ToDateTime(dr["ActivationDate"]).ToString("dd-MMM, yyyy"),
                    Spillby = dr["Spillby"].ToString(),
                    AllLeg1 = dr["AllLeg1"].ToString(),
                    AllLeg2 = dr["AllLeg2"].ToString(),

                    PermanentLeg1 = dr["PermanentLeg1"].ToString(),
                    PermanentLeg2 = dr["PermanentLeg2"].ToString(),

                    InactiveLeft = dr["InactiveLeft"].ToString(),
                    InactiveRight = dr["InactiveRight"].ToString(),

                    PCountLeg1 = dr["PCountLeg1"].ToString(),
                    PCountLeg2 = dr["PCountLeg2"].ToString(),

                    PaidLeg1 = dr["PaidLeg1"].ToString(),
                    PaidLeg2 = dr["PaidLeg2"].ToString(),

                    BalanceLeft = dr["BalanceLeft"].ToString(),
                    BalanceRight = dr["BalanceRight"].ToString(),

                    ProductName = dr["PackageName"].ToString(),
                    Gender = dr["Gender"].ToString(),
                    ProfilePic = dr["ProfilePic"].ToString(),
                    ActivationDate = dr["ActivationDate"].ToString(),
                    AllBusinessLeft = dr["AllBusinessLeft"].ToString(),
                    AllBusinessRight = dr["AllBusinessRight"].ToString(),

                    ProductLeftBusiness = dr["ProductBusinessLeft"].ToString(),
                    ProductRightBusiness = dr["ProductBusinessRight"].ToString(),
                });




            }
            return Json(tree, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetUserFirst(string memID)
        {
            AgentModel model = new AgentModel();
            model.Fk_UserId = memID;
            AgentDAL obj = new AgentDAL();
            DataTable dt = obj.GetUserFirst(model);
            List<AgentModel> List = new List<AgentModel>();
            foreach (DataRow dr in dt.Rows)
            {
                model.Leg = dr["Leg"].ToString();
                model.Status = dr["Status"].ToString();
                model.MemberName = dr["MemberName"].ToString();
                model.ParentId = dr["ParentId"].ToString();
                model.LoginId = dr["LoginId"].ToString();
                model.ParentLoginId = dr["ParentLoginId"].ToString();
                model.BlockStatus = dr["BlockStatus"].ToString();
                model.Fk_UserId = memID;
                model.JoiningDate = Convert.ToDateTime(dr["JoiningDate"]).ToString("dd-MMM, yyyy");

                model.AllLeg1 = dr["AllLeg1"].ToString();
                model.AllLeg2 = dr["AllLeg2"].ToString();

                model.PermanentLeg1 = dr["PermanentLeg1"].ToString();
                model.PermanentLeg2 = dr["PermanentLeg2"].ToString();

                model.InactiveLeft = dr["InactiveLeft"].ToString();
                model.InactiveRight = dr["InactiveRight"].ToString();

                model.PCountLeg1 = dr["PCountLeg1"].ToString();
                model.PCountLeg2 = dr["PCountLeg2"].ToString();

                model.PaidLeg1 = dr["PaidLeg1"].ToString();
                model.PaidLeg2 = dr["PaidLeg2"].ToString();

                model.BalanceLeft = dr["BalanceLeft"].ToString();
                model.BalanceRight = dr["BalanceRight"].ToString();

                model.ProductName = dr["PackageName"].ToString();
                model.Gender = dr["Gender"].ToString();
                model.ProfilePic = dr["ProfilePic"].ToString();
                model.ActivationDate = dr["ActivationDate"].ToString();
                model.AllBusinessLeft = dr["AllBusinessLeft"].ToString();
                model.AllBusinessRight = dr["AllBusinessRight"].ToString();
                model.ProductLeftBusiness = dr["ProductLeftBusiness"].ToString();
                model.ProductRightBusiness = dr["ProductRightBusiness"].ToString();
                List.Add(model);
            }

            return Json(List, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserFirstForAdmin(string memID)
        {
            AgentModel model = new AgentModel();
            model.Fk_UserId = memID;
            AgentDAL obj = new AgentDAL();
            DataTable dt = obj.GetUserFirstForAdmin(model);
            List<AgentModel> List = new List<AgentModel>();
            foreach (DataRow dr in dt.Rows)
            {
                model.Leg = dr["Leg"].ToString();
                model.Status = dr["Status"].ToString();
                model.MemberName = dr["MemberName"].ToString();
                model.ParentId = dr["ParentId"].ToString();
                model.LoginId = dr["LoginId"].ToString();
                model.ParentLoginId = dr["ParentLoginId"].ToString();
                model.BlockStatus = dr["BlockStatus"].ToString();
                model.Fk_UserId = memID;
                model.JoiningDate = Convert.ToDateTime(dr["JoiningDate"]).ToString("dd-MMM, yyyy");

                model.AllLeg1 = dr["AllLeg1"].ToString();
                model.AllLeg2 = dr["AllLeg2"].ToString();

                model.PermanentLeg1 = dr["PermanentLeg1"].ToString();
                model.PermanentLeg2 = dr["PermanentLeg2"].ToString();

                model.InactiveLeft = dr["InactiveLeft"].ToString();
                model.InactiveRight = dr["InactiveRight"].ToString();

                model.PCountLeg1 = dr["PCountLeg1"].ToString();
                model.PCountLeg2 = dr["PCountLeg2"].ToString();

                model.PaidLeg1 = dr["PaidLeg1"].ToString();
                model.PaidLeg2 = dr["PaidLeg2"].ToString();

                model.BalanceLeft = dr["BalanceLeft"].ToString();
                model.BalanceRight = dr["BalanceRight"].ToString();

                model.ProductName = dr["PackageName"].ToString();
                model.Gender = dr["Gender"].ToString();
                model.ProfilePic = dr["ProfilePic"].ToString();
                model.ActivationDate = dr["ActivationDate"].ToString();
                model.AllBusinessLeft = dr["AllBusinessLeft"].ToString();
                model.AllBusinessRight = dr["AllBusinessRight"].ToString();
                model.ProductLeftBusiness = dr["ProductLeftBusiness"].ToString();
                model.ProductRightBusiness = dr["ProductRightBusiness"].ToString();
                List.Add(model);
            }

            return Json(List, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchCustomersByLoginId2(string prefix)
        {
            List<string> list = new List<string>();
            if (Session["Pk_AdminId"] != null)
            {
                int headId = 1;
                //string qry = "Select LoginId,Pk_UserId as MemberID from tbluserlogin Where LoginID = '" + prefix + "'";
                Models.DAL.ReturnData obj = new Models.DAL.ReturnData();
                DataTable dt = obj.GetUserLoginDetailByLoginId(prefix);

                if (dt.Rows.Count > 0)
                {
                    string loginId = dt.Rows[0]["LoginId"].ToString();
                    DataTable dtCheck = obj.CheckParentForTreeView(Convert.ToInt32(dt.Rows[0]["MemberID"]), headId);
                    if (dtCheck.Rows.Count > 0)
                    {
                        if (dtCheck.Rows[0]["Msg"].ToString() == "Success")
                        {
                            int returnID = Convert.ToInt32(dtCheck.Rows[0]["Fk_UserId"]);
                        }
                        else
                        {
                            int returnID = Convert.ToInt32(dtCheck.Rows[0]["Fk_UserId"]);
                        }
                    }

                    foreach (DataRow dr in dtCheck.Rows)
                    {
                        list.Add(string.Format("{0}.{1}", loginId, dr["Fk_UserId"]));
                    }

                }

            }
            else if (Session["Pk_UserId"] != null)
            {
                int headId = Convert.ToInt32(Session["Pk_UserId"].ToString());
                //string qry = "Select LoginId,Pk_UserId as MemberID from tbluserlogin Where LoginID = '" + prefix + "'";
                Models.DAL.ReturnData obj = new Models.DAL.ReturnData();
                DataTable dt = obj.GetUserLoginDetailByLoginId(prefix);

                if (dt.Rows.Count > 0)
                {
                    string loginId = dt.Rows[0]["LoginId"].ToString();
                    DataTable dtCheck = obj.CheckParentForTreeView(Convert.ToInt32(dt.Rows[0]["MemberID"]), headId);
                    if (dtCheck.Rows.Count > 0)
                    {
                        if (dtCheck.Rows[0]["Msg"].ToString() == "Success")
                        {
                            int returnID = Convert.ToInt32(dtCheck.Rows[0]["Fk_UserId"]);
                        }
                        else
                        {
                            int returnID = Convert.ToInt32(dtCheck.Rows[0]["Fk_UserId"]);
                        }
                    }

                    foreach (DataRow dr in dtCheck.Rows)
                    {
                        list.Add(string.Format("{0}.{1}", loginId, dr["Fk_UserId"]));
                    }

                }
            }
            else
            {
                list.Add(string.Format("{0}.{1}", "Please Login First", "0"));
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}