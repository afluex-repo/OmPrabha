using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OmPrabha.Filter;
using OmPrabha.Models;

namespace OmPrabha.Controllers
{
    public class TransactionController : Controller
    {
        #region LoginAssociate
        public ActionResult AssociateLogin()
        {
            return View();
        }

        public ActionResult AssociateList(Transaction model)
        {
            List<Transaction> lst = new List<Transaction>();
            DataSet ds = model.GetList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Transaction obj = new Transaction();

                    obj.UserID = r["PK_UserId"].ToString();
                    obj.LoginID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    lst.Add(obj);
                }
                model.lstTrad = lst;
            }
            return View(model);
        }


        [HttpPost]
        [ActionName("AssociateLogin")]
        [OnAction(ButtonName = "Search")]
        public ActionResult Login(Transaction model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                if (model.SearchBy == "loginid")
                {
                    model.LoginID = model.Search;
                    DataSet ds = model.Login();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["Login"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                        else
                        {

                            Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            Session["Pk_userId"] = ds.Tables[0].Rows[0]["Pk_userId"].ToString();
                            Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                            Session["FullName"] = ds.Tables[0].Rows[0]["FullName"].ToString();
                            Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                            Session["Status"] = ds.Tables[0].Rows[0]["Status"].ToString();
                            Session["CssClass"] = ds.Tables[0].Rows[0]["StatusColor"].ToString();
                            FormName = "AssociateDashBoard";
                            Controller = "AssociateDashboard";

                        }

                    }
                    else
                    {
                        TempData["T"] = "Associate is either blocked or is deleted permanently !";
                        FormName = "AssociateLogin";
                        Controller = "Transaction";
                    }
                }
                else
                {
                    List<Transaction> lst = new List<Transaction>();
                    DataSet ds = model.GetList();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            Transaction obj = new Transaction();

                            obj.UserID = r["PK_UserId"].ToString();
                            obj.LoginID = r["AssociateId"].ToString();
                            obj.EncryptKey = Crypto.Encrypt(r["AssociateId"].ToString());
                            obj.AssociateName = r["AssociateName"].ToString();
                            obj.Contact = r["Mobile"].ToString();
                            obj.PanNo = r["PanNumber"].ToString();
                            lst.Add(obj);
                        }
                        model.lstTrad = lst;
                    }
                    return View(model);

                }


            }
            catch (Exception ex)
            {
                TempData["Login"] = ex.Message;
                FormName = "AssociateLogin";
                Controller = "Transaction";
            }

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult LoginAssociate(string Id)
        {
            Transaction model = new Transaction();

            model.LoginID = Crypto.Decrypt(Id);
            DataSet ds = model.Login();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["Login"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
                else
                {

                    Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                    Session["Pk_userId"] = ds.Tables[0].Rows[0]["Pk_userId"].ToString();
                    Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                    Session["FullName"] = ds.Tables[0].Rows[0]["FullName"].ToString();
                    Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                    Session["Status"] = ds.Tables[0].Rows[0]["Status"].ToString();
                    Session["CssClass"] = ds.Tables[0].Rows[0]["StatusColor"].ToString();


                }

            }
            return RedirectToAction("AssociateDashBoard", "AssociateDashBoard");

        }
        #endregion
    }
}