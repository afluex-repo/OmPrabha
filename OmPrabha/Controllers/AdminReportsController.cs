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
    public class AdminReportsController : AdminBaseController
    {
        #region Due Installment Report

        public ActionResult DueInstallmentReport(string PK_BookingId)
        {
            Plot model = new Plot();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.PlotNumber = string.IsNullOrEmpty(model.PlotNumber) ? null : model.PlotNumber;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.PK_BookingId = PK_BookingId;
            DataSet dsBookingDetails = model.GetBookingDetailsList();
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
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

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);

        }

        public ActionResult FillDueInstsDetails(string BookingNumber, string FromDate, string ToDate, string SiteID, string SectorID, string BlockID, string PlotNumber)
        {

            Plot model = new Plot();
            List<Plot> lst = new List<Plot>();
            model.SiteID = SiteID == "0" ? null : SiteID;
            model.SectorID = SectorID == "0" ? null : SectorID;
            model.BlockID = BlockID == "0" ? null : BlockID;
            model.BookingNumber = string.IsNullOrEmpty(BookingNumber) ? null : BookingNumber;
            model.PlotNumber = string.IsNullOrEmpty(PlotNumber) ? null : PlotNumber;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            // model.PlotNumber = PlotNumber;
            DataSet dsblock = model.FillDueInstDetails();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {

                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {

                    model.Result = "yes";
                    // model.PlotID = dsblock.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsblock.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.PayAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.BookingDate = dsblock.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.BookingAmount = dsblock.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.PaymentDate = dsblock.Tables[0].Rows[0]["PaymentDate"].ToString();
                    model.PaidAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.Discount = dsblock.Tables[0].Rows[0]["Discount"].ToString();
                    model.PaymentPlanID = dsblock.Tables[0].Rows[0]["Fk_PlanId"].ToString();
                    model.PlanName = dsblock.Tables[0].Rows[0]["PlanName"].ToString();
                    model.PK_BookingId = dsblock.Tables[0].Rows[0]["PK_BookingId"].ToString();
                    model.TotalAllotmentAmount = dsblock.Tables[0].Rows[0]["TotalAllotmentAmount"].ToString();
                    model.PaidAllotmentAmount = dsblock.Tables[0].Rows[0]["PaidAllotmentAmount"].ToString();
                    model.BalanceAllotmentAmount = dsblock.Tables[0].Rows[0]["BalanceAllotmentAmount"].ToString();
                    model.TotalInstallment = dsblock.Tables[0].Rows[0]["TotalInstallment"].ToString();
                    model.InstallmentAmount = dsblock.Tables[0].Rows[0]["InstallmentAmount"].ToString();
                    model.PlotArea = dsblock.Tables[0].Rows[0]["PlotArea"].ToString();
                    model.Balance = dsblock.Tables[0].Rows[0]["BalanceAmount"].ToString();


                }

            }
            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in dsblock.Tables[1].Rows)
                {
                    Plot obj = new Plot();
                    obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                    obj.PK_BookingId = r["Fk_BookingId"].ToString();
                    obj.InstallmentNo = r["InstallmentNo"].ToString();
                    obj.InstallmentDate = r["InstallmentDate"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.InstallmentAmount = r["InstAmt"].ToString();
                    obj.PaymentMode = r["PaymentModeName"].ToString();
                    obj.DueAmount = r["DueAmount"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            else
            {
                model.Result = "No record found !";
            }
            #region ddlSite
            int count1 = 0;
            Master objmaster = new Master();
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
            DataSet dsblock1 = objmaster.GetBlockList();


            if (dsblock1 != null && dsblock1.Tables.Count > 0 && dsblock1.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock1.Tables[0].Rows)
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
            return Json(model, JsonRequestBehavior.AllowGet);

        }

        #endregion




        public ActionResult SummaryReport(Plot model)
        {

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
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
            DataSet dsSector = model.GetSectorList();
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
            DataSet dsblock = model.GetBlockList();


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
            #region ddlleg
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.Leg = Leg;
            #endregion ddlleg
            //#endregion
            //List<SelectListItem> ddlSector = new List<SelectListItem>();
            //ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
            //ViewBag.ddlSector = ddlSector;

            //List<SelectListItem> ddlBlock = new List<SelectListItem>();
            //ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            //ViewBag.ddlBlock = ddlBlock;

            return View(model);
        }
        [HttpPost]
        [ActionName("SummaryReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetSummaryRep(Plot model)
        {
            

            int count1 = 0;
            Master objmaster = new Master();
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
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.Downline = model.IsDownline == true ? "1" : "0";
            model.Direct = model.IsDirect == true ? "1" : "0";
            DataSet ds = model.GetSummaryList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.PK_BookingId = r["PK_BookingID"].ToString();
                    obj.CustomerName = r["CustomerInfo"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.AssociateID = r["AssociateInfo"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentDate = r["LastPaymentDate"].ToString();
                    //obj.PlotNumber = r["PlotInfo"].ToString();
                    obj.PlotAmount = r["NetPlotAmount"].ToString();
                    obj.Balance = r["Balance"].ToString();
                    obj.Amount = r["PlotAmount"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    obj.Discount = r["Discount"].ToString();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    obj.BlockName = r["BlockName"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.BookingDate = r["BookingDate"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
                ViewBag.TotalNetPlotAmount = double.Parse(ds.Tables[0].Compute("sum(NetPlotAmount)", "").ToString()).ToString("n2");
                ViewBag.TotalPaidAmount = double.Parse(ds.Tables[0].Compute("sum(PaidAmount)", "").ToString()).ToString("n2");
                ViewBag.TotalBalance = double.Parse(ds.Tables[0].Compute("sum(Balance)", "").ToString()).ToString("n2");
            }
            return View(model);
        }
        #region CustomerLedgerReport
        public ActionResult CustomerLedgerReport(Plot model)
        {
            //PK_BookingId = Convert.ToInt32(Session["PK_BookingId"].ToString()).ToString();
            string SessionBookingNumber = Session["BookingNumber"] as string;
            if (model.BookingNumber == null && SessionBookingNumber != null || SessionBookingNumber == "")
            {
                model.BookingNumber = Session["BookingNumber"].ToString();
            }
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.PlotNumber = string.IsNullOrEmpty(model.PlotNumber) ? null : model.PlotNumber;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.PK_BookingId = model.PK_BookingId;
            DataSet dsBookingDetails = model.GetBookingDetailsList();
            #region ddlSite
            int count1 = 0;

            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
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

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }

        public ActionResult Details(string BookingNumber, string SiteID, string SectorID, string BlockID, string PlotNumber, string CustomerName)
        {
            Plot model = new Plot();
            try
            {
                List<Plot> lst = new List<Plot>();
                model.SiteID = SiteID == "0" ? null : SiteID;
                model.SectorID = SectorID == "0" ? null : SectorID;
                model.BlockID = BlockID == "0" ? null : BlockID;
                model.BookingNumber = string.IsNullOrEmpty(BookingNumber) ? null : BookingNumber;
                model.PlotNumber = string.IsNullOrEmpty(PlotNumber) ? null : PlotNumber;
                model.CustomerName = string.IsNullOrEmpty(CustomerName) ? null : CustomerName;
                //model.TotalGeneratedAmount = "0";
                //model.TotalPaidAmount = "0";
                //model.TotalRemainingAmount = "0";
                DataSet dsblock = model.FillDetails();
                if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
                {

                    if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        model.Result = dsblock.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                    else if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {

                        model.Result = "yes";
                        model.hdBookingNo = Crypto.Encrypt(dsblock.Tables[0].Rows[0]["BookingNo"].ToString());
                        // model.PlotID = dsblock.Tables[0].Rows[0]["PK_PlotID"].ToString();
                        model.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                        model.ActualPlotRate = dsblock.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                        model.PlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();
                        model.PayAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                        model.BookingDate = dsblock.Tables[0].Rows[0]["BookingDate"].ToString();
                        model.BookingAmount = dsblock.Tables[0].Rows[0]["BookingAmt"].ToString();
                        model.PaymentDate = dsblock.Tables[0].Rows[0]["PaymentDate"].ToString();
                        model.PaidAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                        model.Discount = dsblock.Tables[0].Rows[0]["Discount"].ToString();
                        model.PaymentPlanID = dsblock.Tables[0].Rows[0]["Fk_PlanId"].ToString();
                        model.PlanName = dsblock.Tables[0].Rows[0]["PlanName"].ToString();
                        model.PK_BookingId = dsblock.Tables[0].Rows[0]["PK_BookingId"].ToString();
                        model.TotalAllotmentAmount = dsblock.Tables[0].Rows[0]["TotalAllotmentAmount"].ToString();
                        model.PaidAllotmentAmount = dsblock.Tables[0].Rows[0]["PaidAllotmentAmount"].ToString();
                        model.BalanceAllotmentAmount = dsblock.Tables[0].Rows[0]["BalanceAllotmentAmount"].ToString();
                        model.TotalInstallment = dsblock.Tables[0].Rows[0]["TotalInstallment"].ToString();
                        model.InstallmentAmount = dsblock.Tables[0].Rows[0]["InstallmentAmount"].ToString();
                        model.PlotArea = dsblock.Tables[0].Rows[0]["PlotArea"].ToString();
                        model.PlotSize = dsblock.Tables[0].Rows[0]["PlotSize"].ToString();
                        model.Balance = dsblock.Tables[0].Rows[0]["BalanceAmount"].ToString();
                        model.CustomerLoginID = dsblock.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                        model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();
                        model.SiteName = dsblock.Tables[0].Rows[0]["SiteName"].ToString();
                        model.PlotNumber = dsblock.Tables[0].Rows[0]["PlotNumber"].ToString();
                        model.SectorName = dsblock.Tables[0].Rows[0]["SectorName"].ToString();
                        model.BlockName = dsblock.Tables[0].Rows[0]["BlockName"].ToString();
                        model.Status = dsblock.Tables[0].Rows[0]["Status"].ToString();
                        if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[1].Rows.Count > 0)
                        {
                            foreach (DataRow r in dsblock.Tables[1].Rows)
                            {
                                Plot obj = new Plot();

                                obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                                obj.PK_BookingId = r["Fk_BookingId"].ToString();
                                //obj.ReceiptNo = r["ReceiptNo"].ToString();
                                obj.GeneratedAmount = r["GeneratedAmount"].ToString();
                                obj.RemainingAmount = r["RemainingAmount"].ToString();
                                obj.InstallmentNo = r["InstallmentNo"].ToString();
                                obj.InstallmentDate = r["InstallmentDate"].ToString();
                                obj.PaymentDate = r["PaymentDate"].ToString();
                                obj.PaidAmount = r["PaidAmount"].ToString();
                                obj.InstallmentAmount = r["InstAmt"].ToString();
                                obj.PaymentMode = r["PaymentModeName"].ToString();
                                obj.BankName = r["BankName"].ToString();
                                obj.PaymentStatus = r["paymentstatus"].ToString();
                                obj.ReceiptNo = r["ReceiptNo"].ToString();
                                obj.TransactionNumber = r["TransactionNo"].ToString();
                                //obj.Cheque = r["Cheque"].ToString();
                                obj.Remark = r["Remark"].ToString();
                                lst.Add(obj);
                                model.TotalGeneratedAmount = double.Parse(dsblock.Tables[1].Compute("sum(GeneratedAmount)", "").ToString()).ToString("n2");
                                model.TotalPaidAmount = double.Parse(dsblock.Tables[1].Compute("sum(PaidAmount)", "").ToString()).ToString("n2");
                                model.TotalRemainingAmount = double.Parse(dsblock.Tables[1].Compute("sum(RemainingAmount)", "").ToString()).ToString("n2");
                            }
                            model.lstPlot = lst;
                        }
                    }
                }
                else
                {
                    model.Result = "No record found !";
                }


                #region ddlSite
                int count1 = 0;
                Master objmaster = new Master();
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
                DataSet dsblock1 = objmaster.GetBlockList();


                if (dsblock1 != null && dsblock1.Tables.Count > 0 && dsblock1.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in dsblock1.Tables[0].Rows)
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
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);

        }
        public ActionResult LedgerReport(string BookingNumber)
        {
            string FormName = "";
            string Controller = "";
            Plot model = new Plot();
            model.LoginId = Session["LoginId"].ToString();
            model.BookingNumber = string.IsNullOrEmpty(BookingNumber) ? null : BookingNumber;
            List<Plot> lstBlockList = new List<Plot>();

            DataSet dsblock = model.FillDetails();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {

                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {

                    model.Result = "yes";
                    // model.PlotID = dsblock.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsblock.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.PayAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.BookingDate = dsblock.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.BookingAmount = dsblock.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.PaymentDate = dsblock.Tables[0].Rows[0]["PaymentDate"].ToString();
                    model.PaidAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.Discount = dsblock.Tables[0].Rows[0]["Discount"].ToString();
                    model.PaymentPlanID = dsblock.Tables[0].Rows[0]["Fk_PlanId"].ToString();
                    model.PlanName = dsblock.Tables[0].Rows[0]["PlanName"].ToString();
                    model.PK_BookingId = dsblock.Tables[0].Rows[0]["PK_BookingId"].ToString();
                    model.TotalAllotmentAmount = dsblock.Tables[0].Rows[0]["TotalAllotmentAmount"].ToString();
                    model.PaidAllotmentAmount = dsblock.Tables[0].Rows[0]["PaidAllotmentAmount"].ToString();
                    model.BalanceAllotmentAmount = dsblock.Tables[0].Rows[0]["BalanceAllotmentAmount"].ToString();
                    model.TotalInstallment = dsblock.Tables[0].Rows[0]["TotalInstallment"].ToString();
                    model.InstallmentAmount = dsblock.Tables[0].Rows[0]["InstallmentAmount"].ToString();
                    model.PlotArea = dsblock.Tables[0].Rows[0]["PlotArea"].ToString();
                    model.Balance = dsblock.Tables[0].Rows[0]["BalanceAmount"].ToString();
                    Session["BookingNumber"] = model.BookingNumber;

                }
                Session["PK_BookingId"] = model.PK_BookingId;
                FormName = "CustomerLedgerReport";
                Controller = "AdminReports";
            }
            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in dsblock.Tables[1].Rows)
                {
                    Plot obj = new Plot();

                    obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                    obj.PK_BookingId = r["Fk_BookingId"].ToString();
                    obj.InstallmentNo = r["InstallmentNo"].ToString();
                    obj.InstallmentDate = r["InstallmentDate"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.InstallmentAmount = r["InstAmt"].ToString();
                    obj.PaymentMode = r["PaymentModeName"].ToString();
                    obj.DueAmount = r["DueAmount"].ToString();
                    lstBlockList.Add(obj);
                    FormName = "CustomerLedgerReport";
                    Controller = "AdminReports";
                }
                model.lstPlot = lstBlockList;
            }
            else
            {
                TempData["Login"] = "Data Not Found";

            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult PrintCustomerLedger(string bn)
        {
            Plot model = new Plot();
            try
            {
                List<Plot> lst = new List<Plot>();
                model.BookingNumber = Crypto.Decrypt(bn);
                DataSet dsblock = model.FillDetails();
                if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
                {

                    if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        model.Result = "yes";
                        ViewBag.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString() + " (" + dsblock.Tables[0].Rows[0]["CustomerLoginID"].ToString() + ")";
                        ViewBag.CustomerMobile = dsblock.Tables[0].Rows[0]["Mobile"].ToString();
                        ViewBag.CustomerAddress = dsblock.Tables[0].Rows[0]["Address"].ToString();
                        ViewBag.Pincode = dsblock.Tables[0].Rows[0]["Pincode"].ToString();
                        ViewBag.State = dsblock.Tables[0].Rows[0]["statename"].ToString();
                        ViewBag.City = dsblock.Tables[0].Rows[0]["Districtname"].ToString();
                        ViewBag.SiteName = dsblock.Tables[0].Rows[0]["SiteName"].ToString();
                        ViewBag.SiteAddress = dsblock.Tables[0].Rows[0]["SiteAddress"].ToString();
                        ViewBag.SectorName = dsblock.Tables[0].Rows[0]["SectorName"].ToString();
                        ViewBag.BlockName = dsblock.Tables[0].Rows[0]["BlockName"].ToString();
                        ViewBag.PlotNumber = dsblock.Tables[0].Rows[0]["PlotNumber"].ToString();

                        ViewBag.NetPlotAmount = dsblock.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                        ViewBag.PaidAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                        ViewBag.NetAmtWords = dsblock.Tables[0].Rows[0]["NetAmountInWords"].ToString();
                        ViewBag.PaidAmtWords = dsblock.Tables[0].Rows[0]["PaidAmountInWords"].ToString();

                        ViewBag.AssociateName = dsblock.Tables[0].Rows[0]["AssociateName"].ToString();
                        ViewBag.AssociateLoginID = dsblock.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                        ViewBag.PlotArea = dsblock.Tables[0].Rows[0]["PlotArea"].ToString();
                        ViewBag.PLC = dsblock.Tables[0].Rows[0]["PLCCharge"].ToString();

                    }
                }
                else
                {
                    model.Result = "No record found !";
                }
                if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in dsblock.Tables[1].Rows)
                    {
                        Plot obj = new Plot();

                        obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                        obj.PK_BookingId = r["Fk_BookingId"].ToString();
                        obj.InstallmentNo = r["InstallmentNo"].ToString();
                        obj.InstallmentDate = r["InstallmentDate"].ToString();
                        obj.PaymentDate = r["PaymentDate"].ToString();
                        obj.PaidAmount = r["PaidAmount"].ToString();
                        obj.InstallmentAmount = r["InstAmt"].ToString();
                        obj.PaymentMode = r["PaymentModeName"].ToString();
                        obj.DueAmount = r["DueAmount"].ToString();

                        lst.Add(obj);
                    }
                    model.lstPlot = lst;
                }
            }
            catch (Exception ex)
            {

            }
            return View();
            //return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult GetBookingNoByName(string CustomerName)
        {
            Plot model = new Plot();
            List<Plot> lst = new List<Plot>();
            model.CustomerName = CustomerName;
            DataSet ds = model.GetBookingNoByName();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #region PlotAvailability

        public ActionResult PlotAvailability(Master model)
        {
            #region ddlSiteType
            Master objSiteType = new Master();
            int count1 = 0;
            List<SelectListItem> ddlSiteType = new List<SelectListItem>();
            DataSet ds2 = objSiteType.GetSiteTypeList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    //if (count1 == 0)
                    //{
                    //    ddlSiteType.Add(new SelectListItem { Text = "Select Site Type", Value = "0" });
                    //}
                    ddlSiteType.Add(new SelectListItem { Text = r["SiteTypeName"].ToString(), Value = r["PK_SiteTypeID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlSiteType = ddlSiteType;

            #endregion


            //List<SelectListItem> ddlSite = new List<SelectListItem>();
            //ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
            //ViewBag.ddlSite = ddlSite;
            #region GetSite
            int count2 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count2 = count2 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;

            return View(model);
        }

        public ActionResult GetSiteBySiteType(string SiteTypeID)
        {
            try
            {
                Master model = new Master();
                model.SiteTypeID = SiteTypeID;

                #region GetSite
                List<SelectListItem> ddlSite = new List<SelectListItem>();
                DataSet dsSector = model.GetSiteList();

                if (dsSector != null && dsSector.Tables.Count > 0)
                {
                    foreach (DataRow r in dsSector.Tables[0].Rows)
                    {
                        ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });

                    }
                }
                model.ddlSite = ddlSite;
                #endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public ActionResult GetSiteDetails(string SiteID)
        {
            try
            {
                Master model = new Master();
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
            Master model = new Master();
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
        public ActionResult Details(Master model, string SiteID, string SectorID)
        {
            //Master model = new Master();
            List<Master> lst = new List<Master>();

            //model.SiteID = SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.SiteTypeID = model.SiteTypeID == "0" ? null : model.SiteTypeID;

            DataSet dsblock1 = model.GetDetails();
            if (dsblock1 != null && dsblock1.Tables[0].Rows.Count > 0)
            {
                model.SiteID = dsblock1.Tables[1].Rows[0]["SiteID"].ToString();
                model.SectorID = dsblock1.Tables[1].Rows[0]["SectorID"].ToString();
                model.BlockID = dsblock1.Tables[1].Rows[0]["BlockID"].ToString();
                foreach (DataRow r in dsblock1.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PlotID = r["PK_PlotID"].ToString();
                    obj.SiteID = r["FK_SiteID"].ToString();
                    obj.SectorID = r["FK_SectorID"].ToString();
                    obj.BlockID = r["FK_BlockID"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.PlotStatus = r["Status"].ToString();
                    obj.ColorCSS = r["ColorCSS"].ToString();
                    obj.PlotAmount = r["PlotAmount"].ToString();
                    obj.PlotArea = r["TotalArea"].ToString();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.BlockName = r["BlockName"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    obj.PlotSize = r["PlotSize"].ToString();
                    obj.UnitName = r["UnitName"].ToString();
                    obj.PlotRate = r["PlotRate"].ToString();
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
            Master objmaster = new Master();
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
            objmaster.SiteID = SiteID;
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
            objmaster.SectorID = SectorID;
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

            #region ddlSiteType
            Master objSiteType = new Master();
            int countType = 0;
            List<SelectListItem> ddlSiteType = new List<SelectListItem>();
            DataSet ds2 = objSiteType.GetSiteTypeList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (countType == 0)
                    {
                        ddlSiteType.Add(new SelectListItem { Text = "Select Site Type", Value = "0" });
                    }
                    ddlSiteType.Add(new SelectListItem { Text = r["SiteTypeName"].ToString(), Value = r["PK_SiteTypeID"].ToString() });
                    countType = countType + 1;
                }
            }

            ViewBag.ddlSiteType = ddlSiteType;

            #endregion

            return View(model);
        }
        #endregion


        #region PlotAllotmentReport

        public ActionResult PlotAllotmentReport(Plot model)
        {

            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.PlotNumber = string.IsNullOrEmpty(model.PlotNumber) ? null : model.PlotNumber;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
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

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;

            return View(model);
        }

        [HttpPost]
        [ActionName("PlotAllotmentReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetAllotRep(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.List();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                    obj.PK_BookingId = r["PK_BookingID"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    //obj.TransactionDate = r["TransactionDate"].ToString();
                    //obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    //obj.PlotNumber = r["PlotInfo"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_BookingDetailsId"].ToString());
                    obj.SiteName = r["SiteName"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    obj.BlockName = r["BlockName"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            #region ddlSite
            int count1 = 0;
            Master objmaster = new Master();
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
            DataSet dsblock1 = objmaster.GetBlockList();


            if (dsblock1 != null && dsblock1.Tables.Count > 0 && dsblock1.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock1.Tables[0].Rows)
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

        public ActionResult PrintAllotment(string PrintId)
        {
            Plot newdata = new Plot();
            newdata.EncryptKey = Crypto.Decrypt(PrintId);
            newdata.PK_BookingDetailsId = Crypto.Decrypt(PrintId);
            ViewBag.Name = Session["Name"].ToString();
            DataSet ds = newdata.List();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {

                    newdata.Result = "yes";
                    ViewBag.PK_BookingId = ds.Tables[0].Rows[0]["PK_BookingId"].ToString();
                    ViewBag.CustomerName = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                    ViewBag.CustomerFatherName = ds.Tables[0].Rows[0]["FathersName"].ToString();
                    ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    ViewBag.Pin = ds.Tables[0].Rows[0]["PinCode"].ToString();
                    ViewBag.State = ds.Tables[0].Rows[0]["State"].ToString();
                    ViewBag.City = ds.Tables[0].Rows[0]["City"].ToString();
                    ViewBag.AssociateID = ds.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    ViewBag.Contact = ds.Tables[0].Rows[0]["AssociateMobile"].ToString();
                    ViewBag.CustomerID = ds.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    ViewBag.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                    ViewBag.SectorName = ds.Tables[0].Rows[0]["SectorName"].ToString();
                    ViewBag.BlockName = ds.Tables[0].Rows[0]["BlockName"].ToString();
                    ViewBag.PlotNo = ds.Tables[0].Rows[0]["PlotNumber"].ToString();

                    ViewBag.PlotNumber = ds.Tables[0].Rows[0]["PlotInfo"].ToString();
                    ViewBag.PaidAmount = ds.Tables[0].Rows[0]["latestpayment"].ToString();
                    ViewBag.TotalDeposit = ds.Tables[0].Rows[0]["TotalDeposit"].ToString();
                    ViewBag.PlotArea = ds.Tables[0].Rows[0]["PlotArea"].ToString();
                    ViewBag.PaymentMode = ds.Tables[0].Rows[0]["PaymentMode"].ToString();
                    ViewBag.ReasonOfPayment = ds.Tables[0].Rows[0]["ReasonOfPayment"].ToString();
                    ViewBag.PaymentDate = ds.Tables[0].Rows[0]["PaymentDate"].ToString();
                    ViewBag.ReceiptNo = ds.Tables[0].Rows[0]["ReceiptNo"].ToString();
                    ViewBag.CorporateOffice = ds.Tables[0].Rows[0]["CorporateOffice"].ToString();
                    ViewBag.AssociateName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                    ViewBag.BookingDate = ds.Tables[0].Rows[0]["BookingDate"].ToString();
                    ViewBag.customerMobile = ds.Tables[0].Rows[0]["customerMobile"].ToString();
                    ViewBag.PLC = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["PLC"].ToString()) ? "N/A" : ds.Tables[0].Rows[0]["PLC"].ToString();
                    ViewBag.AmountInWords = ds.Tables[0].Rows[0]["PaidAmountInWords"].ToString();
                    ViewBag.NetPlotAmount = ds.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                    ViewBag.NetPlotAmountInWords = ds.Tables[0].Rows[0]["NetPlotAmountInWords"].ToString();
                  
                    ViewBag.TransactionNo = ds.Tables[0].Rows[0]["TransactionNo"].ToString();
                    ViewBag.TransactionDate = ds.Tables[0].Rows[0]["TransactionDate"].ToString();
                    ViewBag.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                    ViewBag.BankBranch = ds.Tables[0].Rows[0]["BankBranch"].ToString();
                    ViewBag.RemainingAmount = ds.Tables[0].Rows[0]["Balance"].ToString();
                    ViewBag.PlotRate = ds.Tables[0].Rows[0]["PlotRate"].ToString();
                    ViewBag.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                    ViewBag.InstallmentNo = ds.Tables[0].Rows[0]["InstallmentNo"].ToString();
                    //ViewBag.AdjustmentloginId = ds.Tables[0].Rows[0]["AdjustmentloginId"].ToString();

                    ViewBag.CompanyName = SoftwareDetails.CompanyName;
                    ViewBag.CompanyAddress = SoftwareDetails.CompanyAddress;
                    ViewBag.Pin1 = SoftwareDetails.Pin1;
                    ViewBag.State1 = SoftwareDetails.State1;
                    ViewBag.City1 = SoftwareDetails.City1;
                    ViewBag.ContactNo = SoftwareDetails.ContactNo;
                    ViewBag.LandLine = SoftwareDetails.LandLine;
                    ViewBag.Website = SoftwareDetails.Website;
                    ViewBag.EmailID = SoftwareDetails.EmailID;
                }
            }

            return View(newdata);
        }

        #endregion

        #region WelcomeLetter Associate

        public ActionResult WelcomeLetter()
        {
            return View();
        }
        [HttpPost]
        [ActionName("WelcomeLetter")]
        [OnAction(ButtonName = "btnSearchCustomer")]
        public ActionResult AssociateList(TraditionalAssociate model)
        {
            List<TraditionalAssociate> lst = new List<TraditionalAssociate>();
            DataSet ds = model.GetList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    TraditionalAssociate obj = new TraditionalAssociate();

                    obj.UserID = r["PK_UserId"].ToString();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.SponsorID = r["SponsorId"].ToString();
                    obj.SponsorName = r["SponsorName"].ToString();
                    //   obj.LoginID = r["LoginId"].ToString();
                    //  obj.DesignationID = r["FK_DesignationID"].ToString();
                    // obj.FirstName = r["AName"].ToString();
                    obj.isBlocked = r["isBlocked"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    obj.City = r["City"].ToString();
                    obj.State = r["State"].ToString();
                    obj.Address = r["Address"].ToString();
                    // obj.PanNo = r["PanNumber"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_UserId"].ToString());
                    lst.Add(obj);
                }
                model.lstTrad = lst;
            }
            return View(model);
        }

        public ActionResult PrintWelcomeLetter(string id)
        {
            TraditionalAssociate obj = new TraditionalAssociate();
            obj.UserID = Crypto.Decrypt(id);

            DataSet ds = obj.GetList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                // obj.Result = "yes";
                //ViewBag.PK_BookingId = ds.Tables[0].Rows[0]["PK_BookingId"].ToString();
                ViewBag.AssociateID = ds.Tables[0].Rows[0]["AssociateId"].ToString();
                ViewBag.AssociateName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                ViewBag.Pin = ds.Tables[0].Rows[0]["PinCode"].ToString();
                ViewBag.State = ds.Tables[0].Rows[0]["State"].ToString();
                ViewBag.City = ds.Tables[0].Rows[0]["City"].ToString();
                ViewBag.Contact = ds.Tables[0].Rows[0]["Mobile"].ToString();
                ViewBag.Designation = ds.Tables[0].Rows[0]["DesignationName"].ToString();

                ViewBag.MemberAccNo = ds.Tables[0].Rows[0]["MemberAccNo"].ToString();
                ViewBag.MemberBankName = ds.Tables[0].Rows[0]["MemberBankName"].ToString();
                ViewBag.MemberBranch = ds.Tables[0].Rows[0]["MemberBranch"].ToString();
                ViewBag.IFSCCode = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
                ViewBag.ProfilePic = ds.Tables[0].Rows[0]["ProfilePic"].ToString();


                ViewBag.CompanyName = SoftwareDetails.CompanyName;
            }

            return View(obj);
        }
        public ActionResult PrintIDCard(string id)
        {
            TraditionalAssociate obj = new TraditionalAssociate();
            obj.UserID = Crypto.Decrypt(id);

            DataSet ds = obj.GetList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                // obj.Result = "yes";
                //ViewBag.PK_BookingId = ds.Tables[0].Rows[0]["PK_BookingId"].ToString();
                ViewBag.AssociateID = ds.Tables[0].Rows[0]["AssociateId"].ToString();
                ViewBag.AssociateName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                ViewBag.Pin = ds.Tables[0].Rows[0]["PinCode"].ToString();
                ViewBag.State = ds.Tables[0].Rows[0]["State"].ToString();
                ViewBag.City = ds.Tables[0].Rows[0]["City"].ToString();
                ViewBag.Contact = ds.Tables[0].Rows[0]["Mobile"].ToString();
                ViewBag.Designation = ds.Tables[0].Rows[0]["DesignationName"].ToString();
            }

            return View(obj);
        }
        #endregion

        #region Approve KYC
        public ActionResult AssociateListForKYC()
        {
            List<SelectListItem> ddlKYCStatus = Common.BindKYCStatus();
            ViewBag.ddlKYCStatus = ddlKYCStatus;
            List<Reports> lst = new List<Reports>();

            return View();
        }
        [HttpPost]
        [ActionName("AssociateListForKYC")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult AssociateListForKYC(AssociateBooking model)
        {
            List<SelectListItem> ddlKYCStatus = Common.BindKYCStatus();
            ViewBag.ddlKYCStatus = ddlKYCStatus;
            List<AssociateBooking> lst = new List<AssociateBooking>();

            DataSet ds = model.AssociateListForKYC();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.PK_DocumentID = r["PK_UserDocumentID"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.DisplayName = r["FirstName"].ToString();
                    obj.DocumentNumber = r["DocumentNumber"].ToString();
                    obj.DocumentType = r["DocumentType"].ToString();
                    obj.DocumentImage = (r["DocumentImage"].ToString());
                    obj.Status = (r["Status"].ToString());

                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            return View(model);
        }

        public ActionResult ApproveKYC(string Id, string DocumentType, string LoginID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                List<SelectListItem> ddlKYCStatus = Common.BindKYCStatus();
                ViewBag.ddlKYCStatus = ddlKYCStatus;
                List<AssociateBooking> lst = new List<AssociateBooking>();

                AssociateBooking model = new AssociateBooking();
                model.LoginId = LoginID;
                model.PK_DocumentID = Id;
                model.DocumentType = DocumentType;
                model.Status = "Approved";
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = new DataSet();
                ds = model.ApproveKYC();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["KYCVerification"] = "KYC Approved Successfully..";
                        FormName = "AssociateListForKYC";
                        Controller = "AdminReports";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["KYCVerification"] = "Error : " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AssociateListForKYC";
                        Controller = "AdminReports";
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["KYCVerification"] = ex.Message;
                FormName = "AssociateListForKYC";
                Controller = "AdminReports";
            }
            return RedirectToAction(FormName, Controller);
        }


        #endregion

        #region  PayoutDetails
        public ActionResult PayoutDetails(AssociateBooking model, string PK_PaidPayoutId)
        {

            //model.FromDate = Common.ConvertToSystemDate(DateTime.Today.ToShortDateString(), "MM/dd/yyyy");
            //model.ToDate = Common.ConvertToSystemDate(DateTime.Today.ToShortDateString(), "MM/dd/yyyy");
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.PayoutDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.PK_PaidPayoutId = r["PK_PaidPayoutId"].ToString();
                    obj.PayOutNo = r["PayoutNo"].ToString();
                    obj.ClosingDate = r["ClosingDate"].ToString();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.GrossAmount = r["GrossAmount"].ToString();
                    obj.DirectIncome = r["DirectIncome"].ToString();
                    obj.DifferentialIncome = r["DifferentialIncome"].ToString();
                    obj.DirectLeadershipIncome = r["DirectLeadershipIncome"].ToString();
                    obj.TDS = r["TDS"].ToString();
                    obj.Processing = r["Processing"].ToString();
                    obj.NetAmount = r["NetAmount"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
                ViewBag.GrossAmount = double.Parse(ds.Tables[0].Compute("sum(GrossAmount)", "").ToString()).ToString("n2");
                ViewBag.TDS = double.Parse(ds.Tables[0].Compute("sum(TDS)", "").ToString()).ToString("n2");
                ViewBag.Processing = double.Parse(ds.Tables[0].Compute("sum(Processing)", "").ToString()).ToString("n2");
                ViewBag.NetAmount = double.Parse(ds.Tables[0].Compute("sum(NetAmount)", "").ToString()).ToString("n2");
                ViewBag.DirectIncome = double.Parse(ds.Tables[0].Compute("sum(DirectIncome)", "").ToString()).ToString("n2");
                ViewBag.DifferentialIncome = double.Parse(ds.Tables[0].Compute("sum(DifferentialIncome)", "").ToString()).ToString("n2");
                ViewBag.DirectLeadershipIncome = double.Parse(ds.Tables[0].Compute("sum(DirectLeadershipIncome)", "").ToString()).ToString("n2");
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("PayoutDetails")]
        [OnAction(ButtonName = "Search")]
        public ActionResult PayoutDetailsBy(AssociateBooking model, string PK_PaidPayoutId)
        {
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            //model.LoginId = model.LoginId == "0" ? null : model.LoginId;

            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.PayoutDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.PK_PaidPayoutId = r["PK_PaidPayoutId"].ToString();
                    obj.PayOutNo = r["PayoutNo"].ToString();
                    obj.ClosingDate = r["ClosingDate"].ToString();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.GrossAmount = r["GrossAmount"].ToString();
                    obj.DirectIncome = r["DirectIncome"].ToString();
                    obj.DifferentialIncome = r["DifferentialIncome"].ToString();
                    obj.DirectLeadershipIncome = r["DirectLeadershipIncome"].ToString();
                    obj.TDS = r["TDS"].ToString();
                    obj.Processing = r["Processing"].ToString();
                    obj.NetAmount = r["NetAmount"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
                ViewBag.GrossAmount = double.Parse(ds.Tables[0].Compute("sum(GrossAmount)", "").ToString()).ToString("n2");
                ViewBag.TDS = double.Parse(ds.Tables[0].Compute("sum(TDS)", "").ToString()).ToString("n2");
                ViewBag.Processing = double.Parse(ds.Tables[0].Compute("sum(Processing)", "").ToString()).ToString("n2");
                ViewBag.NetAmount = double.Parse(ds.Tables[0].Compute("sum(NetAmount)", "").ToString()).ToString("n2");
                ViewBag.DirectIncome = double.Parse(ds.Tables[0].Compute("sum(DirectIncome)", "").ToString()).ToString("n2");
                ViewBag.DifferentialIncome = double.Parse(ds.Tables[0].Compute("sum(DifferentialIncome)", "").ToString()).ToString("n2");
                ViewBag.DirectLeadershipIncome = double.Parse(ds.Tables[0].Compute("sum(DirectLeadershipIncome)", "").ToString()).ToString("n2");
            }
            return View(model);
        }



        public ActionResult PayoutRequestReport(AssociateBooking model)
        {
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.PayoutRequestReport();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();

                    obj.RequestID = r["Pk_RequestId"].ToString();
                    obj.ClosingDate = r["RequestedDate"].ToString();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.FirstName = r["Name"].ToString();
                    obj.GrossAmount = r["AMount"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.DisplayName = r["BackColor"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("PayoutRequestReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult PayoutRequestReportBy(AssociateBooking model)
        {
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.PayoutRequestReport();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();

                    obj.RequestID = r["Pk_RequestId"].ToString();
                    obj.ClosingDate = r["RequestedDate"].ToString();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.FirstName = r["Name"].ToString();
                    obj.GrossAmount = r["AMount"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.DisplayName = r["BackColor"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            return View(model);
        }

        public ActionResult ApproveRequest(string id)
        {
            AssociateBooking obj = new AssociateBooking();
            try
            {
                obj.RequestID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.ApproveRequest();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Request"] = "Request Approved";
                        string mob = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string name = ds.Tables[0].Rows[0]["Name"].ToString();
                        string Amount = ds.Tables[0].Rows[0]["Amount"].ToString();
                        string TempId = "1707166296882557362";
                        BLSMS.SendSMS(mob, "Dear " + name + ", Your Payout Request of Rs" + Amount + " has been approved and processed successfully. Please check your account. OmPrabha", TempId);
                    }
                    else
                    {
                        TempData["Request"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Request"] = ex.Message;
            }
            return RedirectToAction("PayoutRequestReport");
        }

        public ActionResult DeclineRequest(string id)
        {
            AssociateBooking obj = new AssociateBooking();
            try
            {
                obj.RequestID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeclineRequest();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Request"] = "Request Declined";
                    }
                    else
                    {
                        TempData["Request"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Request"] = ex.Message;
            }
            return RedirectToAction("PayoutRequestReport");
        }

        #endregion

        #region UnpaidIncome
        public ActionResult DistributeUnpaidIncome(TraditionalAssociate model, string LoginId)
        {
            List<TraditionalAssociate> lst = new List<TraditionalAssociate>();
            if (LoginId != null)
            {

                model.ToID = LoginId;
            }
            DataSet ds = model.GetUnPaidForDistribute();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.AssociateName = ds.Tables[0].Rows[0]["ToName"].ToString() + "(" + ds.Tables[0].Rows[0]["ToLoginId"].ToString() + ")";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    TraditionalAssociate obj = new TraditionalAssociate();
                    obj.JoiningFromDate = r["CurrentDate"].ToString();
                    obj.FromID = r["FromLoginId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToID = r["ToLoginId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.Amount = r["BusinessAmount"].ToString();
                    obj.DifferencePercentage = r["DifferencePerc"].ToString();
                    obj.Income = r["Income"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.PlotDetails = r["PlotDetails"].ToString();
                    lst.Add(obj);
                }
                model.lstTrad = lst;
                ViewBag.Amount = double.Parse(ds.Tables[0].Compute("sum(BusinessAmount)", "").ToString()).ToString("n2");
                ViewBag.DifferencePerc = double.Parse(ds.Tables[0].Compute("sum(DifferencePerc)", "").ToString()).ToString("n2");
                ViewBag.Income = double.Parse(ds.Tables[0].Compute("sum(Income)", "").ToString()).ToString("n2");

            }
            return View(model);
        }
        public ActionResult UnpaidIncome(TraditionalAssociate model, string LoginId)
        {
            List<TraditionalAssociate> lst = new List<TraditionalAssociate>();
            if (LoginId != null)
            {
                TempData["Plot"] = "UNPAID";
                model.ToID = LoginId;
            }
            DataSet ds = model.UnpaidIncomes();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.AssociateName = ds.Tables[0].Rows[0]["ToName"].ToString() + "(" + ds.Tables[0].Rows[0]["ToLoginId"].ToString() + ")";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    TraditionalAssociate obj = new TraditionalAssociate();
                    obj.JoiningFromDate = r["CurrentDate"].ToString();
                    obj.FromID = r["FromLoginId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToID = r["ToLoginId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.Amount = r["BusinessAmount"].ToString();
                    obj.DifferencePercentage = r["DifferencePerc"].ToString();
                    obj.Income = r["Income"].ToString();
                    obj.Status = r["Status"].ToString();

                    lst.Add(obj);
                }
                model.lstTrad = lst;
                ViewBag.Amount = double.Parse(ds.Tables[0].Compute("sum(BusinessAmount)", "").ToString()).ToString("n2");
                ViewBag.DifferencePerc = double.Parse(ds.Tables[0].Compute("sum(DifferencePerc)", "").ToString()).ToString("n2");
                ViewBag.Income = double.Parse(ds.Tables[0].Compute("sum(Income)", "").ToString()).ToString("n2");

            }
            return View(model);
        }

        [HttpPost]
        [ActionName("UnpaidIncome")]
        [OnAction(ButtonName = "Search")]
        public ActionResult UnpaidIncomeBy(TraditionalAssociate model)
        {
            List<TraditionalAssociate> lst = new List<TraditionalAssociate>();
            model.JoiningFromDate = string.IsNullOrEmpty(model.JoiningFromDate) ? null : Common.ConvertToSystemDate(model.JoiningFromDate, "dd/MM/yyyy");
            model.JoiningToDate = string.IsNullOrEmpty(model.JoiningToDate) ? null : Common.ConvertToSystemDate(model.JoiningToDate, "dd/MM/yyyy");
            DataSet ds = model.UnpaidIncomes();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    TraditionalAssociate obj = new TraditionalAssociate();
                    obj.JoiningFromDate = r["CurrentDate"].ToString();
                    obj.FromID = r["FromLoginId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToID = r["ToLoginId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.Amount = r["BusinessAmount"].ToString();
                    obj.DifferencePercentage = r["DifferencePerc"].ToString();
                    obj.Income = r["Income"].ToString();
                    obj.Status = r["Status"].ToString();

                    lst.Add(obj);
                }
                model.lstTrad = lst;
                ViewBag.Amount = double.Parse(ds.Tables[0].Compute("sum(BusinessAmount)", "").ToString()).ToString("n2");
                ViewBag.DifferencePerc = double.Parse(ds.Tables[0].Compute("sum(DifferencePerc)", "").ToString()).ToString("n2");
                ViewBag.Income = double.Parse(ds.Tables[0].Compute("sum(Income)", "").ToString()).ToString("n2");
            }
            return View(model);
        }

        #endregion

        //#region UnpaidIncome
        //public ActionResult UnpaidIncome(TraditionalAssociate model)
        //{
        //    List<TraditionalAssociate> lst = new List<TraditionalAssociate>();
        //    DataSet ds = model.UnpaidIncomes();

        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow r in ds.Tables[0].Rows)
        //        {
        //            TraditionalAssociate obj = new TraditionalAssociate();
        //            obj.JoiningFromDate = r["CurrentDate"].ToString();
        //            obj.FromID = r["FromLoginId"].ToString();
        //            obj.FromName = r["FromName"].ToString();
        //            obj.ToID = r["ToLoginId"].ToString();
        //            obj.ToName = r["ToName"].ToString();
        //            obj.Amount = r["BusinessAmount"].ToString();
        //            obj.DifferencePercentage = r["DifferencePerc"].ToString();
        //            obj.Income = r["Income"].ToString();


        //            lst.Add(obj);
        //        }
        //        model.lstTrad = lst;
        //    }
        //    return View(model);
        //}

        //[HttpPost]
        //[ActionName("UnpaidIncome")]
        //[OnAction(ButtonName = "Search")]
        //public ActionResult UnpaidIncomeBy(TraditionalAssociate model)
        //{
        //    List<TraditionalAssociate> lst = new List<TraditionalAssociate>();
        //    model.FromID = string.IsNullOrEmpty(model.FromID) ? null : Common.ConvertToSystemDate(model.FromID, "dd/MM/yyyy");
        //    model.ToID = string.IsNullOrEmpty(model.ToID) ? null : Common.ConvertToSystemDate(model.ToID, "dd/MM/yyyy");
        //    model.JoiningFromDate = string.IsNullOrEmpty(model.JoiningFromDate) ? null : Common.ConvertToSystemDate(model.JoiningFromDate, "dd/MM/yyyy");
        //    model.JoiningToDate = string.IsNullOrEmpty(model.JoiningToDate) ? null : Common.ConvertToSystemDate(model.JoiningToDate, "dd/MM/yyyy");
        //    DataSet ds = model.UnpaidIncomes();

        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow r in ds.Tables[0].Rows)
        //        {
        //            TraditionalAssociate obj = new TraditionalAssociate();
        //            obj.JoiningFromDate = r["CurrentDate"].ToString();
        //            obj.FromID = r["FromLoginId"].ToString();
        //            obj.FromName = r["FromName"].ToString();
        //            obj.ToID = r["ToLoginId"].ToString();
        //            obj.ToName = r["ToName"].ToString();
        //            obj.Amount = r["BusinessAmount"].ToString();
        //            obj.DifferencePercentage = r["DifferencePerc"].ToString();
        //            obj.Income = r["Income"].ToString();


        //            lst.Add(obj);
        //        }
        //        model.lstTrad = lst;
        //    }
        //    return View(model);
        //}

        //#endregion

        #region TransactionLogReport

        public ActionResult TransactionLogReport(AssociateBooking model)
        {

            #region GetExpenseName
            List<SelectListItem> ddlexpensename = new List<SelectListItem>();
            //model.ExpenseID = "4";
            DataSet ds = model.GetExpenseNameList();
            int count = 0;
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlexpensename.Add(new SelectListItem { Text = "Select Expense", Value = "0" });
                    }
                    ddlexpensename.Add(new SelectListItem { Text = r["ExpenseName"].ToString(), Value = r["FK_ExpenseId"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ExpenseName = ddlexpensename;
            #endregion
            #region GetFarmerList
            List<SelectListItem> ddlfarmername = new List<SelectListItem>();
            DataSet ds1 = model.GetFarmerList();

            if (ds1 != null && ds1.Tables.Count > 0)
            {
                int count1 = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlfarmername.Add(new SelectListItem { Text = "Select Farmer", Value = "0" });
                    }
                    ddlfarmername.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_FarmerId"].ToString() });
                    count1 = count1 + 1;
                }
            }
            ViewBag.FarmerloginId = ddlfarmername;
            #endregion
            #region GetcustomerList
            List<SelectListItem> ddlcustomername = new List<SelectListItem>();
            DataSet dscust = model.GetcustomerList();

            if (dscust != null && dscust.Tables.Count > 0)
            {
                int count2 = 0;
                foreach (DataRow r in dscust.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlcustomername.Add(new SelectListItem { Text = "Select Customer", Value = "0" });
                    }
                    ddlcustomername.Add(new SelectListItem { Text = r["Fullname"].ToString(), Value = r["Pk_UserId"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.CustomerLoginId = ddlcustomername;
            #endregion
            #region GetAssociateList
            List<SelectListItem> ddlassociatename = new List<SelectListItem>();
            DataSet dsassociatet = model.GetAssociateList();

            if (dsassociatet != null && dsassociatet.Tables.Count > 0)
            {
                int count3 = 0;
                foreach (DataRow r in dsassociatet.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlassociatename.Add(new SelectListItem { Text = "Select Associate", Value = "0" });
                    }
                    ddlassociatename.Add(new SelectListItem { Text = r["Fullname"].ToString(), Value = r["Pk_UserId"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.AssociateloginId = ddlassociatename;
            #endregion
            #region GetcustomerList
            List<SelectListItem> ddlemployeeename = new List<SelectListItem>();
            DataSet dsemp = model.GetEmployeeList();

            if (dsemp != null && dsemp.Tables.Count > 0)
            {
                int count4 = 0;
                foreach (DataRow r in dsemp.Tables[0].Rows)
                {
                    if (count4 == 0)
                    {
                        ddlemployeeename.Add(new SelectListItem { Text = "Select Employee", Value = "0" });
                    }
                    ddlemployeeename.Add(new SelectListItem { Text = r["Fullname"].ToString(), Value = r["PK_AdminId"].ToString() });
                    count4 = count4 + 1;
                }
            }
            ViewBag.EmployeeLoginId = ddlemployeeename;
            #endregion

            return View(model);
        }
        [HttpPost]
        [ActionName("TransactionLogReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult TransactionLogReportBy(AssociateBooking model)
        {

            List<AssociateBooking> lst = new List<AssociateBooking>();
            model.CustomerID = model.CustomerID == "0" ? null : model.CustomerID;
            model.AssociateID = model.AssociateID == "0" ? null : model.AssociateID;
            model.AdminId = model.AdminId == "0" ? null : model.AdminId;
            model.FarmerId = model.FarmerId == "0" ? null : model.FarmerId;
            model.ExpenseID = model.ExpenseID == "0" ? null : model.ExpenseID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.TransactionLogReportBy();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.ActionDateTime = r["ActionDateTime"].ToString();
                    obj.FormAction = r["ActionName"].ToString();
                    obj.TransactionBy = r["TransactionBy"].ToString();
                    obj.Remarks = r["Remark"].ToString();
                    obj.AdminId = r["AdminInfo"].ToString();
                    obj.CustomerLoginID = r["CustInfo"].ToString();
                    obj.AssociateLoginID = r["AssociateInfo"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    obj.FarmerId = r["PK_FarmerId"].ToString();
                    obj.EncryptFarmerId = Crypto.Encrypt(r["PK_FarmerId"].ToString());
                    obj.Name = r["FarmerName"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    obj.PK_BookingId = r["Fk_BookingId"].ToString();
                    // obj.Farmen = Crypto.Decrypt(r["PK_FarmerId"].ToString());
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            #region GetExpenseName
            List<SelectListItem> ddlexpensename = new List<SelectListItem>();
            model.ExpenseID = "4";
            DataSet dsex = model.GetExpenseNameList();
            int count = 0;
            if (dsex != null && dsex.Tables.Count > 0)
            {
                foreach (DataRow r in dsex.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlexpensename.Add(new SelectListItem { Text = "Select Expense", Value = "0" });
                    }
                    ddlexpensename.Add(new SelectListItem { Text = r["ExpenseName"].ToString(), Value = r["FK_ExpenseId"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ExpenseName = ddlexpensename;
            #endregion
            #region GetFarmerList
            List<SelectListItem> ddlfarmername = new List<SelectListItem>();
            DataSet ds1 = model.GetFarmerList();

            if (ds1 != null && ds1.Tables.Count > 0)
            {
                int count1 = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlfarmername.Add(new SelectListItem { Text = "Select Farmer", Value = "0" });
                    }
                    ddlfarmername.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_FarmerId"].ToString() });
                    count1 = count1 + 1;
                }
            }
            ViewBag.FarmerloginId = ddlfarmername;
            #endregion
            #region GetcustomerList
            List<SelectListItem> ddlcustomername = new List<SelectListItem>();
            DataSet dscust = model.GetcustomerList();

            if (dscust != null && dscust.Tables.Count > 0)
            {
                int count2 = 0;
                foreach (DataRow r in dscust.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlcustomername.Add(new SelectListItem { Text = "Select Customer", Value = "0" });
                    }
                    ddlcustomername.Add(new SelectListItem { Text = r["Fullname"].ToString(), Value = r["Pk_UserId"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.CustomerLoginId = ddlcustomername;
            #endregion
            #region GetAssociateList
            List<SelectListItem> ddlassociatename = new List<SelectListItem>();
            DataSet dsassociatet = model.GetAssociateList();

            if (dsassociatet != null && dsassociatet.Tables.Count > 0)
            {
                int count3 = 0;
                foreach (DataRow r in dsassociatet.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlassociatename.Add(new SelectListItem { Text = "Select Associate", Value = "0" });
                    }
                    ddlassociatename.Add(new SelectListItem { Text = r["Fullname"].ToString(), Value = r["Pk_UserId"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.AssociateloginId = ddlassociatename;
            #endregion
            #region GetcustomerList
            List<SelectListItem> ddlemployeeename = new List<SelectListItem>();
            DataSet dsemp = model.GetEmployeeList();

            if (dsemp != null && dsemp.Tables.Count > 0)
            {
                int count4 = 0;
                foreach (DataRow r in dsemp.Tables[0].Rows)
                {
                    if (count4 == 0)
                    {
                        ddlemployeeename.Add(new SelectListItem { Text = "Select Employee", Value = "0" });
                    }
                    ddlemployeeename.Add(new SelectListItem { Text = r["Fullname"].ToString(), Value = r["PK_AdminId"].ToString() });
                    count4 = count4 + 1;
                }
            }
            ViewBag.EmployeeLoginId = ddlemployeeename;
            #endregion
            return View(model);
        }

        #endregion


        public ActionResult Direct()
        {
            List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            ViewBag.ddlStatus = AssociateStatus;
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.ddlleg = Leg;
            return View();
        }
        [HttpPost]
        [ActionName("Direct")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult DirectList(Reports model)
        {

            List<Reports> lst = new List<Reports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.FromActivationDate = string.IsNullOrEmpty(model.FromActivationDate) ? null : Common.ConvertToSystemDate(model.FromActivationDate, "dd/MM/yyyy");
            model.ToActivationDate = string.IsNullOrEmpty(model.ToActivationDate) ? null : Common.ConvertToSystemDate(model.ToActivationDate, "dd/MM/yyyy");

            DataSet ds = model.GetDirectList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.LoginId = (r["LoginId"].ToString());
                    obj.Leg = r["Leg"].ToString();
                    obj.Name = (r["Name"].ToString());
                    obj.Package = (r["ProductName"].ToString());

                    lst.Add(obj);
                }
                model.lstassociate = lst;


            }
            List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            ViewBag.ddlStatus = AssociateStatus;
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.ddlleg = Leg;
            return View(model);
        }
        public ActionResult DownLine(Reports model)
        {
            List<Reports> lst = new List<Reports>();
            model.FromDate = Common.ConvertToSystemDate(DateTime.Today.ToShortDateString(), "MM/dd/yyyy");
            model.ToDate = Common.ConvertToSystemDate(DateTime.Today.ToShortDateString(), "MM/dd/yyyy");

            DataSet ds = model.GetDownlineList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Name = r["Name"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Package = (r["ProductName"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.Mobile = (r["Mobile"].ToString());

                    lst.Add(obj);
                }
                model.lstassociate = lst;
            }
            List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            ViewBag.ddlStatus = AssociateStatus;
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.ddlleg = Leg;
            return View();
        }
        [HttpPost]
        [ActionName("DownLine")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult DownLineList(Reports model)
        {

            List<Reports> lst = new List<Reports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.FromActivationDate = string.IsNullOrEmpty(model.FromActivationDate) ? null : Common.ConvertToSystemDate(model.FromActivationDate, "dd/MM/yyyy");
            model.ToActivationDate = string.IsNullOrEmpty(model.ToActivationDate) ? null : Common.ConvertToSystemDate(model.ToActivationDate, "dd/MM/yyyy");
            DataSet ds = model.GetDownlineList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Name = r["Name"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Package = (r["ProductName"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.Mobile = (r["Mobile"].ToString());

                    lst.Add(obj);
                }
                model.lstassociate = lst;
            }
            List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            ViewBag.ddlStatus = AssociateStatus;
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.ddlleg = Leg;
            return View(model);
        }


        public ActionResult ReturnBenefitReport(Reports model)
        {

            List<SelectListItem> Status = Common.Status();
            ViewBag.Status = Status;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            List<Reports> lst1 = new List<Reports>();
            DataSet ds11 = model.ReturnBenefit();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["FirstName"].ToString();
                    Obj.AssociateLoginID = r["AssociateLoginID"].ToString();
                    Obj.AssociateName = r["AssociateName"].ToString();
                    Obj.ReturnBenefitStartDate = r["ReturnBenefitStartDate"].ToString();
                    // Obj.PlotNumber = r["PlotNumber"].ToString();
                    //Obj.DueDate = r["Installmentdate"].ToString();
                    // Obj.Status = r["IsPaid"].ToString();
                    //Obj.ReceiptNo = r["ReceiptNo"].ToString();
                    //Obj.TransactionNo = r["TransactionNo"].ToString();
                    //Obj.TransactionDate = r["TransactionDate"].ToString();
                    //Obj.BankBranch = r["BankBranch"].ToString();
                    //Obj.BankName = r["BankName"].ToString();
                    //Obj.InstAmt = r["InstAmt"].ToString();
                    lst1.Add(Obj);
                }

                model.lstassociate = lst1;
            }
            return View(model);
        }

        public ActionResult ReturnBenefitByLogin(Reports model)
        {

            //model.LoginId = LoginId;
            List<SelectListItem> Status = Common.Status();

            ViewBag.Status = Status;
            List<Reports> lst1 = new List<Reports>();
            DataSet ds11 = model.ReturnBenefitView();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["FirstName"].ToString();
                    Obj.PaymentDate = r["PaymentDate"].ToString();
                    Obj.PaymentMode = r["PaymentMode"].ToString();
                    Obj.PaidAmount = r["PaidAmount"].ToString();
                    Obj.DueDate = r["Installmentdate"].ToString();
                    Obj.Status = r["IsPaid"].ToString();
                    Obj.ReceiptNo = r["ReceiptNo"].ToString();
                    Obj.TransactionNo = r["TransactionNo"].ToString();
                    Obj.TransactionDate = r["TransactionDate"].ToString();
                    Obj.BankBranch = r["BankBranch"].ToString();
                    Obj.BankName = r["BankName"].ToString();
                    Obj.InstAmt = r["InstAmt"].ToString();
                    lst1.Add(Obj);
                }

                model.lstassociate = lst1;
            }
            return View(model);
        }
        public ActionResult ApproveAssociateList()
        {
            return View();
        }
        [HttpPost]
        [ActionName("ApproveAssociateList")]
        [OnAction(ButtonName = "GetList")]
        public ActionResult GetAssociateList(Reports model)
        {
            try
            {
                List<Reports> lstassociate = new List<Reports>();
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                DataSet ds = model.GetAssociateList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Reports obj = new Reports();
                        obj.Name = r["Name"].ToString();
                        obj.LoginId = r["LoginId"].ToString();
                        obj.AssociateID = r["AssociateID"].ToString();
                        obj.PaymentMode = (r["PaymentMode"].ToString());
                        obj.TransactionDate = r["TransactionDate"].ToString();
                        obj.PaymentDate = r["Entrydate"].ToString();
                        obj.TransactionNo = r["TransactionNo"].ToString();
                        obj.BankName = r["BankName"].ToString();
                        obj.BankBranch = (r["BankBranch"].ToString());
                        obj.Status = (r["Status"].ToString());
                        lstassociate.Add(obj);
                    }
                    model.lstassociate = lstassociate;
                }
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Approved(string FK_UserID)
        {
            string FormName = "'";
            string Controller = "";
            try
            {
                if (FK_UserID != null)
                {
                    Reports model = new Reports();
                    model.AssociateID = FK_UserID;
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    model.LoginId = Session["LoginId"].ToString();
                    DataSet ds = model.ApprovedAssociate();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["Approved"] = "Associate Approved  Successfull";
                            FormName = "ApproveAssociateList";
                            Controller = "AdminReports";
                        }
                        else
                        {
                            TempData["Approved"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            FormName = "ApproveAssociateList";
                            Controller = "AdminReports";
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction(FormName, Controller);
        }
        public ActionResult RejetAssociate(string FK_UserID)
        {
            string FormName = "'";
            string Controller = "";
            try
            {
                if (FK_UserID != null)
                {
                    Reports model = new Reports();
                    model.AssociateID = FK_UserID;
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    model.LoginId = Session["LoginId"].ToString();
                    DataSet ds = model.RejectAssociate();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["Approved"] = "Associate Rejected  Successfull";
                            FormName = "ApproveAssociateList";
                            Controller = "AdminReports";
                        }
                        else
                        {
                            TempData["Approved"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            FormName = "ApproveAssociateList";
                            Controller = "AdminReports";
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction(FormName, Controller);
        }
        public ActionResult DayBook(Reports model)
        {
            List<Reports> lstDayBook = new List<Reports>();
            List<SelectListItem> ExpenseType = Common.ExpenseType();
            ViewBag.ExpenseType = ExpenseType;
            List<SelectListItem> ddlexpensename = new List<SelectListItem>();
            ddlexpensename.Add(new SelectListItem { Text = "Select Expense Name", Value = "0" });
            ViewBag.ddlexpensename = ddlexpensename;
            DateTime now = DateTime.Now;
            DateTime firstDay = new DateTime(now.Year, now.Month, 01);
            model.FromDate = (firstDay).ToString("dd/MM/yyyy");
            //string dayOfFirstDay = firstDay.DayOfWeek.ToString();
            model.ToDate = DateTime.Now.ToString("dd/MM/yyyy");
            //string CurrentMonth = DateTime.Now.Month.ToString();
            model.NFromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.NToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.DayBookList1();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.ExpenseName = r["ExpenseName"].ToString();
                    obj.ExType = r["ExpenseType"].ToString();
                    obj.Remarks = r["Remarks"].ToString();
                    obj.TransactionNo = r["ChequeNo"].ToString();
                    obj.PaymentMode = (r["PaymentMode"].ToString());
                    obj.Status = r["TransactionStatus"].ToString();
                    obj.Transaction = r["TransactionType"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    //obj.Amount = r["amount"].ToString();
                    obj.ClosingDate = r["CheckDate"].ToString();

                    lstDayBook.Add(obj);
                }
                model.lstDayBook = lstDayBook;
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("DayBook")]
        [OnAction(ButtonName = "Search")]
        public ActionResult DayBookDetails(Reports model)
        {
            List<Reports> lstDayBook = new List<Reports>();
            List<SelectListItem> ExpenseType = Common.ExpenseType();
            ViewBag.ExpenseType = ExpenseType;
            List<SelectListItem> ddlexpensename = new List<SelectListItem>();
            ddlexpensename.Add(new SelectListItem { Text = "Select Expense Name", Value = "0" });
            ViewBag.ddlexpensename = ddlexpensename;
            model.ExpenseID = model.ExpenseID == "0" ? null : model.ExpenseID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.DayBookList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.ExpenseName = r["ExpenseName"].ToString();
                    obj.ExType = r["ExpenseType"].ToString();
                    obj.Remarks = r["Remarks"].ToString();
                    obj.TransactionNo = r["ChequeNo"].ToString();
                    obj.PaymentMode = (r["PaymentMode"].ToString());
                    obj.Status = r["TransactionStatus"].ToString();
                    obj.Transaction = r["TransactionType"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    //obj.Amount = r["amount"].ToString();
                    obj.ClosingDate = r["CheckDate"].ToString();

                    lstDayBook.Add(obj);
                }
                model.lstDayBook = lstDayBook;
            }

            return View(model);
        }
        public ActionResult PlotPaidList(Plot model)
        {
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.PlotNumber = string.IsNullOrEmpty(model.PlotNumber) ? null : model.PlotNumber;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
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

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }
        [HttpPost]
        [ActionName("PlotPaidList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetPaymentpaidlist(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            DataSet ds = model.PaymentPaidList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.ReceiptNo = r["ReceiptNo"].ToString();
                    //obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.PlotNumber = r["PlotInfo"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    obj.PaymentStatus = r["PaymentStatus"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_BookingDetailsId"].ToString());
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
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
            DataSet dsSector = model.GetSectorList();
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
            DataSet dsblock1 = model.GetBlockList();


            if (dsblock1 != null && dsblock1.Tables.Count > 0 && dsblock1.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock1.Tables[0].Rows)
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
        public ActionResult EditPaidList(string PaidID)
        {
            Plot model = new Plot();
            if (PaidID != "")
            {
                try
                {
                    model.PK_BookingDetailsId = Crypto.Decrypt(PaidID);
                    DataSet ds = model.PaymentPaidList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        model.PaidAmount = ds.Tables[0].Rows[0]["PaidAmount"].ToString();
                        model.PaymentDate = ds.Tables[0].Rows[0]["PaymentDate"].ToString();
                        model.PaymentMode = ds.Tables[0].Rows[0]["PaymentMode"].ToString();
                        model.CustomerLoginID = ds.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                        model.CustomerName = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                        model.TransactionNumber = ds.Tables[0].Rows[0]["TransactionNo"].ToString();
                        model.TransactionDate = ds.Tables[0].Rows[0]["TransactionDate"].ToString();
                        model.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                        model.BankBranch = ds.Tables[0].Rows[0]["BankBranch"].ToString();
                        model.ReceiptNo = ds.Tables[0].Rows[0]["ReceiptNo"].ToString();
                        model.PK_BookingDetailsId = ds.Tables[0].Rows[0]["PK_BookingDetailsId"].ToString();
                        model.PK_paymentID = ds.Tables[0].Rows[0]["PK_paymentID"].ToString();
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
            else
            {

            }
            Common obj = new Common();
            List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
            ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
            DataSet ds2 = obj.GetPaymentMode();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PaymentMode"].ToString() });
                }
            }

            ViewBag.ddlpaymentmode = ddlpaymentmode;
            return View(model);
        }
        [HttpPost]
        [ActionName("EditPaidList")]
        [OnAction(ButtonName = "UpdatePayment")]
        public ActionResult Update(Plot model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                model.PK_BookingDetailsId = model.PK_BookingDetailsId;
                model.PaymentDate = string.IsNullOrEmpty(model.PaymentDate) ? null : Common.ConvertToSystemDate(model.PaymentDate, "dd/MM/yyyy");
                model.TransactionDate = string.IsNullOrEmpty(model.TransactionDate) ? null : Common.ConvertToSystemDate(model.TransactionDate, "dd/MM/yyyy");
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.UpdatePlotPayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["Update"] = " Update successfully ";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["Update"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                    else
                    {
                        TempData["Update"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            FormName = "PlotPaidList";
            Controller = "AdminReports";

            return RedirectToAction(FormName, Controller);

        }

        public ActionResult EMIBookingReports()
        {
            Reports model = new Reports();
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            //model.PaymentMode = dsBookingDetails.Tables[0].Rows[0]["PaymentMode"].ToString();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion


            //List<Reports> lst = new List<Reports>();
            //DataSet ds = model.GetEMIBookingReportsDetails();
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        Reports obj = new Reports();
            //        obj.PK_BookingDetailsId = dr["PK_BookingDetailsId"].ToString();
            //        obj.PlotInfo = dr["PlotInfo"].ToString();
            //        obj.CustomerInfo = dr["CustomerInfo"].ToString();
            //        obj.AssociateInfo = dr["AssociateInfo"].ToString();
            //        obj.PaymentMode = dr["PaymentMode"].ToString();
            //        obj.PaymentDate = dr["PaymentDate"].ToString();
            //        obj.PaidAmount = dr["PaidAmount"].ToString();
            //        obj.TransactionNo = dr["TransactionNo"].ToString();
            //        obj.TransactionDate = dr["TransactionDate"].ToString();
            //        obj.BankName = dr["BankName"].ToString();
            //        obj.BankBranch = dr["BankBranch"].ToString();
            //        obj.Status = dr["Status"].ToString();
            //        obj.PaymentStatus = dr["PaymentStatus"].ToString();
            //        obj.PlotAmount = dr["PlotAmount"].ToString();
            //        obj.TotalPaid = dr["TotalPaid"].ToString();
            //        obj.RemainingAmount = dr["RemainingAmount"].ToString();
            //        lst.Add(obj);
            //    }
            //    model.EMIBookingReports = lst;
            //}
            return View();
        }

        [HttpPost]
        [ActionName("EMIBookingReports")]
        [OnAction(ButtonName = "Search")]
        public ActionResult EMIBookingReportDetails(Reports model)
        {
            List<Reports> lst = new List<Reports>();
            model.PK_paymentID = model.PK_paymentID == "0" ? null : model.PK_paymentID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetEMIBookingReportsDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.PlotInfo = dr["PlotInfo"].ToString();
                    obj.CustomerInfo = dr["CustomerInfo"].ToString();
                    obj.AssociateInfo = dr["AssociateInfo"].ToString();
                    obj.PaymentMode = dr["PaymentMode"].ToString();
                    obj.PK_BookingDetailsId = dr["PK_BookingDetailsId"].ToString();
                    obj.PaymentDate = dr["PaymentDate"].ToString();
                    obj.PaidAmount = dr["PaidAmount"].ToString();
                    obj.TransactionNo = dr["TransactionNo"].ToString();
                    obj.TransactionDate = dr["TransactionDate"].ToString();
                    obj.BankName = dr["BankName"].ToString();
                    obj.BankBranch = dr["BankBranch"].ToString();
                    obj.Status = dr["Status"].ToString();
                    obj.PaymentStatus = dr["PaymentStatus"].ToString();
                    obj.PlotAmount = dr["PlotAmount"].ToString();
                    obj.TotalPaid = dr["TotalPaid"].ToString();
                    obj.RemainingAmount = dr["RemainingAmount"].ToString();
                    lst.Add(obj);
                }
                model.EMIBookingReports = lst;
                ViewBag.PlotAmount = double.Parse(ds.Tables[0].Compute("sum(PlotAmount)", "").ToString()).ToString("n2");
                ViewBag.PaidAmount = double.Parse(ds.Tables[0].Compute("sum(PaidAmount)", "").ToString()).ToString("n2");
                ViewBag.TotalPaid = double.Parse(ds.Tables[0].Compute("sum(TotalPaid)", "").ToString()).ToString("n2");
                ViewBag.RemainingAmount = double.Parse(ds.Tables[0].Compute("sum(RemainingAmount)", "").ToString()).ToString("n2");
            }
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            //model.PaymentMode = dsBookingDetails.Tables[0].Rows[0]["PaymentMode"].ToString();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            return View(model);

        }

        public ActionResult ClosingWisePayoutDetails(string PK_PaidPayoutId)
        {
            AssociateBooking model = new AssociateBooking();
            model.PK_PaidPayoutId = PK_PaidPayoutId;
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.GetPayoutWiseIncomeDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.DisplayName = ds.Tables[0].Rows[0]["Name"].ToString();
                ViewBag.ClosingDate = ds.Tables[0].Rows[0]["ClosingDate"].ToString();
                ViewBag.PayoutNo = ds.Tables[0].Rows[0]["PayoutNo"].ToString();
                ViewBag.UserId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.PK_PaidPayoutId = r["PK_PaidPayoutId"].ToString();
                    obj.DisplayName = r["Name"].ToString();
                    obj.CustomerID = r["CustomerId"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.Income = r["Income"].ToString();
                    obj.CommPercentage = r["DifferencePerc"].ToString();
                    lst.Add(obj);
                }
                model.ClosingWisePayoutlist = lst;
            }
            return View(model);
        }


        public ActionResult IsKharijDakhilList(Reports model)
        {
            List<Reports> lst = new List<Reports>();
         
            DataSet ds = model.GetKharijDakhilList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Pk_KharijId = r["Pk_KharijId"].ToString();
                    obj.IsKharijDakhilDone = r["IsKharijDakhilDone"].ToString();
                    obj.KharijDakhilDate = r["KharijDakhilDate"].ToString();
                    obj.KharijDakhilRemarks = r["KharijDakhilRemarks"].ToString();
                    obj.CustomerId = r["CustomerID"].ToString();
                    obj.Customername = r["CustomerName"].ToString();
                    lst.Add(obj);
                }
                model.lstkharijdakhil = lst;
            }
            return View(model);
        }


        [HttpPost]
        [ActionName("IsKharijDakhilList")]
        [OnAction(ButtonName = "GetList")]
        public ActionResult IsKharijDakhilListAction(Reports model)
        {
            model.Pk_KharijId = model.Pk_KharijId == "0" ? null : model.Pk_KharijId;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            List<Reports> lst = new List<Reports>();
            DataSet ds = model.GetKharijDakhilList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Pk_KharijId = r["Pk_KharijId"].ToString();
                    obj.IsKharijDakhilDone = r["IsKharijDakhilDone"].ToString();
                    obj.KharijDakhilDate = r["KharijDakhilDate"].ToString();
                    obj.KharijDakhilRemarks = r["KharijDakhilRemarks"].ToString();
                    obj.CustomerId = r["CustomerID"].ToString();
                    obj.Customername = r["CustomerName"].ToString();
                    lst.Add(obj);
                }
                model.lstkharijdakhil = lst;
            }
            return View(model);
        }
        
        public ActionResult DeleteIsKharijDakhil(Reports model, string Id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                if (Id != null)
                {
                    model.Pk_KharijId = Id;
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = model.DeleteIsKharijDakhil();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {

                            TempData["Kharij"] = " Kharij Dakhil Deleted Successfully ";
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {

                            TempData["Kharij"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                        else
                        {
                            TempData["Kharij"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            FormName = "IsKharijDakhilList";
            Controller = "AdminReports";

            return RedirectToAction(FormName, Controller);

        }

        #region Income
        public ActionResult DirectIncome(Reports objreports)
        {
            List<Reports> lst = new List<Reports>();
            DataSet ds = objreports.GetDirectIncome();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.ToLoginId = r["ToLoginId"].ToString();
                    obj.ToUserName = r["ToUserName"].ToString();
                    obj.FromLoginId = r["FromLoginId"].ToString();
                    obj.FromUserName = r["FromUserName"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.IncomeType = (r["IncomeType"].ToString());
                    obj.Income = (r["Income"].ToString());
                    obj.Date = (r["CurrentDate"].ToString());
                    obj.Status = (r["PaidStatus"].ToString());
                    lst.Add(obj);
                }
                objreports.lstDirectIncome = lst;
            }
            return View(objreports);
        }

        [HttpPost]
        [ActionName("DirectIncome")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult DirectIncomeAction(Reports objreports)
        {
            List<Reports> lst = new List<Reports>();
            objreports.FromDate = string.IsNullOrEmpty(objreports.FromDate) ? null : Common.ConvertToSystemDate(objreports.FromDate, "dd/MM/yyyy");
            objreports.ToDate = string.IsNullOrEmpty(objreports.ToDate) ? null : Common.ConvertToSystemDate(objreports.ToDate, "dd/MM/yyyy");
            DataSet ds = objreports.GetDirectIncome();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.ToLoginId = r["ToLoginId"].ToString();
                    obj.ToUserName = r["ToUserName"].ToString();
                    obj.FromLoginId = r["FromLoginId"].ToString();
                    obj.FromUserName = r["FromUserName"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.IncomeType = (r["IncomeType"].ToString());
                    obj.Income = (r["Income"].ToString());
                    obj.Date = (r["CurrentDate"].ToString());
                    obj.Status = (r["PaidStatus"].ToString());
                    lst.Add(obj);
                }
                objreports.lstDirectIncome = lst;
            }
            return View(objreports);
        }

      
        public ActionResult DifferentialIncome(Reports objreports)
        {
            List<Reports> lst = new List<Reports>();
            DataSet ds = objreports.GetDifferentialIncome();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.ToLoginId = r["ToLoginId"].ToString();
                    obj.ToUserName = r["ToUserName"].ToString();
                    obj.FromLoginId = r["FromLoginId"].ToString();
                    obj.FromUserName = r["FromUserName"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.IncomeType = (r["IncomeType"].ToString());
                    obj.Income = (r["Income"].ToString());
                    obj.Date = (r["CurrentDate"].ToString());
                    obj.Status = (r["PaidStatus"].ToString());
                    lst.Add(obj);
                }
                objreports.lstDifferentialIncome = lst;
            }
            return View(objreports);
        }

        [HttpPost]
        [ActionName("DifferentialIncome")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult DifferentialIncomeAction(Reports objreports)
        {
            List<Reports> lst = new List<Reports>();
            objreports.FromDate = string.IsNullOrEmpty(objreports.FromDate) ? null : Common.ConvertToSystemDate(objreports.FromDate, "dd/MM/yyyy");
            objreports.ToDate = string.IsNullOrEmpty(objreports.ToDate) ? null : Common.ConvertToSystemDate(objreports.ToDate, "dd/MM/yyyy");
            DataSet ds = objreports.GetDifferentialIncome();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.ToLoginId = r["ToLoginId"].ToString();
                    obj.ToUserName = r["ToUserName"].ToString();
                    obj.FromLoginId = r["FromLoginId"].ToString();
                    obj.FromUserName = r["FromUserName"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.IncomeType = (r["IncomeType"].ToString());
                    obj.Income = (r["Income"].ToString());
                    obj.Date = (r["CurrentDate"].ToString());
                    obj.Status = (r["PaidStatus"].ToString());
                    lst.Add(obj);
                }
                objreports.lstDifferentialIncome = lst;
            }
            return View(objreports);
        }

        public ActionResult DirectLeadershipIncome(Reports objreports)
        {
            List<Reports> lst = new List<Reports>();
            DataSet ds = objreports.GetDirectLeadershipIncome();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.ToLoginId = r["ToLoginId"].ToString();
                    obj.ToUserName = r["ToUserName"].ToString();
                    obj.FromLoginId = r["FromLoginId"].ToString();
                    obj.FromUserName = r["FromUserName"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.IncomeType = (r["IncomeType"].ToString());
                    obj.Income = (r["Income"].ToString());
                    obj.Date = (r["CurrentDate"].ToString());
                    obj.Status = (r["PaidStatus"].ToString());
                    lst.Add(obj);
                }
                objreports.lstDirectLeadershipIncome = lst;
            }
            return View(objreports);
        }

        [HttpPost]
        [ActionName("DirectLeadershipIncome")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult DirectLeadershipIncomeAction(Reports objreports)
        {
            List<Reports> lst = new List<Reports>();
            objreports.FromDate = string.IsNullOrEmpty(objreports.FromDate) ? null : Common.ConvertToSystemDate(objreports.FromDate, "dd/MM/yyyy");
            objreports.ToDate = string.IsNullOrEmpty(objreports.ToDate) ? null : Common.ConvertToSystemDate(objreports.ToDate, "dd/MM/yyyy");
            DataSet ds = objreports.GetDirectLeadershipIncome();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.ToLoginId = r["ToLoginId"].ToString();
                    obj.ToUserName = r["ToUserName"].ToString();
                    obj.FromLoginId = r["FromLoginId"].ToString();
                    obj.FromUserName = r["FromUserName"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.IncomeType = (r["IncomeType"].ToString());
                    obj.Income = (r["Income"].ToString());
                    obj.Date = (r["CurrentDate"].ToString());
                    obj.Status = (r["PaidStatus"].ToString());
                    lst.Add(obj);
                }
                objreports.lstDirectLeadershipIncome = lst;
            }
            return View(objreports);
        }
        #endregion

        public ActionResult AssociateSelfdownBusinessReport()
        {
           return View();
        }
        [HttpPost]
        [ActionName("AssociateSelfdownBusinessReport")]
        [OnAction(ButtonName = "GetList")]
        public ActionResult AssociateSelfdownBusinessReportAction(Reports model)
        {
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            List<Reports> lst = new List<Reports>();
            DataSet ds = model.GetAssociateSelfdownBusinessReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.LoginId = r["LoginID"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.SelfBusiness = r["SelfBusiness"].ToString();
                    obj.TeamBusiness = r["TeamBusiness"].ToString();
                    lst.Add(obj);
                }
                model.lstAssSelfdownBusinessReport = lst;
            }
            return View(model);
        }

        public ActionResult CheckDesignationUpdateLog(Reports model)
        {
            #region ddlDesignation

            int desgnationCount = 0;
            List<SelectListItem> ddlDesignation = new List<SelectListItem>();
            DataSet dsdesignation = model.GetDesignationList();
            if (dsdesignation != null && dsdesignation.Tables.Count > 0 && dsdesignation.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsdesignation.Tables[0].Rows)
                {
                    if (desgnationCount == 0)
                    {
                        ddlDesignation.Add(new SelectListItem { Text = "Select Designation"});
                    }
                    ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                    desgnationCount = desgnationCount + 1;
                }
            }

            ViewBag.ddlDesignation = ddlDesignation;

            #endregion

            return View(model);
        }


        [HttpPost]
        [ActionName("CheckDesignationUpdateLog")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult SearchCheckDesignationUpdateLog(Reports model)
        {
            #region ddlDesignation

            int desgnationCount = 0;
            List<SelectListItem> ddlDesignation = new List<SelectListItem>();
            DataSet dsdesignation = model.GetDesignationList();
            if (dsdesignation != null && dsdesignation.Tables.Count > 0 && dsdesignation.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsdesignation.Tables[0].Rows)
                {
                    if (desgnationCount == 0)
                    {
                        ddlDesignation.Add(new SelectListItem { Text = "Select Designation"});
                    }
                    ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                    desgnationCount = desgnationCount + 1;
                }
            }

            ViewBag.ddlDesignation = ddlDesignation;

            #endregion

            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.DesignationID = model.DesignationID == "0" ? null : model.DesignationID;
            List<Reports> lst = new List<Reports>();
            DataSet ds = model.GetAutoUpdateDesignation();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.LoginId = r["LoginID"].ToString();
                    obj.OldDesignationName = r["OldDesignationName"].ToString();
                    obj.OldDesignationPercentage = r["OldDesignationPercentage"].ToString();
                    obj.OldBusiness = r["OldBusiness"].ToString();
                    obj.NewFk_DesignationId = r["NewFk_DesignationId"].ToString();
                    obj.NewDesignationName = r["NewDesignationName"].ToString();
                    obj.NewDesignationPercentage = r["NewDesignationPercentage"].ToString();
                    obj.NewBusiness = r["NewBusiness"].ToString();
                    obj.DesignationUpgradeDate = r["DesignationUpgradeDate"].ToString();
                    lst.Add(obj);
                }
                model.lstAutoUpdateDesignation = lst;
 
            }
            return View(model);
        }


        public ActionResult TopupReport(Reports model)
        {
            List<Reports> list = new List<Reports>();
            DataSet ds = model.GetTopupreport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.UpgradtionDate = r["UpgradtionDate"].ToString();
                    obj.Package = r["Package"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.BV = r["BV"].ToString();
                    obj.TopupBy = r["TopupBy"].ToString();
                    obj.Pk_InvestmentId = r["Pk_InvestmentId"].ToString();
                    obj.Status = r["Status"].ToString();
                    list.Add(obj);
                }
                model.TopupList = list;
            }

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
        [OnAction(ButtonName = "GetDetails")]
        [ActionName("TopupReport")]
        public ActionResult GetTopupReport(Reports model)
        {
            #region BindddlPackageName

            Reports mod = new Reports();
            List<SelectListItem> ddlPackageName = new List<SelectListItem>();
            ddlPackageName.Add(new SelectListItem { Text = "Select Package", Value = "0" });
            DataSet ds1 = mod.GetPackage();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    ddlPackageName.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                }
            }

            ViewBag.ddlPackageName = ddlPackageName;

            #endregion BindddlPackageName

            List<Reports> list = new List<Reports>();
            model.LoginId = model.LoginId;
            if (model.Fk_PackageId == "0")
            {
                model.Package = null;
            }
            else
            {
                model.Package = model.Fk_PackageId;
            }
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetTopupreport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.UpgradtionDate = r["UpgradtionDate"].ToString();
                    obj.Package = r["Package"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.BV = r["BV"].ToString();
                    obj.TopupBy = r["TopupBy"].ToString();
                    obj.Pk_InvestmentId = r["Pk_InvestmentId"].ToString();
                    obj.Status = r["Status"].ToString();
                    list.Add(obj);
                }
                model.TopupList = list;
            }
            return View(model);
        }
    }
}
