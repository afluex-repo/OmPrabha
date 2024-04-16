using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OmPrabha.Filter;
using OmPrabha.Models;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace OmPrabha.Controllers
{
    public class AssociateDashboardController : BaseController
    {
        // GET: AssociateDashboard
        public ActionResult AssociateDashBoard()
        {
            ViewBag.Name = Session["FullName"].ToString();
            AssociateBooking newdata = new AssociateBooking();
            try
            {
                List<AssociateBooking> lstInst = new List<AssociateBooking>();
                newdata.AssociateID = Session["Pk_userId"].ToString();
                DataSet dsInst = newdata.GetDueInstallmentList();
                if (dsInst != null && dsInst.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsInst.Tables[0].Rows)
                    {
                        AssociateBooking obj = new AssociateBooking();

                        obj.CustomerID = r["PK_UserId"].ToString();
                        obj.CustomerLoginID = r["LoginId"].ToString();
                        obj.CustomerName = r["FirstName"].ToString();
                        obj.PlotNumber = r["PlotInfo"].ToString();
                        obj.InstallmentNo = r["InstallmentNo"].ToString();
                        obj.InstallmentAmount = r["InstAmt"].ToString();


                        lstInst.Add(obj);
                    }

                    newdata.ListInstallment = lstInst;
                }
                DashBoard obj1 = new DashBoard();
                List<DashBoard> lstinvestment = new List<DashBoard>();
                obj1.Fk_UserId = Session["Pk_UserId"].ToString();
                DataSet ds = obj1.GetAssociateDashboard();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.TotalDownline = ds.Tables[0].Rows[0]["TotalDownline"].ToString();
                    ViewBag.TotalDirects = ds.Tables[0].Rows[0]["TotalDirect"].ToString();
                    //ViewBag.ProductWalletBalance = ds.Tables[0].Rows[0]["ProductWalletBalance"].ToString();
                    ViewBag.PayoutWalletBalance = ds.Tables[0].Rows[0]["PayoutWalletBalance"].ToString();
                    ViewBag.TotalPayout = ds.Tables[0].Rows[0]["TotalPayout"].ToString();
                    ViewBag.TotalDeduction = ds.Tables[0].Rows[0]["TotalDeduction"].ToString();
                    ViewBag.TotalActive = ds.Tables[0].Rows[0]["TotalActive"].ToString();
                    ViewBag.TotalInActive = ds.Tables[0].Rows[0]["TotalInActive"].ToString();
                    ViewBag.TotalPaidPayout = ds.Tables[0].Rows[0]["TotalPaidPayout"].ToString();

                    //if (ds.Tables[1].Rows.Count > 0 && ds.Tables[1].Rows[0]["PlotNumber"].ToString() != "")
                    //{

                    //    ViewBag.PlotNumber = ds.Tables[1].Rows[0]["PlotNumber"].ToString();
                    //}
                    //else
                    //{
                    //    ViewBag.PlotNumber = "N/A";
                    //}

                    ViewBag.PaidBusinessLeft = ds.Tables[2].Rows[0]["PaidBusinessLeft"].ToString();
                    ViewBag.PaidBusinessRight = ds.Tables[2].Rows[0]["PaidBusinessRight"].ToString();
                    ViewBag.TotalBusinessLeft = ds.Tables[2].Rows[0]["TotalBusinessLeft"].ToString();
                    ViewBag.TotalBusinessRight = ds.Tables[2].Rows[0]["TotalBusinessRight"].ToString();
                    ViewBag.CarryLeft = ds.Tables[2].Rows[0]["CarryLeft"].ToString();
                    ViewBag.CarryRight = ds.Tables[2].Rows[0]["CarryRight"].ToString();
                    //ViewBag.TopupDays = ds.Tables[4].Rows[0]["TopupDays"].ToString();
                    //ViewBag.ProductPaidBusinessLeft = ds.Tables[3].Rows[0]["PaidBusinessLeft"].ToString();
                    //ViewBag.ProductPaidBusinessRight = ds.Tables[3].Rows[0]["PaidBusinessRight"].ToString();
                    //ViewBag.ProductTotalBusinessLeft = ds.Tables[3].Rows[0]["TotalBusinessLeft"].ToString();
                    //ViewBag.ProductTotalBusinessRight = ds.Tables[3].Rows[0]["TotalBusinessRight"].ToString();
                    //ViewBag.ProductCarryLeft = ds.Tables[3].Rows[0]["CarryLeft"].ToString();
                    //ViewBag.ProductCarryRight = ds.Tables[3].Rows[0]["CarryRight"].ToString();
                }
                DashBoard model = new DashBoard();
                List<AssociateBooking> lstNews = new List<AssociateBooking>();
                newdata.NewsFor = "Associate";
                DataSet dsAssociate = newdata.GetNews();
                if (dsAssociate != null && dsAssociate.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsAssociate.Tables[0].Rows)
                    {
                        AssociateBooking obj = new AssociateBooking();
                        //   obj.PK_BookingId = r["PK_UserId"].ToString();

                        obj.PK_NewsID = r["PK_NewsID"].ToString();
                        obj.NewsHeading = r["NewsHeading"].ToString();
                        obj.NewsBody = r["NewsBody"].ToString();


                        //model.PlotStatus = dsblock.Tables[0].Rows[0]["Status"].ToString();

                        lstNews.Add(obj);
                    }

                    newdata.ListNEWS = lstNews;
                }
            }
            catch (Exception ex)
            {
                TempData["Dashboard"] = ex.Message;
            }
            return View(newdata);
        }

        #region ChangePasswordAssociate
        public ActionResult ChangePasswordAssociate()
        {

            return View();
        }
        [HttpPost]
        [ActionName("ChangePasswordAssociate")]
        [OnAction(ButtonName = "Change")]
        public ActionResult UpdatePassword(AssociateBooking obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.Password = Crypto.Encrypt(obj.Password);
                obj.NewPassword = Crypto.Encrypt(obj.NewPassword);
                obj.UpdatedBy = Session["Pk_userId"].ToString();
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
                        FormName = "ChangePasswordAssociate";
                        Controller = "AssociateDashboard";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Login"] = ex.Message;
                FormName = "ChangePasswordAssociate";
                Controller = "AssociateDashboard";
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion
        #region Tree
        public ActionResult Tree()
        {
            return View();
        }
        #endregion
        #region PayoutRequest
        public ActionResult PayoutRequest(AssociateBooking model)
        {
            model.UserID = Session["Pk_userId"].ToString();
            DataSet Ds = model.GetPayoutBalance();
            ViewBag.Balance = Ds.Tables[0].Rows[0]["Balance"].ToString();

            return View(model);
        }

        [HttpPost]
        [ActionName("PayoutRequest")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SavePayoutRequest(AssociateBooking obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                obj.AddedBy = Session["Pk_userId"].ToString();
                obj.UserID = Session["LoginId"].ToString();
                DataSet ds = obj.SavePayoutRequest();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["PayoutReq"] = " Payout requested successfully !";
                        try
                        {
                            string mob = ds.Tables[0].Rows[0]["Column1"].ToString();
                            string name = ds.Tables[0].Rows[0]["Column3"].ToString();
                            string payout = ds.Tables[0].Rows[0]["Column2"].ToString();
                            string TempId = "1707166296876400822";
                            BLSMS.SendSMS(mob, "Dear "+ name + ", Your Payout Request of Rs"+ payout + " has been processed successfully. Your amount will be credited in your account within 1 to 5 working days. OmPrabha", TempId);
                            
                        }
                        catch
                        {

                        }

                    }
                    else
                    {
                        TempData["PayoutReq"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["PayoutReq"] = ex.Message;
            }
            FormName = "PayoutRequest";
            Controller = "AssociateDashboard";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult PayoutDetails(AssociateBooking model)

        {
            model.UserID = Session["Pk_userId"].ToString();
            //model.FromDate = Common.ConvertToSystemDate(DateTime.Today.ToShortDateString(), "dd/MM/yyyy");
            //model.ToDate = Common.ConvertToSystemDate(DateTime.Today.ToShortDateString(), "dd/MM/yyyy");
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.PayoutDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
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
        public ActionResult PayoutDetailsBy(AssociateBooking model)
        {
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.UserID = Session["Pk_userId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.PayoutDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
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
        public ActionResult PayoutLedger(AssociateBooking model)
        {
            model.UserID = Session["Pk_userId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.PayoutLedger();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.PayoutWalletID = r["Pk_PayoutWalletId"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    obj.Credit = r["Credit"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.TType = r["TransactionType"].ToString();
                    obj.Debit = r["Debit"].ToString();


                    lst.Add(obj);
                }
                ViewBag.Credit = double.Parse(ds.Tables[0].Compute("sum(Credit)", "").ToString()).ToString("n2");
                ViewBag.Debit = double.Parse(ds.Tables[0].Compute("sum(Debit)", "").ToString()).ToString("n2");
                model.lstPlot = lst;
            }
            return View(model);
        }
        #endregion
        #region KYC

        public ActionResult KYCDocuments()
        {
            AssociateBooking model = new AssociateBooking();

            model.UserID = Session["Pk_userId"].ToString();
            AssociateBooking obj = new AssociateBooking();
            DataSet ds = model.GetKYCDocuments();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.AdharNumber = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                obj.AdharImage = ds.Tables[0].Rows[0]["AdharImage"].ToString();
                obj.AdharBackSideImage = ds.Tables[0].Rows[0]["AdharBackSideImage"].ToString();
                obj.AdharStatus = "Status : " + ds.Tables[0].Rows[0]["AdharStatus"].ToString();
                obj.AdharBackSideStatus = "Status : " + ds.Tables[0].Rows[0]["AdharBackSideStatus"].ToString();
                obj.PanNumber = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                obj.PanImage = ds.Tables[0].Rows[0]["PanImage"].ToString();
                obj.PanStatus = "Status : " + ds.Tables[0].Rows[0]["PanStatus"].ToString();
                obj.DocumentNumber = ds.Tables[0].Rows[0]["DocumentNumber"].ToString();
                obj.DocumentImage = ds.Tables[0].Rows[0]["DocumentImage"].ToString();
                obj.DocumentStatus = "Status : " + ds.Tables[0].Rows[0]["DocumentStatus"].ToString();
                obj.ProfilePic = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                obj.ProfileStatus = "Status : " + ds.Tables[0].Rows[0]["ProfileStatus"].ToString();
            }
            return View(obj);
        }

        [HttpPost]
        [ActionName("KYCDocuments")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult KYCDocuments(IEnumerable<HttpPostedFileBase> postedFile, AssociateBooking obj)
        {
            string FormName = "";
            string Controller = "";
            int count = 0;
            try
            {
                foreach (var file in postedFile)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        if (count == 0)
                        {
                            obj.AdharImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath(obj.AdharImage)));
                        }
                        if (count == 1)
                        {
                            obj.AdharBackSideImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath(obj.AdharBackSideImage)));
                        }
                        if (count == 2)
                        {
                            obj.PanImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath(obj.PanImage)));
                        }
                        if (count == 3)
                        {
                            obj.DocumentImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath(obj.DocumentImage)));
                        }
                        if (count == 4)
                        {
                            obj.ProfilePic = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath(obj.ProfilePic)));
                        }
                    }
                    count++;
                }
                obj.UserID = Session["Pk_userId"].ToString();

                DataSet ds = obj.UploadKYCDocuments();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["DocumentUpload"] = "Document uploaded successfully..";
                        FormName = "KYCDocuments";
                        Controller = "AssociateDashboard";
                    }
                    else
                    {
                        TempData["DocumentUpload"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "KYCDocuments";
                        Controller = "AssociateDashboard";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["DocumentUpload"] = ex.Message;
                FormName = "KYCDocuments";
                Controller = "AssociateDashboard";
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion
        #region Enquiry
        public ActionResult EnquiryList(Master model)
        {

            List<Master> lst = new List<Master>();

            DataSet ds = model.EnquiryList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.EnquiryID = r["PK_EnquiryMasterId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Details = r["Details"].ToString();

                    lst.Add(obj);
                }

                model.lstBlock1 = lst;
            }
            return View(model);

        }
        [HttpPost]
        [ActionName("EnquiryList")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveEnquiry(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_userId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveEnquiry();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Enquiry"] = "Enquiry saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Enquiry"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Enquiry"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Enquiry"] = ex.Message;
            }
            return RedirectToAction("EnquiryList", "AssociateDashboard");
        }

        #endregion
        #region UserReward
        public ActionResult UserReward(AssociateBooking model)
        {

            model.UserID = Session["Pk_userId"].ToString();
            model.RewardID = "1";

            List<AssociateBooking> lst = new List<AssociateBooking>();

            DataSet ds = model.RewardList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();

                    obj.Status = r["Status"].ToString();
                    obj.QualifyDate = r["QualifyDate"].ToString();
                    obj.RewardImage = r["RewardImage"].ToString();
                    obj.RewardName = r["RewardName"].ToString();
                    obj.Contact = r["BackColor"].ToString();
                    obj.PK_RewardItemId = r["PK_RewardItemId"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }

            return View(model);
        }

        public ActionResult ClaimReward(string id)
        {
            AssociateBooking obj = new AssociateBooking();
            try
            {
                obj.PK_RewardItemId = id;
                obj.Status = "Claim";
                obj.Fk_UserId = Session["Pk_UserId"].ToString();
                DataSet ds = obj.ClaimReward();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Rewardmsg"] = "Reward Claimed";
                    }
                    else
                    {
                        TempData["Rewardmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Rewardmsg"] = ex.Message;
            }
            return RedirectToAction("UserReward");
        }
        public ActionResult SkipReward(string id)
        {
            AssociateBooking obj = new AssociateBooking();
            try
            {
                obj.PK_RewardItemId = id;
                obj.Status = "Skip";
                obj.Fk_UserId = Session["Pk_UserId"].ToString();
                DataSet ds = obj.ClaimReward();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Rewardmsg"] = "Reward Skipped";
                    }
                    else
                    {
                        TempData["Rewardmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Rewardmsg"] = ex.Message;
            }
            return RedirectToAction("UserReward");
        }
        #endregion
        #region AssociateBookings
        public ActionResult AssociateBookingList(AssociateBooking model)
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

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }
        [HttpPost]
        [ActionName("AssociateBookingList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult BookingList(AssociateBooking model)
        {
            model.LoginId = Session["LoginId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();
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
                    AssociateBooking obj = new AssociateBooking();
                    obj.BookingStatus = r["BookingStatus"].ToString();
                    obj.PK_BookingId = r["PK_BookingId"].ToString();
                    obj.BranchID = r["BranchID"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.CustomerID = r["CustomerID"].ToString();
                    obj.CustomerLoginID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.AssociateID = r["AssociateID"].ToString();
                    obj.AssociateLoginID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.PlotNumber = r["PlotInfo"].ToString();
                    obj.BookingDate = r["BookingDate"].ToString();
                    obj.BookingAmount = r["BookingAmt"].ToString();
                    obj.PaymentPlanID = r["PlanName"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PlotArea = r["PlotArea"].ToString();
                    obj.Discount = r["Discount"].ToString();
                    obj.PlotAmount = r["PlotAmount"].ToString();
                    obj.NetPlotAmount = r["NetPlotAmount"].ToString();
                    obj.PK_PLCCharge = r["PLCCharge"].ToString();
                    obj.PlotRate = r["PlotRate"].ToString();
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

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }
        #endregion
        #region Customer Ledger Report
        public ActionResult CustomerLedgerReport(AssociateBooking model)
        {


            //AssociateBooking model = new AssociateBooking();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.PlotNumber = string.IsNullOrEmpty(model.PlotNumber) ? null : model.PlotNumber;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            //model.PK_BookingId = PK_BookingId;

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

        public ActionResult Details(string BookingNumber, string LoginId, string SiteID, string SectorID, string BlockID, string PlotNumber)
        {
            AssociateBooking model = new AssociateBooking();
            model.LoginId = Session["LoginId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();
            model.SiteID = SiteID == "0" ? null : SiteID;
            model.SectorID = SectorID == "0" ? null : SectorID;
            model.BlockID = BlockID == "0" ? null : BlockID;
            model.BookingNumber = string.IsNullOrEmpty(BookingNumber) ? null : BookingNumber;
            model.PlotNumber = string.IsNullOrEmpty(PlotNumber) ? null : PlotNumber;
            DataSet dsblock = model.FillDetails();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {

                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
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

                }

            }
            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in dsblock.Tables[1].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
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
        public ActionResult LedgerReport(string BookingNumber)
        {
            string FormName = "";
            string Controller = "";
            AssociateBooking model = new AssociateBooking();
            model.LoginId = Session["LoginId"].ToString();
            model.BookingNumber = string.IsNullOrEmpty(BookingNumber) ? null : BookingNumber;
            List<AssociateBooking> lstBlockList = new List<AssociateBooking>();
            DataSet dsblock = model.FillDetails();
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
                FormName = "CustomerLedgerReport";
                Controller = "AssociateDashboard";
            }
            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in dsblock.Tables[1].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();

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
                    Controller = "AssociateDashboard";
                }
                model.lstPlot = lstBlockList;
            }
            else
            {
                TempData["Login"] = "Data Not Found";

            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion
        #region AssociateUpline
        public ActionResult AssociateUplineDetail(AssociateBooking model)
        {

            model.LoginId = Session["LoginId"].ToString();

            List<AssociateBooking> lst = new List<AssociateBooking>();

            DataSet ds = model.GetDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.Percentage = r["Percentage"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }

            return View(model);
        }
        #endregion
        #region AssociateDownline
        public ActionResult AssociateDownlineDetail(AssociateBooking model)
        {

            model.LoginId = Session["LoginId"].ToString();

            List<AssociateBooking> lst = new List<AssociateBooking>();

            DataSet ds = model.GetDownlineDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.Percentage = r["Percentage"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }

            return View(model);
        }
        #endregion
        #region UnpaidIncomeReport
        public ActionResult UnpaidIncome(AssociateBooking model)
        {
            model.UserID = Session["Pk_userId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.UnpaidIncomes();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.FromDate = r["CurrentDate"].ToString();
                    obj.FromID = r["FromLoginId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToID = r["ToLoginId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.Amount = r["BusinessAmount"].ToString();
                    obj.DifferencePercentage = r["DifferencePerc"].ToString();
                    obj.Income = r["Income"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("UnpaidIncome")]
        [OnAction(ButtonName = "Search")]
        public ActionResult UnpaidIncomeBy(AssociateBooking model)
        {
            model.UserID = Session["Pk_userId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.UnpaidIncomes();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.FromDate = r["CurrentDate"].ToString();
                    obj.FromID = r["FromLoginId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToID = r["ToLoginId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.Amount = r["BusinessAmount"].ToString();
                    obj.DifferencePercentage = r["DifferencePerc"].ToString();
                    obj.Income = r["Income"].ToString();


                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            return View(model);
        }
        #endregion
        #region PayoutRequestReport
        public ActionResult PayoutRequestReport(AssociateBooking model)
        {
            model.Status = model.Status == "0" ? null : model.Status;
            model.UserID = Session["LoginId"].ToString();
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

            #region ddl 
            List<SelectListItem> RequestStatus = Common.RequestStatus();
            ViewBag.RequestStatus = RequestStatus;
            #endregion  

            return View(model);
        }
        [HttpPost]
        [ActionName("PayoutRequestReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult PayoutRequestReportBy(AssociateBooking model)
        {
            model.UserID = Session["LoginId"].ToString();
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
            #region ddl 
            List<SelectListItem> RequestStatus = Common.RequestStatus();
            ViewBag.RequestStatus = RequestStatus;
            #endregion  
            return View(model);
        }
        #endregion
        public ActionResult GetSiteDetails(string SiteID)
        {
            try
            {
                Master model = new Master();
                model.SiteID = SiteID;

                #region GetSiteRate
                DataSet dsSiteRate = model.GetSiteList();
                if (dsSiteRate != null)
                {
                    model.Rate = dsSiteRate.Tables[0].Rows[0]["Rate"].ToString();
                    model.Result = "yes";
                }
                #endregion
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
                #region SitePLCCharge
                List<Master> lstPlcCharge = new List<Master>();
                DataSet dsPlcCharge = model.GetPLCChargeList();

                if (dsPlcCharge != null && dsPlcCharge.Tables.Count > 0)
                {
                    foreach (DataRow r in dsPlcCharge.Tables[0].Rows)
                    {
                        Master obj = new Master();
                        obj.SiteName = r["SiteName"].ToString();
                        obj.PLCName = r["PLCName"].ToString();
                        obj.PLCCharge = r["PLCCharge"].ToString();
                        obj.PLCID = r["PK_PLCID"].ToString();

                        lstPlcCharge.Add(obj);
                    }
                    model.lstPLC = lstPlcCharge;
                }
                #endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public ActionResult GetPlotBookingOnGraph()
        {

            List<AssociateBooking> dataList3 = new List<AssociateBooking>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            AssociateBooking newdata = new AssociateBooking();
            newdata.AssociateID = Session["Pk_userId"].ToString();

            Ds = newdata.GetDetailsForBarGraph();
            if (Ds.Tables.Count > 0)
            {
                ViewBag.TotalUsers = Ds.Tables[0].Rows.Count;
                int count = 0;
                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    AssociateBooking details = new AssociateBooking();


                    details.TotalBooking = (dr["TotalBooking"].ToString());
                    details.Month = (dr["Month"].ToString());


                    dataList3.Add(details);

                    count++;
                }
            }
            return Json(dataList3, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGraphDetailsOfPlot()
        {
            List<AssociateBooking> dataList = new List<AssociateBooking>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            AssociateBooking newdata = new AssociateBooking();
            newdata.LoginId = Session["LoginId"].ToString();
            Ds = newdata.BindGraphDetails();
            if (Ds.Tables.Count > 0)
            {

                int count = 0;
                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    AssociateBooking details = new AssociateBooking();


                    details.Total = (dr["Total"].ToString());
                    details.Status = (dr["Status"].ToString());


                    dataList.Add(details);

                    count++;
                }
            }
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AssociateTree(AssociateBooking model, string AssociateID)
        {
            if (AssociateID == "1")
            {
                model.Fk_UserId = Session["Pk_UserId"].ToString();
            }
            else if (AssociateID != null)
            {
                model.Fk_UserId = AssociateID;
            }
            else
            {
                model.Fk_UserId = Session["Pk_UserId"].ToString();
            }

            
            //if (AssociateID != null)
            //{
            //    model.Fk_UserId = AssociateID;
            //}
            //else
            //{
            //    model.Fk_UserId = Session["Pk_UserId"].ToString();
            //}


            List<AssociateBooking> lst = new List<AssociateBooking>();

            DataSet ds = model.GetDownlineTree();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.Fk_SponsorId = ds.Tables[0].Rows[0]["Fk_SponsorId"].ToString();
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.Fk_UserId = r["Pk_UserId"].ToString();
                    obj.Fk_SponsorId = r["Fk_SponsorId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();

                    obj.Status = r["Status"].ToString();
                    obj.ActiveStatus = r["ActiveStatus"].ToString();
                    lst.Add(obj);

                }
                model.lstPlot = lst;
            }


            return View(model);
        }
        public ActionResult ViewProfile()
        {
            Profile objprofile = new Profile();

            List<Profile> lstprofile = new List<Profile>();
            objprofile.LoginId = Session["LoginId"].ToString();
            Profile obj = new Profile();
            DataSet ds = objprofile.GetUserProfile();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                obj.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                obj.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                obj.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                obj.JoiningDate = ds.Tables[0].Rows[0]["JoiningDate"].ToString();
                obj.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                obj.EmailId = ds.Tables[0].Rows[0]["Email"].ToString();
                obj.SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString();
                obj.SponsorName = ds.Tables[0].Rows[0]["SponsorName"].ToString();
                obj.AccountNumber = ds.Tables[0].Rows[0]["AccountNo"].ToString();
                obj.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                obj.BankBranch = ds.Tables[0].Rows[0]["BankBranch"].ToString();
                obj.IFSC = ds.Tables[0].Rows[0]["IFSC"].ToString();
                obj.DOB = ds.Tables[0].Rows[0]["DOB"].ToString();
                obj.ProfilePicture = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                obj.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                obj.PanNumber = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                obj.AccountHolderName = ds.Tables[0].Rows[0]["BankHolderName"].ToString();
                obj.PanStatus = ds.Tables[0].Rows[0]["PanStatus"].ToString();
                obj.AdharStatus = ds.Tables[0].Rows[0]["AdharStatus"].ToString();
            }
            return View(obj);
        }
        [HttpPost]
        [ActionName("ViewProfile")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateProfile(HttpPostedFileBase fileProfilePicture, Profile obj)
        {
            string FormName = "";
            string Controller = "";
            obj.DOB = string.IsNullOrEmpty(obj.DOB) ? null : Common.ConvertToSystemDate(obj.DOB, "dd/MM/yyyy");
            
            try
            {
                if (fileProfilePicture != null)
                {
                    obj.ProfilePicture = "/images/ProfilePicture/" + Guid.NewGuid() + Path.GetExtension(fileProfilePicture.FileName);
                    fileProfilePicture.SaveAs(Path.Combine(Server.MapPath(obj.ProfilePicture)));
                }

                //Profile objProfile = new Profile();
                obj.PK_UserID = Session["Pk_userId"].ToString();
                DataSet ds = obj.UpdateProfile();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["UpdateProfile"] = "Profile updated successfully..";
                        FormName = "ViewProfile";
                        Controller = "AssociateDashboard";
                        //return View();
                    }
                    else
                    {
                        TempData["UpdateProfile"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ViewProfile";
                        Controller = "AssociateDashboard";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateProfile"] = ex.Message;
                FormName = "ViewProfile";
                Controller = "AssociateDashboard";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult Registration(string UserID)
        {
            Home obj = new Home();

            if (UserID != null)
            {

                obj.UserID = Crypto.Decrypt(UserID);
                //model.UserID = UserID;
                DataSet dsPlotDetails = obj.GetList();
                if (dsPlotDetails != null && dsPlotDetails.Tables.Count > 0)
                {
                    obj.UserID = UserID;
                    obj.SponsorId = dsPlotDetails.Tables[0].Rows[0]["SponsorId"].ToString();
                    obj.SponsorName = dsPlotDetails.Tables[0].Rows[0]["SponsorName"].ToString();
                    obj.LoginId = dsPlotDetails.Tables[0].Rows[0]["AssociateID"].ToString();
                    obj.AssociateName = dsPlotDetails.Tables[0].Rows[0]["AssociateName"].ToString();
                    obj.FirstName = dsPlotDetails.Tables[0].Rows[0]["FirstName"].ToString();
                    obj.LastName = dsPlotDetails.Tables[0].Rows[0]["LastName"].ToString();
                    obj.DesignationID = dsPlotDetails.Tables[0].Rows[0]["FK_DesignationID"].ToString();
                    obj.DesignationName = dsPlotDetails.Tables[0].Rows[0]["DesignationName"].ToString();
                    obj.BranchID = dsPlotDetails.Tables[0].Rows[0]["Fk_BranchId"].ToString();
                    obj.MobileNo = dsPlotDetails.Tables[0].Rows[0]["Mobile"].ToString();
                    obj.Email = dsPlotDetails.Tables[0].Rows[0]["Email"].ToString();
                    obj.State = dsPlotDetails.Tables[0].Rows[0]["State"].ToString();
                    obj.City = dsPlotDetails.Tables[0].Rows[0]["City"].ToString();
                    obj.Address = dsPlotDetails.Tables[0].Rows[0]["Address"].ToString();
                    obj.Pincode = dsPlotDetails.Tables[0].Rows[0]["PinCode"].ToString();
                    obj.PanCard = dsPlotDetails.Tables[0].Rows[0]["PanNumber"].ToString();
                    obj.BranchName = dsPlotDetails.Tables[0].Rows[0]["BranchName"].ToString();
                    // objViewBag.Disabled = "disabled";
                    obj.AdharNumber = dsPlotDetails.Tables[0].Rows[0]["AdharNumber"].ToString();
                    obj.BankAccountNo = dsPlotDetails.Tables[0].Rows[0]["MemberAccNo"].ToString();
                    obj.BankName = dsPlotDetails.Tables[0].Rows[0]["MemberBankName"].ToString();
                    obj.BankBranch = dsPlotDetails.Tables[0].Rows[0]["MemberBranch"].ToString();
                    obj.IFSCCode = dsPlotDetails.Tables[0].Rows[0]["IFSCCode"].ToString();
                    obj.ProfilePic = dsPlotDetails.Tables[0].Rows[0]["ProfilePic"].ToString();
                    obj.Signature = dsPlotDetails.Tables[0].Rows[0]["Signature"].ToString();
                }
            }

            else
            {
                // ViewBag.Disabled = "";

            }

            #region ForQueryString
            if (Request.QueryString["Pid"] != null)
            {
                obj.SponsorId = Request.QueryString["Pid"].ToString();
            }
            if (Request.QueryString["lg"] != null)
            {
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
            }
            if (Request.QueryString["Pid"] != null)
            {
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
                ViewBag.RightChecked = "";
                ViewBag.LeftChecked = "checked";
            }
            #endregion ForQueryString

            //obj.SponsorId = Session["LoginId"].ToString();
            //   obj.SponsorName = Session["FullName"].ToString();
            #region ddlgender
            List<SelectListItem> ddlgender = Common.BindGender();
            ViewBag.ddlgender = ddlgender;
            #endregion ddlgender
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
            return View(obj);
        }
        public ActionResult RegistrationAction(string SponsorId, string FirstName, string LastName, string Email, string MobileNo, string PanCard, string Gender, string OTP, string Leg, string Amount, string PaymentMode, string TransactionNo, string BankName, string BankBranch, string TransactionDate)
        {
            Home obj = new Home();

            try
            {
                obj.SponsorId = SponsorId;
                obj.FirstName = FirstName;
                obj.LastName = LastName;
                obj.Email = Email;
                obj.MobileNo = MobileNo;
                obj.PanCard = PanCard;
                obj.RegistrationBy = "Web";
                obj.Gender = Gender;
                obj.Leg = Leg;
                obj.PaymentMode = PaymentMode;
                obj.Amount = Amount;
                obj.TransactionNo = TransactionNo;
                obj.BankName = BankName;
                obj.BankBranch = BankBranch;
                obj.TransactionDate = string.IsNullOrEmpty(TransactionDate) ? null : Common.ConvertToSystemDate(TransactionDate, "dd/MM/yyyy");
                string password = Common.GenerateRandom();
                obj.Password = Crypto.Encrypt(password);

                DataSet ds = obj.AssociateRegistration();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["AssociateName"] = ds.Tables[0].Rows[0]["Name"].ToString();
                        Session["PassWord"] = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        Session["MobileNo"] = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        try
                        {
                            //  string str2 = BLSMS.Registration(ds.Tables[0].Rows[0]["Name"].ToString(), ds.Tables[0].Rows[0]["LoginId"].ToString(), Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()));
                            //  BLSMS.SendSMSNew(MobileNo, str2);
                        }
                        catch { }
                        obj.Response = "1";

                    }
                    else
                    {
                        obj.Response = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Response = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ActionName("Registration")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateAssociateProfile(Home obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateAssociateProfileByAdmin();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["UpdateProfilebyadmin"] = "Profile updated successfully..";
                        FormName = "AssociateList";
                        Controller = "TraditionalAssociate";
                        //return View();
                    }
                    else
                    {
                        TempData["UpdateProfilebyadmin"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AssociateList";
                        Controller = "TraditionalAssociate";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateProfilebyadmin"] = ex.Message;
                FormName = "AssociateList";
                Controller = "TraditionalAssociate";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult GetSponserDetails(string ReferBy)
        {
            Common obj = new Common();
            obj.ReferBy = ReferBy;
            DataSet ds = obj.GetMemberDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                obj.DisplayName = ds.Tables[0].Rows[0]["FullName"].ToString();

                obj.Result = "Yes";

            }
            else { obj.Result = "Invalid SponsorId"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmRegistration()
        {
            return View();
        }

        #region RegistrationDetails
        public ActionResult RegistrationDetails(TraditionalAssociate model)
        {
            //= new TraditionalAssociate();
            //List<TraditionalAssociate> lst = new List<TraditionalAssociate>();
            //DataSet ds = model.GetList();

            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in ds.Tables[0].Rows)
            //    {
            //        TraditionalAssociate obj = new TraditionalAssociate();

            //        obj.UserID = r["PK_UserId"].ToString();
            //        obj.AssociateID = r["AssociateId"].ToString();
            //        obj.AssociateName = r["AssociateName"].ToString();
            //        obj.SponsorID = r["SponsorId"].ToString();
            //        obj.SponsorName = r["SponsorName"].ToString();
            //        //   obj.LoginID = r["LoginId"].ToString();
            //        //  obj.DesignationID = r["FK_DesignationID"].ToString();
            //        // obj.FirstName = r["AName"].ToString();
            //        obj.isBlocked = r["isBlocked"].ToString();
            //        obj.Contact = r["Mobile"].ToString();
            //        obj.Email = r["Email"].ToString();
            //        obj.PanNo = r["PanNumber"].ToString();
            //        obj.City = r["City"].ToString();
            //        obj.State = r["State"].ToString();
            //        obj.Status = r["MemberStatus"].ToString();
            //        obj.Address = r["Address"].ToString();
            //        // obj.PanNo = r["PanNumber"].ToString();
            //        obj.BranchName = r["BranchName"].ToString();
            //        obj.DesignationName = r["DesignationName"].ToString();
            //        obj.EncryptKey = Crypto.Encrypt(r["PK_UserId"].ToString());
            //        obj.Password = Crypto.Decrypt(r["Password"].ToString());
            //        lst.Add(obj);
            //    }
            //    model.lstTrad = lst;
            //}
            return View(model);
        }
        [HttpPost]
        [ActionName("RegistrationDetails")]
        [OnAction(ButtonName = "btnSearchCustomer")]
        public ActionResult AssociateListbyId(TraditionalAssociate model)
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
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    lst.Add(obj);
                }
                model.lstTrad = lst;
            }
            return View(model);
        }
        #endregion
        #region Customer
        public ActionResult CustomerList()
        {
            return View();
        }
        [HttpPost]
        [ActionName("CustomerList")]
        [OnAction(ButtonName = "SearchCustomer")]
        public ActionResult CustomerListDetails(AssociateBooking model)
        {
            List<AssociateBooking> lstCustomer = new List<AssociateBooking>();
            model.JoiningFromDate = string.IsNullOrEmpty(model.JoiningFromDate) ? null : Common.ConvertToSystemDate(model.JoiningFromDate, "dd/MM/yyyy");
            model.JoiningToDate = string.IsNullOrEmpty(model.JoiningToDate) ? null : Common.ConvertToSystemDate(model.JoiningToDate, "dd/MM/yyyy");
            model.UserID = Session["Pk_userId"].ToString();
            DataSet ds = model.GetListCustomer();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.Fk_SponsorId = r["FK_SponsorId"].ToString();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.UserID = r["PK_UserId"].ToString();
                    obj.SponsorID = r["SponsorId"].ToString();
                    obj.SponsorName = r["SponsorName"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.LastName = r["LastName"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    obj.City = r["City"].ToString();
                    obj.State = r["State"].ToString();
                    obj.Address = r["Address"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.JoiningFromDate = r["JoiningDate"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_UserId"].ToString());
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
            
                    
                    lstCustomer.Add(obj);
                }
                model.lstCustomer = lstCustomer;
            }
            return View(model);
        }
        #endregion
        public ActionResult TreeView()
        {
            return View();
        }


       
        /////////////////////////////////////////////////////////////////////////////////////
      


        public ActionResult GetStateCity(string Pincode)
        {
            try
            {
                Common model = new Common();
                model.Pincode = Pincode;

                #region GetStateCity
                DataSet dsStateCity = model.GetStateCity();
                if (dsStateCity != null && dsStateCity.Tables[0].Rows.Count > 0)
                {
                    model.State = dsStateCity.Tables[0].Rows[0]["State"].ToString();
                    model.City = dsStateCity.Tables[0].Rows[0]["City"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.State = model.City = "";
                    model.Result = "no";
                }
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }


        public ActionResult GetSponsorName(string SponsorID)
        {
            try
            {
                AssociateBooking model = new AssociateBooking();
                model.LoginId = SponsorID;

                #region GetSiteRate
                DataSet dssponsor = model.GetAssociateListss();
                if (dssponsor != null && dssponsor.Tables[0].Rows.Count > 0)
                {
                    model.SponsorName = dssponsor.Tables[0].Rows[0]["Name"].ToString();
                    model.UserID = dssponsor.Tables[0].Rows[0]["PK_UserID"].ToString();
                    model.SponsorDesignationID = dssponsor.Tables[0].Rows[0]["FK_DesignationID"].ToString();
                    model.Percentage = dssponsor.Tables[0].Rows[0]["Percentage"].ToString();
                    int desgnationCount = 0;

                    DataSet dsdesignation = model.GetDesignationList();
                    List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                    if (dsdesignation != null && dsdesignation.Tables.Count > 0 && dsdesignation.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in dsdesignation.Tables[0].Rows)
                        {
                            if (desgnationCount == 0)
                            {
                                ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                            }
                            ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                            desgnationCount = desgnationCount + 1;
                        }
                    }

                    // ViewBag.ddlDesignation = ddlDesignation;

                    model.ddlDesignation = ddlDesignation;
                    model.Result = "yes";
                }
                else
                {
                    model.SponsorName = "";
                    model.Result = "no";
                }
                #endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult GetSponsorForCustomerRegistraton(string SponsorID)
        {
            try
            {
                AssociateBooking model = new AssociateBooking();
                model.LoginId = SponsorID;

                #region GetSiteRate
                DataSet dsSponsorName = model.GetSponsorForCustomerRegistraton();
                if (dsSponsorName != null && dsSponsorName.Tables[0].Rows.Count > 0)
                {
                    model.SponsorName = dsSponsorName.Tables[0].Rows[0]["Name"].ToString();
                    model.UserID = dsSponsorName.Tables[0].Rows[0]["PK_UserID"].ToString();
                    model.AssociateImage = dsSponsorName.Tables[0].Rows[0]["profilepic"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.SponsorName = "";
                    model.Result = "no";
                }
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        

        public ActionResult GetUserList()
        {
            Reports obj = new Reports();
            List<Reports> lst = new List<Reports>();
            obj.LoginId = Session["LoginId"].ToString();
            DataSet ds = obj.GettingUserProfile();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Reports objList = new Reports();
                    objList.UserName = dr["Fullname"].ToString();
                    objList.LoginIDD = dr["LoginId"].ToString();
                    lst.Add(objList);
                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        

        public ActionResult GetSponsorNameFOrCustomer(string SponsorID)
        {
            try
            {
                AssociateBooking model = new AssociateBooking();
                model.LoginId = SponsorID;

                #region GetSiteRate
                DataSet dsSponsorName = model.GetSponsorNameFOrCustomer();
                if (dsSponsorName != null && dsSponsorName.Tables[0].Rows.Count > 0)
                {
                    model.SponsorName = dsSponsorName.Tables[0].Rows[0]["Name"].ToString();
                    model.UserID = dsSponsorName.Tables[0].Rows[0]["PK_UserID"].ToString();
                    model.AssociateImage = dsSponsorName.Tables[0].Rows[0]["profilepic"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.SponsorName = "";
                    model.Result = "no";
                }
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }






        public ActionResult GetSponsorNames(string SponsorID)
        {
            try
            {
                AssociateBooking model = new AssociateBooking();
                model.LoginId = SponsorID;

                #region GetSiteRate
                DataSet dssponsor = model.GetAssociateListsssss();
                if (dssponsor != null && dssponsor.Tables[0].Rows.Count > 0)
                {
                    model.SponsorName = dssponsor.Tables[0].Rows[0]["Name"].ToString();
                    model.UserID = dssponsor.Tables[0].Rows[0]["PK_UserID"].ToString();
                    model.SponsorDesignationID = dssponsor.Tables[0].Rows[0]["FK_DesignationID"].ToString();
                    model.Percentage = dssponsor.Tables[0].Rows[0]["Percentage"].ToString();
                    int desgnationCount = 0;

                    DataSet dsdesignation = model.GetDesignationList();
                    List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                    if (dsdesignation != null && dsdesignation.Tables.Count > 0 && dsdesignation.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in dsdesignation.Tables[0].Rows)
                        {
                            if (desgnationCount == 0)
                            {
                                ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                            }
                            ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                            desgnationCount = desgnationCount + 1;
                        }
                    }

                    // ViewBag.ddlDesignation = ddlDesignation;

                    model.ddlDesignation = ddlDesignation;
                    model.Result = "yes";
                }
                else
                {
                    model.SponsorName = "";
                    model.Result = "no";
                }
                #endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }








        /////////////////////////////////////////////////////////////////////////////////////



        public ActionResult AssociateRegistration()
        {

            AssociateBooking obj = new AssociateBooking();
            obj.SponsorID = Session["LoginId"].ToString();
            obj.SponsorName = Session["FullName"].ToString();
            obj.UserID = Session["Pk_userId"].ToString();

            try
            {
                #region ddlBranch
           
                int count = 0;
                List<SelectListItem> ddlBranch = new List<SelectListItem>();
                DataSet dsBranch = obj.GetBranchList();
                if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsBranch.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                        }
                        ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlBranch = ddlBranch;

                #endregion

                #region ddlDesignation
                
                obj.Percentage = Session["Percentage"].ToString();
                
                int desgnationCount = 0;
                List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                DataSet dsdesignation = obj.GetDesignationList();
                if (dsdesignation != null && dsdesignation.Tables.Count > 0 && dsdesignation.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsdesignation.Tables[0].Rows)
                    {
                        if (desgnationCount == 0)
                        {
                            ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                        }
                        ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                        desgnationCount = desgnationCount + 1;
                    }
                }

                ViewBag.ddlDesignation = ddlDesignation;

                #endregion
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(obj);

        }

        
        [HttpPost]
        [ActionName("AssociateRegistration")]
        [OnAction(ButtonName = "btnRegistration")]
        public ActionResult AssociateRegistration(HttpPostedFileBase postedFile, HttpPostedFileBase postedFile1, AssociateBooking model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Random rnd = new Random();
                int ctrPasword = rnd.Next(111111, 999999);
                model.Password = Crypto.Encrypt(ctrPasword.ToString());
                model.AddedBy = Session["Pk_userId"].ToString();
                if (postedFile != null)
                {
                    model.ProfilePic = "/images/ProfilePicture/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.ProfilePic)));
                    Session["ProfilePic"] = model.ProfilePic;
                }
                if (postedFile1 != null)
                {
                    model.Signature = "/images/ProfilePicture/" + Guid.NewGuid() + Path.GetExtension(postedFile1.FileName);
                    postedFile1.SaveAs(Path.Combine(Server.MapPath(model.Signature)));

                }
                DataSet dsRegistration = model.AssociateRegistration();
                if (dsRegistration != null && dsRegistration.Tables.Count > 0)
                {
                    if (dsRegistration.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        Session["DisplayNameConfirm"] = dsRegistration.Tables[0].Rows[0]["Name"].ToString();
                        Session["LoginIDConfirm"] = dsRegistration.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["PasswordConfirm"] = Crypto.Decrypt(dsRegistration.Tables[0].Rows[0]["Password"].ToString());
                        Session["PKUserID"] = Crypto.Encrypt(dsRegistration.Tables[0].Rows[0]["PKUserID"].ToString());
                        string assocname = dsRegistration.Tables[0].Rows[0]["Name"].ToString();
                        string loginid = dsRegistration.Tables[0].Rows[0]["LoginId"].ToString();
                        string password = Crypto.Decrypt(dsRegistration.Tables[0].Rows[0]["Password"].ToString());
                        string mobile = model.Contact;
                        string tempid = "1707166296837269294";
                        try
                        {
                            string msg = BLSMS.AssociateRegistration(assocname, loginid, password);
                            BLSMS.SendSMS(mobile, msg, tempid);
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        TempData["Registration"] = dsRegistration.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                //if (dsRegistration.Tables[0].Rows[0][0].ToString() == "1")
                //{
                //    if (model.Email != null)
                //    {
                //        string mailbody = "";
                //        try
                //        {
                //            mailbody = "Dear  " + dsRegistration.Tables[0].Rows[0]["Name"].ToString() + ",You have been successfully registered as Afluex Associate.Given below are your login details .!<br/>  <b>Login ID</b> :  " + dsRegistration.Tables[0].Rows[0]["LoginId"].ToString() + "<br/> <b>Passoword</b>  : " + Crypto.Decrypt(dsRegistration.Tables[0].Rows[0]["Password"].ToString());

                //            //var fromAddress = new MailAddress("prakher.afluex@gmail.com");
                //            //var toAddress = new MailAddress(model.Email);

                //            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                //            {
                //                Host = "smtp.gmail.com",
                //                Port = 587,
                //                EnableSsl = true,
                //                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                //                UseDefaultCredentials = false,
                //                Credentials = new NetworkCredential("tejinfrazone@gmail.com", "awneesh1")

                //            };

                //            using (var message = new MailMessage("tejinfrazone@gmail.com", model.Email)
                //            {
                //                IsBodyHtml = true,
                //                Subject = "Associate Registration",
                //                Body = mailbody
                //            })
                //                smtp.Send(message);
                //            TempData["Message"] = "Registration Successfull !";
                //        }
                //        catch (Exception ex)
                //        {
                //            //TempData["Registration"] = ex.Message;
                //        }
                //    }
                //    FormName = "ConfirmRegistration";
                //    Controller = "TraditionalAssociate";
                //}
                //else/* if(dsRegistration.Tables[0].Rows[0][0].ToString() == "0")*/
                //{
                //    TempData["Registration"] = dsRegistration.Tables[0].Rows[0]["ErrorMessage"].ToString();
                //    //FormName = "AssociateRegistration";
                //    //Controller = "TraditionalAssociate";
                //}

            }
            catch (Exception ex)
            {
                TempData["Registration"] = ex.Message;
            }
            FormName = "ConfirmRegistrationForAssociate";
            Controller = "AssociateDashboard";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult ConfirmRegistrationForAssociate()
        {
            return View();
        }


        public ActionResult CustomerRegistration(string UserID)
        {
            List<SelectListItem> ddlAbbreviation = Common.Abbreviation();
            ViewBag.ddlAbbreviation = ddlAbbreviation;
            AssociateBooking obj = new AssociateBooking();
            obj.SponsorID = Session["LoginId"].ToString();
            obj.SponsorName = Session["FullName"].ToString();
            obj.UserID = Session["Pk_userId"].ToString();
            
            try
            {
                #region ddlBranch
               
                int count = 0;
                List<SelectListItem> ddlBranch = new List<SelectListItem>();
                DataSet dsBranch = obj.GetBranchList();
                if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsBranch.Tables[0].Rows)
                    {
                        //if (count == 0)
                        //{
                        //    ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                        //}
                        ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlBranch = ddlBranch;

                #endregion  
                return View(obj);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        [HttpPost]
        [ActionName("CustomerRegistration")]
        [OnAction(ButtonName = "btnRegistration")]
        public ActionResult CustomerRegistration(AssociateBooking model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Random rnd = new Random();
                string ctrpass = rnd.Next(111111, 999999).ToString();
                model.Password = Crypto.Encrypt(ctrpass);
                model.AddedBy = Session["Pk_userId"].ToString();
                DataSet dsRegistration = model.CustomerRegistration();
                if (dsRegistration.Tables[0].Rows[0][0].ToString() == "1")
                {
                    if (model.Email != null)
                    {
                        string mailbody = "";
                        try
                        {
                            mailbody = "Dear  " + dsRegistration.Tables[0].Rows[0]["Name"].ToString() + ",You have been successfully registered as RealEstateDemo Customer.Given below are your login details .!<br/>  <b>Login ID</b> :  " + dsRegistration.Tables[0].Rows[0]["LoginId"].ToString() + "<br/> <b>Passoword</b>  : " + Crypto.Decrypt(dsRegistration.Tables[0].Rows[0]["Password"].ToString());

                            //var fromAddress = new MailAddress("prakher.afluex@gmail.com");
                            //var toAddress = new MailAddress(model.Email);

                            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                            {
                                Host = "smtp.gmail.com",
                                Port = 587,
                                EnableSsl = true,
                                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                                UseDefaultCredentials = false,
                                Credentials = new NetworkCredential("tejinfrazone@gmail.com", "awneesh1")

                            };

                            using (var message = new MailMessage("tejinfrazone@gmail.com", model.Email)
                            {
                                IsBodyHtml = true,
                                Subject = "Customer Registration",
                                Body = mailbody
                            })
                                smtp.Send(message);
                            TempData["Message"] = "Registration Successfull !";
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                if (dsRegistration != null && dsRegistration.Tables.Count > 0)
                {
                    if (dsRegistration.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        Session["DisplayNameConfirm"] = dsRegistration.Tables[0].Rows[0]["Name"].ToString();
                        Session["LoginIDConfirm"] = dsRegistration.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["PasswordConfirm"] = Crypto.Decrypt(dsRegistration.Tables[0].Rows[0]["Password"].ToString());
                        Session["PKUserID"] = Crypto.Encrypt(dsRegistration.Tables[0].Rows[0]["PKUserID"].ToString());

                        string name = dsRegistration.Tables[0].Rows[0]["Name"].ToString();
                        string id = dsRegistration.Tables[0].Rows[0]["LoginId"].ToString();
                        string pass = Crypto.Decrypt(dsRegistration.Tables[0].Rows[0]["Password"].ToString());
                        string mob = model.Contact;
                        string tempid = "1707166296829885562";
                        string str = BLSMS.CustomerRegistration(name, id, pass);
                        try
                        {
                            BLSMS.SendSMS(mob, str, tempid);
                        }
                        catch { }

                    }
                    else
                    {
                        TempData["Registration"] = dsRegistration.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Registration"] = ex.Message;
            }
            FormName = "ConfirmRegistrationForCustomer";
            Controller = "AssociateDashboard";

            return RedirectToAction(FormName, Controller);
        }


        public ActionResult ConfirmRegistrationForCustomer()
        {
            return View();
        }


        public ActionResult DirectIncome(AssociateBooking model)
        {
            model.UserID = Session["LoginId"].ToString();

            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.GetDirectIncomes();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.Date = r["CurrentDate"].ToString();
                    obj.FromID = r["FromLoginId"].ToString();
                    obj.FromName = r["FromUserName"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.IncomeType = r["IncomeType"].ToString();
                    obj.Income = r["Income"].ToString();
                    lst.Add(obj);
                }
                model.lstDirectIncome = lst;
            }
            return View(model);
        }



        public ActionResult DifferentialIncome(AssociateBooking model)
        {
            model.UserID = Session["LoginId"].ToString();

            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.GetDifferentialIncome();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.Date = r["CurrentDate"].ToString();
                    obj.FromID = r["FromLoginId"].ToString();
                    obj.FromName = r["FromUserName"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.IncomeType = r["IncomeType"].ToString();
                    obj.Income = r["Income"].ToString();
                    lst.Add(obj);
                }
                model.lstDifferentialIncome = lst;
            }
            return View(model);
        }



        public ActionResult DirectLeadershipIncome(AssociateBooking model)
        {
            model.UserID = Session["LoginId"].ToString();

            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.GetDirectLeadershipIncome();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.Date = r["CurrentDate"].ToString();
                    obj.FromID = r["FromLoginId"].ToString();
                    obj.FromName = r["FromUserName"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.IncomeType = r["IncomeType"].ToString();
                    obj.Income = r["Income"].ToString();
                    lst.Add(obj);
                }
                model.lstDirectLeadershipIncome = lst;
            }
            return View(model);
        }
    }
}
