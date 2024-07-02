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
    public class SettingController : Controller
    {
        #region ChangePassword

        public ActionResult ChangePassword()
        {
            // ViewBag.ddlPasswordType = ddlPasswordType;
            return View();
        }

        [HttpPost]
        [ActionName("ChangePassword")]
        [OnAction(ButtonName = "Change")]
        public ActionResult UpdatePassword(Setting obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdatePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Login"] = "Password updated successfully..";
                        FormName = "Login";
                        Controller = "Home";
                    }
                    else
                    {
                        TempData["Login"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ChangePassword";
                        Controller = "Setting";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Login"] = ex.Message;
                FormName = "ChangePassword";
                Controller = "Setting";
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion


        #region ChangeAssociatePassword

        public ActionResult ChangeAssociatePassword()
        {
            // ViewBag.ddlPasswordType = ddlPasswordType;
            return View();
        }
        [HttpPost]
        [ActionName("ChangeAssociatePassword")]
        [OnAction(ButtonName = "Change")]
        public ActionResult UpdateAssociatePassword(Setting obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
               obj.Password =  Crypto.Encrypt(obj.Password);
                obj.NewPassword = Crypto.Encrypt(obj.NewPassword);
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateAssociatePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Login"] = "Password updated successfully..";
                        FormName = "ChangeAssociatePassword";
                        Controller = "Setting";
                    }
                    else
                    {
                        TempData["Login"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ChangeAssociatePassword";
                        Controller = "Setting";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Login"] = ex.Message;
                FormName = "ChangeAssociatePassword";
                Controller = "Setting";
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion 
    }
}