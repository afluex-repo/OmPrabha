
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
    public class UserReportsController : BaseController
    {
        #region PaidPayout
        public ActionResult PaidPayout()
        {
            return View();
        }
        [HttpPost]
        [ActionName("PaidPayout")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetPaidPayout(Wallet objewallet)
        {
            List<Wallet> lst = new List<Wallet>();
            objewallet.LoginId = Session["LoginId"].ToString();
            objewallet.FromDate = string.IsNullOrEmpty(objewallet.FromDate) ? null : Common.ConvertToSystemDate(objewallet.FromDate, "dd/MM/yyyy");
            objewallet.ToDate = string.IsNullOrEmpty(objewallet.ToDate) ? null : Common.ConvertToSystemDate(objewallet.ToDate, "dd/MM/yyyy");
            DataSet ds = objewallet.GetPaidPayout();
            ViewBag.Total = "0";
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.LoginId = dr["Loginid"].ToString();
                    Objload.DisplayName = dr["Name"].ToString();
                    Objload.PaymentDate = dr["Paymentdate"].ToString();

                    Objload.Amount = dr["Amount"].ToString();
                    Objload.TransactionDate = dr["TransactionDate"].ToString();
                    Objload.TransactionNo = dr["TransactionNo"].ToString();
                    ViewBag.Total = Convert.ToDecimal(ViewBag.Total) + Convert.ToDecimal(dr["Amount"].ToString());
                    lst.Add(Objload);
                }
                objewallet.lstpayoutledger = lst;
            }
            return View(objewallet);
        }
        #endregion

        public ActionResult DirectIncome(Wallet payoutDetail)
        {

            List<Wallet> lst1 = new List<Wallet>();

            payoutDetail.FromDate = Common.ConvertToSystemDate(DateTime.Today.ToShortDateString(), "MM/dd/yyyy");
            payoutDetail.ToDate = Common.ConvertToSystemDate(DateTime.Today.ToShortDateString(), "MM/dd/yyyy");
            payoutDetail.LoginId = Session["LoginID"].ToString();
            DataSet ds11 = payoutDetail.GetPayoutReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Wallet Obj = new Wallet();
                    Obj.EncryptLoginID = Crypto.Encrypt(r["LoginId"].ToString());
                    Obj.EncryptPayoutNo = Crypto.Encrypt(r["PayoutNo"].ToString());

                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["FirstName"].ToString();
                    Obj.PayoutNo = r["PayoutNo"].ToString();
                    Obj.ClosingDate = r["ClosingDate"].ToString();

                    Obj.DirectIncome = r["DirectIncome"].ToString();

                    lst1.Add(Obj);
                }
                payoutDetail.lstPayoutDetail = lst1;
            }
            return View(payoutDetail);
        }
        [HttpPost]
        [ActionName("DirectIncome")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult DirectIncomeDetails(Wallet payoutDetail)
        {
            payoutDetail.FromDate = string.IsNullOrEmpty(payoutDetail.FromDate) ? null : Common.ConvertToSystemDate(payoutDetail.FromDate, "dd/MM/yyyy");
            payoutDetail.ToDate = string.IsNullOrEmpty(payoutDetail.ToDate) ? null : Common.ConvertToSystemDate(payoutDetail.ToDate, "dd/MM/yyyy");
            List<Wallet> lst1 = new List<Wallet>();
            payoutDetail.LoginId = Session["LoginID"].ToString();
            DataSet ds11 = payoutDetail.GetPayoutReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Wallet Obj = new Wallet();
                    Obj.EncryptLoginID = Crypto.Encrypt(r["LoginId"].ToString());
                    Obj.EncryptPayoutNo = Crypto.Encrypt(r["PayoutNo"].ToString());

                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["FirstName"].ToString();
                    Obj.PayoutNo = r["PayoutNo"].ToString();
                    Obj.ClosingDate = r["ClosingDate"].ToString();

                    Obj.DirectIncome = r["DirectIncome"].ToString();

                    lst1.Add(Obj);
                }
                payoutDetail.lstPayoutDetail = lst1;
            }
            return View(payoutDetail);
        }

        public ActionResult PairIncome(Wallet payoutDetail)
        {

            List<Wallet> lst1 = new List<Wallet>();
            payoutDetail.FromDate = Common.ConvertToSystemDate(DateTime.Today.ToShortDateString(), "MM/dd/yyyy");
            payoutDetail.ToDate = Common.ConvertToSystemDate(DateTime.Today.ToShortDateString(), "MM/dd/yyyy");
            payoutDetail.LoginId = Session["LoginID"].ToString();
            DataSet ds11 = payoutDetail.GetPayoutReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Wallet Obj = new Wallet();
                    Obj.EncryptLoginID = Crypto.Encrypt(r["LoginId"].ToString());
                    Obj.EncryptPayoutNo = Crypto.Encrypt(r["PayoutNo"].ToString());

                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["FirstName"].ToString();
                    Obj.PayoutNo = r["PayoutNo"].ToString();
                    Obj.ClosingDate = r["ClosingDate"].ToString();

                    Obj.BinaryIncome = r["BinaryIncome"].ToString();

                    lst1.Add(Obj);
                }
                payoutDetail.lstPayoutDetail = lst1;
            }
            return View(payoutDetail);
        }
        [HttpPost]
        [ActionName("PairIncome")]
        [OnAction(ButtonName = "Search")]
        public ActionResult PairIncomeDetails(Wallet payoutDetail)
        {
            payoutDetail.FromDate = string.IsNullOrEmpty(payoutDetail.FromDate) ? null : Common.ConvertToSystemDate(payoutDetail.FromDate, "dd/MM/yyyy");
            payoutDetail.ToDate = string.IsNullOrEmpty(payoutDetail.ToDate) ? null : Common.ConvertToSystemDate(payoutDetail.ToDate, "dd/MM/yyyy");
            List<Wallet> lst1 = new List<Wallet>();
            payoutDetail.LoginId = Session["LoginID"].ToString();
            DataSet ds11 = payoutDetail.GetPayoutReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Wallet Obj = new Wallet();
                    Obj.EncryptLoginID = Crypto.Encrypt(r["LoginId"].ToString());
                    Obj.EncryptPayoutNo = Crypto.Encrypt(r["PayoutNo"].ToString());

                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["FirstName"].ToString();
                    Obj.PayoutNo = r["PayoutNo"].ToString();
                    Obj.ClosingDate = r["ClosingDate"].ToString();

                    Obj.BinaryIncome = r["BinaryIncome"].ToString();

                    lst1.Add(Obj);
                }
                payoutDetail.lstPayoutDetail = lst1;
            }
            return View(payoutDetail);
        }
        #region BusinessReport

        public ActionResult BusinessReport(Reports model)
        {

            #region ddlleg
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.Leg = Leg;
            #endregion ddlleg

            return View(model);
        }
        [HttpPost]
        [ActionName("BusinessReport")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult BusinessReportBy(Reports model)
        {

            #region ddlleg
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.Leg = Leg;
            #endregion ddlleg

            List<Reports> lst1 = new List<Reports>();
            model.Leg = string.IsNullOrEmpty(model.Leg) ? null : model.Leg;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.LoginId = Session["LoginID"].ToString();
            model.Downline = model.IsDownline == true ? "1" : "0";
            // model.IsDownline = Request["Chk_"].ToString(); 
            DataSet ds11 = model.BusinessReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.AssociateID = r["AssociateLoginID"].ToString();
                    Obj.CustomerLoginID = r["CustomerLoginID"].ToString();
                    Obj.CustomerId = r["CustomerID"].ToString();
                    Obj.Customername = r["CustomerName"].ToString();
                    Obj.AssociateName = r["AssociateName"].ToString();
                    Obj.DisplayName = r["FirstName"].ToString();
                    Obj.Leg = r["Leg"].ToString();
                    Obj.PaidAmount = r["PaidAmount"].ToString();
                    Obj.ClosingDate = r["CalculationDate"].ToString();
                    Obj.NetAmount = r["AMount"].ToString();
                    Obj.SiteName = r["SiteName"].ToString();
                    Obj.SectorName = r["SectorName"].ToString();
                    Obj.BlockName = r["BlockName"].ToString();
                    Obj.PlotNumber = r["PlotNumber"].ToString();
                    //Obj.LeadershipBonus = r["BV"].ToString();
                    Obj.PaymentDate = r["PaymentDate"].ToString();
                    lst1.Add(Obj);
                }
                model.lstassociate = lst1;
                ViewBag.TotalNetAmount = double.Parse(ds11.Tables[0].Compute("sum(AMount)", "").ToString()).ToString("n2");
                //ViewBag.TotalBV = double.Parse(ds11.Tables[0].Compute("sum(BV)", "").ToString()).ToString("n2");
            }


            return View(model);
        }

        #endregion

        public ActionResult TopupList()
        {
            Reports model = new Reports();
            try
            {
                model.LoginId = Session["LoginID"].ToString();
                DataSet ds = model.GetTopupReport();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Reports> lstTopupReport = new List<Reports>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Reports obj = new Reports();
                        obj.FK_InvestmentID = Crypto.Encrypt(r["Pk_InvestmentId"].ToString());
                        obj.Name = r["Name"].ToString() + " (" + r["LoginId"].ToString() + ")";
                        obj.UpgradtionDate = r["UpgradtionDate"].ToString();
                        obj.ProductName = r["Package"].ToString();
                        obj.Amount = r["Amount"].ToString();
                        obj.PlotNumber = r["PlotNumber"].ToString();
                        obj.LoginId = r["LoginId"].ToString();
                        lstTopupReport.Add(obj);
                    }
                    model.lsttopupreport = lstTopupReport;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        public ActionResult PayReport(Reports model)
        {
            List<Reports> lst1 = new List<Reports>();
            model.Fk_UserId = Session["Pk_userId"].ToString();
            DataSet ds11 = model.PayReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["FirstName"].ToString();
                    Obj.PaymentDate = r["PaymentDate"].ToString();
                    Obj.Amount = r["PaidAmount"].ToString();
                    Obj.ReceiptNo = r["ReceiptNo"].ToString();
                    Obj.PlotNumber = r["PlotNumber"].ToString();

                    lst1.Add(Obj);
                }
                model.lstassociate = lst1;
            }
            return View(model);
        }


        #region LuckyDraw

        public ActionResult LuckyDraw(Reports model)
        {
            try
            {
                List<Reports> lst1 = new List<Reports>();
                DataSet ds = model.LuckyDrawList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Reports obj = new Reports();
                        obj.Month = r["Month"].ToString();
                        obj.LoginId = r["LoginId"].ToString();
                        obj.PlotNumber = r["PlotNumber"].ToString();
                        obj.Name = r["Name"].ToString();
                        obj.Id = Convert.ToInt32(r["Id"]);

                        lst1.Add(obj);
                    }
                    model.lstLuckyDraw = lst1;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }




        #endregion

        public ActionResult PlotAvailability(Reports model)
        {
            #region ddlSiteType
            Reports objSite = new Reports();
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet ds2 = objSite.GetSiteList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlSite = ddlSite;

            #endregion


            //List<SelectListItem> ddlSite = new List<SelectListItem>();
            //ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
            //ViewBag.ddlSite = ddlSite;

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;

            return View();
        }
        public ActionResult GetSiteDetails(string SiteID)
        {
            try
            {
                Reports model = new Reports();
                model.SiteID = SiteID;

                #region GetSectors
                List<SelectListItem> ddlSector = new List<SelectListItem>();
                DataSet dsSector = model.GetSectorList();

                if (dsSector != null && dsSector.Tables.Count > 0)
                {
                    foreach (DataRow r in dsSector.Tables[0].Rows)
                    {
                        ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                    }
                }
                model.ddlSector = ddlSector;
                model.Result = "yes";
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public ActionResult GetBlockList(string SiteID, string SectorID)
        {
            List<SelectListItem> lstBlock = new List<SelectListItem>();
            Reports model = new Reports();
            model.SiteID = SiteID;
            model.SectorID = SectorID;
            DataSet dsblock = model.GetBlockList();

            #region ddlBlock
            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {
                    lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                }

            }

            model.lstBlock = lstBlock;
            #endregion

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("PlotAvailability")]
        [OnAction(ButtonName = "Search")]

        public ActionResult Details(Reports model)
        {
            //Master model = new Master();
            List<Reports> lst = new List<Reports>();

            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;

            DataSet dsblock1 = model.GetDetails();
            if (dsblock1 != null && dsblock1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsblock1.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.PlotID = r["PK_PlotID"].ToString();
                    obj.SiteID = r["FK_SiteID"].ToString();
                    obj.SectorID = r["FK_SectorID"].ToString();
                    obj.BlockID = r["FK_BlockID"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.PlotStatus = r["Status"].ToString();
                    obj.ColorCSS = r["ColorCSS"].ToString();
                    obj.PlotAmount = r["PlotAmount"].ToString();
                    obj.PlotArea = r["PlotArea"].ToString();
                    obj.PlotSize = r["PlotSize"].ToString();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.BlockName = r["BlockName"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    //model.PlotID = dsblock.Tables[0].Rows[0]["PK_PLotID"].ToString();
                    //model.SiteID = dsblock.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    //model.SectorID = dsblock.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    //model.BlockID = dsblock.Tables[0].Rows[0]["FK_BlockID"].ToString();
                    //model.PlotNumber = dsblock.Tables[0].Rows[0]["PlotNumber"].ToString();
                    //model.PlotStatus = dsblock.Tables[0].Rows[0]["Status"].ToString();

                    lst.Add(obj);
                }

                model.lstPlot = lst;
            }

            #region ddlSite
            int count1 = 0;
            Reports objmaster = new Reports();
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = objmaster.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            #region GetSectors
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            DataSet dsSector = objmaster.GetSectorList();
            int sectorcount = 0;

            if (dsSector != null && dsSector.Tables.Count > 0)
            {

                foreach (DataRow r in dsSector.Tables[0].Rows)
                {
                    if (sectorcount == 0)
                    {
                        ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
                    }
                    ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });
                    sectorcount = 1;
                }
            }

            ViewBag.ddlSector = ddlSector;
            #endregion

            #region BlockList
            List<SelectListItem> lstBlock = new List<SelectListItem>();

            int blockcount = 0;
            //objmodel.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
            //objmodel.SectorID = ds.Tables[0].Rows[0]["PK_SectorID"].ToString();
            DataSet dsblock = objmaster.GetBlockList();


            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {
                    if (blockcount == 0)
                    {
                        lstBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                    }
                    lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                    blockcount = 1;
                }

            }


            ViewBag.ddlBlock = lstBlock;
            #endregion


            return View(model);
        }
    }
}