using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using OmPrabha;
using OmPrabha.Filter;
using OmPrabha.Models;

namespace OmPrabha.Controllers
{
    public class HomeController : Controller
    {

        #region plot avaailability
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
        public ActionResult PlotAvailability()
        {
            //#region ddlSiteType
            //Master objSiteType = new Master();
            //int count1 = 0;
            //List<SelectListItem> ddlSiteType = new List<SelectListItem>();
            //DataSet ds2 = objSiteType.GetSiteTypeList();
            //if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in ds2.Tables[0].Rows)
            //    {
            //        if (count1 == 0)
            //        {
            //            ddlSiteType.Add(new SelectListItem { Text = "Select Site Type", Value = "0" });
            //        }
            //        ddlSiteType.Add(new SelectListItem { Text = r["SiteTypeName"].ToString(), Value = r["PK_SiteTypeID"].ToString() });
            //        count1 = count1 + 1;
            //    }
            //}

            //ViewBag.ddlSiteType = ddlSiteType;

            //#endregion
            #region ddlSite
            int count1 = 0;
            Master obj = new Master();
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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
        [HttpPost]
        [ActionName("PlotAvailability")]
        [OnAction(ButtonName = "Search")]
        public ActionResult Details(Master model)
        {
            //Master model = new Master();
            List<Master> lst = new List<Master>();

            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.SiteTypeID = "1";

            DataSet dsblock1 = model.GetDetails();
            if (dsblock1 != null && dsblock1.Tables[0].Rows.Count > 0)
            {
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
                    obj.PlotArea = r["PlotArea"].ToString();
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
            //#region ddlSite
            //int count1 = 0;
            //Master objmaster = new Master();
            //List<SelectListItem> ddlSite = new List<SelectListItem>();
            //DataSet dsSite = objmaster.GetSiteList();
            //if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in dsSite.Tables[0].Rows)
            //    {
            //        if (count1 == 0)
            //        {
            //            ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
            //        }
            //        ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
            //        count1 = count1 + 1;

            //    }
            //}
            //ViewBag.ddlSite = ddlSite;
            //#endregion

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
        #region login
        public ActionResult Registration()
        {

            return View();
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
        public ActionResult RegistrationAction(string SponsorId, string FirstName, string LastName, string Email, string MobileNo, string Commitment, string PaymentMethod)
        {
            Home obj = new Home();
            try
            {

                obj.SponsorId = SponsorId;
                obj.FirstName = FirstName;
                obj.LastName = LastName;
                obj.Email = Email;
                obj.MobileNo = MobileNo;
                obj.Commitment = Commitment;
                obj.PaymentMethod = PaymentMethod;
                obj.RegistrationBy = "Web";
                DataSet ds = obj.Registration();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["DisplayName"] = ds.Tables[0].Rows[0]["Name"].ToString();
                        Session["PassWord"] = ds.Tables[0].Rows[0]["Password"].ToString();
                        Session["Transpassword"] = ds.Tables[0].Rows[0]["Password"].ToString();
                        Session["MobileNo"] = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        obj.Response = "1";

                    }
                    else
                    {
                        obj.Response = "Registration not done.Please try after some time";
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Response = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ConfirmRegistration()
        {
            return View();
        }
        #endregion
        #region login
        public ActionResult Login()
        {
            Session.Abandon();
            return View();
        }
        public ActionResult LoginAction(Home obj)
        {
            string FormName = "";
            string Controller = "";
            ProjectStatusResponse datalist = null;
            try
            {
                Home Modal = new Home();
                DataSet ds = obj.Login();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Login"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        //FormName = "Login";
                        //Controller = "Home";
                        
                        FormName = "index";
                        Controller = "website";
                        
                    }
                    else if ((ds.Tables[0].Rows[0]["UserType"].ToString() == "Trad Associate"))
                    {
                        if (obj.Password == Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()))
                        {
                            Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            Session["Pk_userId"] = ds.Tables[0].Rows[0]["Pk_userId"].ToString();
                            Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                            Session["FullName"] = ds.Tables[0].Rows[0]["FullName"].ToString();
                            Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                            Session["Status"] = ds.Tables[0].Rows[0]["Status"].ToString();
                            Session["CssClass"] = ds.Tables[0].Rows[0]["StatusColor"].ToString();
                            Session["DesignationName"] = ds.Tables[0].Rows[0]["DesignationName"].ToString();
                            Session["Percentage"] = ds.Tables[0].Rows[0]["Percentage"].ToString();
                            FormName = "AssociateDashBoard";
                            Controller = "AssociateDashboard";
                        }
                        else
                        {
                             TempData["Login"] = "Incorrect Password";
                            //FormName = "Login";
                            //Controller = "Home";
                            FormName = "index";
                            Controller = "website";
                        }
                    }
                    else if ((ds.Tables[0].Rows[0]["UserType"].ToString() == "MLM Associate"))
                    {
                        if (obj.Password == Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()))
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
                        else
                        {
                            TempData["Login"] = "Incorrect Password";
                            
                            //FormName = "Login";
                            //Controller = "Home";
                            FormName = "index";
                            Controller = "website";
                        }
                    }
                    else if ((ds.Tables[0].Rows[0]["UserType"].ToString() == "Customer"))
                    {
                        if (obj.Password == Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()))
                        {
                            Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            Session["Pk_userId"] = ds.Tables[0].Rows[0]["Pk_userId"].ToString();
                            Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                            Session["FullName"] = ds.Tables[0].Rows[0]["FullName"].ToString();
                            Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                            Session["Status"] = ds.Tables[0].Rows[0]["Status"].ToString();
                            Session["CssClass"] = ds.Tables[0].Rows[0]["StatusColor"].ToString();
                            FormName = "CustomerDashBoard";
                            Controller = "CustomerDashboard";
                        }
                        else
                        {
                            TempData["Login"] = "Incorrect Password";
                            //FormName = "Login";
                            //Controller = "Home";
                            FormName = "index";
                            Controller = "website";
                        }
                    }
                    else if (ds.Tables[0].Rows[0]["UserType"].ToString() == "Admin")
                    {
                        if(ds.Tables[0].Rows[0]["UserTypeName"].ToString() == "Admin")
                        {
                            Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            Session["Pk_AdminId"] = ds.Tables[0].Rows[0]["Pk_adminId"].ToString();
                            Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                            Session["UserTypeName"] = ds.Tables[0].Rows[0]["UserTypeName"].ToString();
                            Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                            Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();


                            FormName = "AdminDashboard";
                            Controller = "Admin";
                        }
                        else
                        {
                            Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            Session["Pk_AdminId"] = ds.Tables[0].Rows[0]["Pk_adminId"].ToString();
                            Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                            Session["UserTypeName"] = ds.Tables[0].Rows[0]["UserTypeName"].ToString();
                            Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                            Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();


                            FormName = "EmployeeDashboard";
                            Controller = "Admin";
                        }
                      
                    }
                }
     
                else
                {
                   TempData["Login"] = "Incorrect Login Id Or Password";
                    
                    //FormName = "Login";
                    //Controller = "Home";
                    FormName = "index";
                    Controller = "website";

                }
            }
            catch (Exception ex)
            {
                TempData["Login"] = ex.Message;

                //FormName = "Login";
                //Controller = "Home";
                FormName = "index";
                Controller = "website";
            }

            return RedirectToAction(FormName, Controller);
        }
        
        public ActionResult ForgetPassword()
        {
            return View();
        }
        public ActionResult GetOtp(string LoginId, string MobileNo)
        {
            Home model = new Home();
            model.LoginId = LoginId;
            if (model.LoginId == "") { model.LoginId = null; }
            model.MobileNo = MobileNo;
            if (model.MobileNo == "") { model.MobileNo = null; }
            Random rnd = new Random();
            string otpass = rnd.Next(1111, 9999).ToString();
            try
            {
                DataSet ds = model.GettingPassword();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.Response = "1";
                        BLSMS.SendSMS2(SMSCredential.UserName, SMSCredential.Password, SMSCredential.SenderId, model.MobileNo, model.Password);
                        model.Response = "1";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        model.Response = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }

                }
            }
            catch (Exception ex)
            {
                model.Response = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPassword(string LoginId, string MobileNo)
        {
            Home model = new Home();
            model.LoginId = LoginId;
            model.MobileNo = MobileNo;
            model.LoginId = LoginId;
            if (model.LoginId == "") { model.LoginId = null; }
            model.MobileNo = MobileNo;
            if (model.MobileNo == "") { model.MobileNo = null; }
            string Status = "";
            DataTable dtSMS = new DataTable();
            dtSMS.Columns.Add("Name", typeof(string));
            dtSMS.Columns.Add("Mobile", typeof(string));
            dtSMS.Columns.Add("Status", typeof(string));
            DataSet ds = model.GettingPassword();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    string FirstName = ds.Tables[0].Rows[0]["LoginId"].ToString();
                    string Password = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()); ;
                    string mob = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                    string tempid = "1707166296858597138";
                    string msg = BLSMS.ForgetPassword(FirstName, Password);
                    if (mob.Length < 10)
                    {
                        Status = "Failed";
                    }
                    else if (mob.Length > 10)
                    {
                        Status = "Failed";
                    }
                    else
                    {

                        BLSMS.SendSMS(MobileNo,msg, tempid);
                        Status = "Send";
                        model.Response = "1";
                    }


                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["ForgotPassword"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    model.BankAccountNo  = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult Login()
        //{
        //    Session.Abandon();
        //    return View();
        //}

        //public ActionResult LoginAction(Home obj)
        //{
        //    string FormName = "";
        //    string Controller = "";
        //    ProjectStatusResponse datalist = null;

        //    try
        //    {
        //        Home Modal = new Home();
        //        DataSet ds = obj.Login();
        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0][0].ToString() == "0")
        //            {
        //                TempData["Login"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //                FormName = "Login";
        //                Controller = "Home";
        //            }
        //            else if ((ds.Tables[0].Rows[0]["UserType"].ToString() == "MLM Associate"))
        //            {
        //                if (obj.Password == Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()))
        //                {
        //                    Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
        //                    Session["Pk_userId"] = ds.Tables[0].Rows[0]["Pk_userId"].ToString();
        //                    Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
        //                    Session["FullName"] = ds.Tables[0].Rows[0]["FullName"].ToString();
        //                    Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
        //                    Session["Status"] = ds.Tables[0].Rows[0]["Status"].ToString();
        //                    Session["CssClass"] = ds.Tables[0].Rows[0]["StatusColor"].ToString();
        //                    FormName = "AssociateDashBoard";
        //                    Controller = "AssociateDashboard";
        //                }
        //                else
        //                {
        //                    TempData["Login"] = "Incorrect Password";
        //                    FormName = "Login";
        //                    Controller = "Home";
        //                }
        //            }
        //            else if ((ds.Tables[0].Rows[0]["UserType"].ToString() == "Customer"))
        //            {
        //                if (obj.Password == Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()))
        //                {
        //                    Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
        //                    Session["Pk_userId"] = ds.Tables[0].Rows[0]["Pk_userId"].ToString();
        //                    Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
        //                    Session["FullName"] = ds.Tables[0].Rows[0]["FullName"].ToString();
        //                    Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
        //                    Session["Status"] = ds.Tables[0].Rows[0]["Status"].ToString();
        //                    Session["CssClass"] = ds.Tables[0].Rows[0]["StatusColor"].ToString();
        //                    FormName = "CustomerDashBoard";
        //                    Controller = "CustomerDashboard";
        //                }
        //                else
        //                {
        //                    TempData["Login"] = "Incorrect Password";
        //                    FormName = "Login";
        //                    Controller = "Home";
        //                }
        //            }
        //            else if (ds.Tables[0].Rows[0]["UserType"].ToString() == "Admin")
        //            {
        //                Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
        //                Session["Pk_AdminId"] = ds.Tables[0].Rows[0]["Pk_adminId"].ToString();
        //                Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
        //                Session["UserTypeName"] = ds.Tables[0].Rows[0]["UserTypeName"].ToString();
        //                Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
        //                Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();


        //                FormName = "AdminDashBoard";
        //                Controller = "Admin";
        //            }
        //        }
        //        else
        //        {
        //            TempData["Login"] = "Incorrect Login ID Or Password";
        //            FormName = "Login";
        //            Controller = "Home";

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Login"] = ex.Message;
        //        FormName = "Login";
        //        Controller = "Home";
        //    }

        //    return RedirectToAction(FormName, Controller);
        //}
        #endregion
        #region Website
        public virtual PartialViewResult Menu()
        {
            Home Menu = null;

            if (Session["_Menu"] != null)
            {
                Menu = (Home)Session["_Menu"];
            }
            else
            {

                Menu = Home.GetMenus(Session["Pk_AdminId"].ToString(), Session["UserTypeName"].ToString()); // pass employee id here
                Session["_Menu"] = Menu;
            }
            return PartialView("_Menu", Menu);
        }
        public ActionResult Index()
        {

            return View();

        }
        public ActionResult CompanyOverView()
        {

            return View();

        }
        public ActionResult ManagementMesssage()
        {

            return View();

        }
        public ActionResult Contactus()
        {

            return View();
        }
        [HttpPost]
        [ActionName("Contactus")]
        [OnAction(ButtonName = "btnsubmit")]
        public ActionResult SendSms(Home model)
        {

            try
            {

                model.AddedBy = "1";
                DataSet ds = model.SendSms();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Message"] = " Message Send Successfully";


                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

            }
            return View();
        }
        public ActionResult Gallery(Master model)
        {

            List<Master> lst = new List<Master>();

            model.SiteID = model.SiteID == "0" ? null : model.SiteID;

            DataSet ds = model.GetGallery();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_GalleryID = r["PK_GalleryID"].ToString();
                    obj.SiteID = r["FK_SiteID"].ToString();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.SiteImage = r["SiteImage"].ToString();


                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }

            return View(model);
        }
        public ActionResult Vision()
        {
            return View();
        }
        public ActionResult Mission()
        {
            return View();
        }
        public ActionResult QualityPolicy()
        {
            return View();
        }
        public ActionResult OurTeam()
        {
            return View();
        }
        public ActionResult Projects()
        {
            return View();
        }
        public ActionResult UnitAvailability()
        {
            return View();
        }
        public ActionResult Career()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Career")]
        [OnAction(ButtonName = "Save")]
        public ActionResult Career(HttpPostedFileBase postedFile, Master model)
        {
            try
            {
                if (postedFile != null)
                {
                    model.FileUpload = "/CareerFileUpload/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.FileUpload)));

                }
                // model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = model.SaveCareer();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        SmtpClient mailServer = new SmtpClient("smtp.gmail.com", 587);
                        mailServer.EnableSsl = true;
                        //mailServer.Credentials = new System.Net.NetworkCredential(model.SenderEmail, model.SenderPassword);
                        mailServer.Credentials = new System.Net.NetworkCredential("tejinfrazone@gmail.com", "awneesh1");

                        MailMessage myMail = new MailMessage();
                        myMail.Subject = "Career";
                        myMail.Body = "P.F.A";
                        myMail.From = new MailAddress("tejinfrazone@gmail.com");
                        myMail.To.Add("tejinfrazone@gmail.com");

                        HttpPostedFileBase file = Request.Files["postedfile"];
                        if (file != null && file.ContentLength > 0)
                        {
                            if (file.ContentLength < 12288000)
                            {
                                string filename = Path.GetFileName(file.FileName);
                                var attachment = new Attachment(file.InputStream, filename);
                                myMail.Attachments.Add(attachment);
                            }
                        }
                        mailServer.Send(myMail);

                        TempData["Career"] = "Data Saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Career"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Career"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Career"] = ex.Message;
            }
            return RedirectToAction("Career", "Home");

        }
        public ActionResult AasraParadise()
        {
            return View();
        }
        public ActionResult AasraPuram()
        {
            return View();
        }

        #endregion
    }
}
