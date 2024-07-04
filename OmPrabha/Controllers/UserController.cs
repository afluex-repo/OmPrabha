
using OmPrabha.Filter;
using OmPrabha.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace OmPrabha.Controllers
{
    public class UserController : BaseController
    {
        public ActionResult BinaryTree()
        {
            ViewBag.Fk_UserId = Session["Pk_UserId"].ToString();
            return View();
        }
        public ActionResult Registration(string Pid, string lg)
        {
            string FormName = "";
            string Controller = "";
            Home obj = new Home();
            #region ForQueryString
            if (Request.QueryString["Pid"] != null)
            {
                obj.SponsorId = Request.QueryString["Pid"].ToString();
                obj.Leg = Request.QueryString["lg"].ToString();
                if (obj.Leg == "Right")
                {
                    ViewBag.RightChecked = "checked";
                    ViewBag.LeftChecked = "";
                    ViewBag.Disabled = "Disabled";
                }
                else
                {
                    ViewBag.RightChecked = "";
                    ViewBag.LeftChecked = "checked";
                    ViewBag.Disabled = "Disabled";
                }
                Common objcomm = new Common();
                objcomm.ReferBy = obj.SponsorId;
                DataSet ds = objcomm.GetMemberDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    obj.SponsorName = ds.Tables[0].Rows[0]["FullName"].ToString();



                }
            }
            else
            {

                obj.SponsorId = Session["LoginId"].ToString();
                obj.SponsorName = Session["FullName"].ToString();
            }


            #endregion ForQueryString

            #region ddlgender
            List<SelectListItem> ddlgender = Common.BindGender();
            ViewBag.ddlgender = ddlgender;
            #endregion ddlgender

            #region ddlRate
            List<SelectListItem> ddlRate = Common.CustomerBindDdlRate();
            ViewBag.ddlRate = ddlRate;
            #endregion ddlRate

            List<SelectListItem> ddlPLC = Common.PLC();
            ViewBag.ddlPLC = ddlPLC;


            Wallet model = new Wallet();
            model.Package = "4";
            model.Amount = "1000";

            Common objcomm1 = new Common();
            List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
            ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
            DataSet ds2 = objcomm1.GetPaymentMode();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {



                    ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PaymentMode"].ToString() });

                }
            }

            ViewBag.ddlpaymentmode = ddlpaymentmode;

            List<SelectListItem> ddlBookingType = Common.BookingType();
            ViewBag.ddlBookingType = ddlBookingType;


            List<SelectListItem> ddlAssociateBenefit = Common.AssociateBenefit();
            ViewBag.ddlAssociateBenefit = ddlAssociateBenefit;

            List<SelectListItem> ddlCustomerBenefit = Common.CustomerBenefit();
            ViewBag.ddlCustomerBenefit = ddlCustomerBenefit;

            List<SelectListItem> ddlamount = Common.BindAmount();
            ViewBag.ddlamount = ddlamount;

            FormName = "Registration";
            Controller = "AssociateDashboard";
            return RedirectToAction(FormName,Controller);
        }
    }
}