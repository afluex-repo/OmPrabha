using OmPrabha.Filter;
using OmPrabha.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OmPrabha.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult TopUp(Wallet model)
        {
            #region BindPaymentmode

            List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
            ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
            DataSet ds2 = model.GetPaymentMode();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                }
            }
            ViewBag.ddlpaymentmode = ddlpaymentmode;

            #endregion BindPaymentmode

            #region BindddlPackageName

            List<SelectListItem> ddlPackageName = new List<SelectListItem>();
            ddlPackageName.Add(new SelectListItem { Text = "Select Package", Value = "0" });
            DataSet ds1 = model.GetPackage();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    ddlPackageName.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                }
            }
            ViewBag.ddlPackageName = ddlPackageName;

            #endregion BindddlPackageName
            return View(model);
        }
        

        [HttpPost]
        [ActionName("TopUp")]
        [OnAction(ButtonName = "btnsave")]
        public ActionResult TopUpByAdmin(Wallet obj)
        {
            try
            {
                obj.TopUpDate = Common.ConvertToSystemDate(obj.TopUpDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.TopUpIdByAdmin();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Topup"] = "TopUp Done successfully";
                    }
                    else
                    {
                        TempData["Topup"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else { }
            }
            catch (Exception ex)
            {
                TempData["Topup"] = ex.Message;
            }
            return RedirectToAction("Topup", "Accounts");
        }

        public ActionResult GetMemberName(string LoginId)
        {
            Wallet obj = new Wallet();
            obj.LoginId = LoginId;
            DataSet ds = obj.GetUserName();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                obj.Result = "Yes";
            }
            else { obj.Result = "No"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}