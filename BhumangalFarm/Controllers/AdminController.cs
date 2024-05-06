using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using BusinessLayer;
using OmPrabha.Models;
using OmPrabha.Filter;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace OmPrabha.Controllers
{
    public class AdminController : AdminBaseController
    {
        public ActionResult AdminDashBoard()
        {
            //test changes

            DashBoard newdata = new DashBoard();
            try
            {
                DataSet Ds = newdata.GetDetails();
                ViewBag.Associates = Ds.Tables[0].Rows[0]["Associates"].ToString();
                ViewBag.Customers = Ds.Tables[0].Rows[0]["TotalCustomers"].ToString();
                ViewBag.Plots = Ds.Tables[0].Rows[0]["Plots"].ToString();
                ViewBag.TotalBusiness = Ds.Tables[0].Rows[0]["TotalBusiness"].ToString();

                List<DashBoard> lst = new List<DashBoard>();
                DataSet dsblock = newdata.GetBookingDetailsList();
                if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsblock.Tables[0].Rows)
                    {
                        DashBoard obj = new DashBoard();
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
                        obj.BookingDate = r["BookingDate"].ToString();
                        obj.BookingStatus = r["BookingStatus"].ToString();
                        obj.PlotRate = r["PlotRate"].ToString();
                        obj.Amount = r["NetPlotAmount"].ToString();

                        //model.PlotStatus = dsblock.Tables[0].Rows[0]["Status"].ToString();

                        lst.Add(obj);
                    }

                    newdata.List = lst;
                }
                List<DashBoard> lstAssociate = new List<DashBoard>();
                DataSet dsAssociate = newdata.GetAssociateDetails();
                if (dsAssociate != null && dsAssociate.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsAssociate.Tables[0].Rows)
                    {
                        DashBoard obj = new DashBoard();
                        //   obj.PK_BookingId = r["PK_UserId"].ToString();

                        obj.AssociateName = r["AssociateName"].ToString();
                        obj.JoiningDate = r["JoiningDate"].ToString();
                        obj.FK_DesignationID = r["FK_DesignationID"].ToString();
                        obj.DesignationName = r["DesignationName"].ToString();
                        obj.ProfilePic = r["ProfilePic"].ToString();
                        obj.AssociateLoginID = r["AssociateLoginID"].ToString();
                        //model.PlotStatus = dsblock.Tables[0].Rows[0]["Status"].ToString();

                        lstAssociate.Add(obj);
                    }

                    newdata.ListAssociate = lstAssociate;
                }

                List<DashBoard> lstInst = new List<DashBoard>();
                DataSet dsInst = newdata.GetDueInstallmentList();
                if (dsInst != null && dsInst.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsInst.Tables[0].Rows)
                    {
                        DashBoard obj = new DashBoard();

                        obj.CustomerID = r["Customer"].ToString();
                        obj.CustomerLoginID = r["LoginId"].ToString();
                        obj.CustomerName = r["FirstName"].ToString();
                        obj.PlotNumber = r["PlotInfo"].ToString();
                        obj.InstallmentNo = r["InstallmentNo"].ToString();
                        obj.InstallmentAmount = r["InstAmt"].ToString();
                        obj.IntallmentDate = r["InstallmentDate"].ToString();
                        obj.AssociateLoginID = r["AssociateLoginID"].ToString();
                        lstInst.Add(obj);
                    }

                    newdata.ListInstallment = lstInst;
                }
            }
            catch (Exception ex)
            {
                TempData["Dashboard"] = ex.Message;
            }
            return View(newdata);
        }
        //    public ActionResult AssociateListForKYC(Reports model)
        //    {
        //        List<SelectListItem> ddlKYCStatus = Common.BindKYCStatus();
        //        ViewBag.ddlKYCStatus = ddlKYCStatus;
        //        List<Reports> lst = new List<Reports>();

        //        model.Status = null;
        //        DataSet ds = model.AssociateListForKYC();

        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow r in ds.Tables[0].Rows)
        //            {
        //                Reports obj = new Reports();
        //                obj.PK_DocumentID = r["PK_UserDocumentID"].ToString();
        //                obj.LoginId = r["LoginId"].ToString();
        //                obj.Name = r["FirstName"].ToString();
        //                obj.DocumentNumber = r["DocumentNumber"].ToString();
        //                obj.DocumentType = r["DocumentType"].ToString();
        //                obj.DocumentImage = (r["DocumentImage"].ToString());
        //                obj.Status = (r["Status"].ToString());

        //                lst.Add(obj);
        //            }
        //            model.lstassociate = lst;
        //        }
        //        return View(model);
        //    }

        //    public ActionResult ApproveKYC(string Id, string DocumentType, string LoginID)
        //    {
        //        string FormName = "";
        //        string Controller = "";
        //        try
        //        {
        //            List<SelectListItem> ddlKYCStatus = Common.BindKYCStatus();
        //            ViewBag.ddlKYCStatus = ddlKYCStatus;
        //            List<Reports> lst = new List<Reports>();

        //            Reports model = new Reports();
        //            model.LoginId = LoginID;
        //            model.PK_DocumentID = Id;
        //            model.DocumentType = DocumentType;
        //            model.Status = "Approved";
        //            model.AddedBy = Session["Pk_AdminId"].ToString();

        //            DataSet ds = new DataSet();
        //            ds = model.ApproveKYC();
        //            if (ds != null && ds.Tables[0].Rows.Count > 0)
        //            {
        //                if (ds.Tables[0].Rows[0][0].ToString() == "1")
        //                {
        //                    TempData["KYCVerification"] = "KYC Approved Successfully..";
        //                    FormName = "AssociateListForKYC";
        //                    Controller = "Admin";
        //                }
        //                else if (ds.Tables[0].Rows[0][0].ToString() == "0")
        //                {
        //                    TempData["KYCVerification"] = "Error : " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //                    FormName = "AssociateListForKYC";
        //                    Controller = "Admin";
        //                }
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            TempData["KYCVerification"] = ex.Message;
        //            FormName = "AssociateListForKYC";
        //            Controller = "Admin";
        //        }
        //        return RedirectToAction(FormName, Controller);
        //    }

        //    public ActionResult GetUserDetails()
        //    {
        //        List<DashBoard> dataList = new List<DashBoard>();
        //        DataSet Ds = new DataSet();
        //        DataTable dt = new DataTable();
        //        DashBoard newdata = new DashBoard();

        //        Ds = newdata.GetDashBoardDetails();
        //        if (Ds.Tables.Count > 0)
        //        {
        //            ViewBag.TotalUsers = Ds.Tables[0].Rows.Count;
        //            int count = 0;
        //            foreach (DataRow dr in Ds.Tables[0].Rows)
        //            {
        //                DashBoard details = new DashBoard();


        //                details.Total = (dr["Total"].ToString());
        //                details.Status = (dr["Status"].ToString());


        //                dataList.Add(details);

        //                count++;
        //            }
        //        }
        //        return Json(dataList, JsonRequestBehavior.AllowGet);
        //    }

        //    public ActionResult GetPayoutStatus()
        //    {
        //        List<DashBoard> dataList = new List<DashBoard>();
        //        DataSet Ds = new DataSet();
        //        DataTable dt = new DataTable();
        //        DashBoard newdata = new DashBoard();

        //        Ds = newdata.GetDashBoardDetails();
        //        if (Ds.Tables.Count > 0)
        //        {
        //            ViewBag.TotalUsers = Ds.Tables[2].Rows.Count;
        //            int count = 0;
        //            foreach (DataRow dr in Ds.Tables[2].Rows)
        //            {
        //                DashBoard details = new DashBoard();


        //                details.Total = (dr["Total"].ToString());
        //                details.Status = (dr["Status"].ToString());


        //                dataList.Add(details);

        //                count++;
        //            }
        //        }
        //        return Json(dataList, JsonRequestBehavior.AllowGet);
        //    }

        //    public ActionResult GetInvestmentDetails()
        //    {
        //        List<DashBoard> dataList = new List<DashBoard>();
        //        DataSet Ds = new DataSet();
        //        DataTable dt = new DataTable();
        //        DashBoard newdata = new DashBoard();

        //        Ds = newdata.GetDashBoardDetails();
        //        if (Ds.Tables.Count > 0)
        //        {
        //            ViewBag.TotalUsers = Ds.Tables[3].Rows.Count;
        //            int count = 0;
        //            foreach (DataRow dr in Ds.Tables[3].Rows)
        //            {
        //                DashBoard details = new DashBoard();


        //                details.Amount = (dr["Amount"].ToString());
        //                details.Month = (dr["Month"].ToString());


        //                dataList.Add(details);

        //                count++;
        //            }
        //        }
        //        return Json(dataList, JsonRequestBehavior.AllowGet);
        //    }

        //    public ActionResult GetJoiningDeatils()
        //    {
        //        List<DashBoard> dataList = new List<DashBoard>();
        //        DataSet Ds = new DataSet();
        //        DataTable dt = new DataTable();
        //        DashBoard newdata = new DashBoard();

        //        Ds = newdata.GetDashBoardDetails();
        //        if (Ds.Tables.Count > 0)
        //        {
        //            ViewBag.TotalUsers = Ds.Tables[4].Rows.Count;
        //            int count = 0;
        //            foreach (DataRow dr in Ds.Tables[4].Rows)
        //            {
        //                DashBoard details = new DashBoard();


        //                details.Total = (dr["Total"].ToString());
        //                details.Month = (dr["Month"].ToString());


        //                dataList.Add(details);

        //                count++;
        //            }
        //        }
        //        return Json(dataList, JsonRequestBehavior.AllowGet);
        //    }

        //    public ActionResult BinaryTree()
        //    {
        //        ViewBag.Fk_UserId = "1";
        //        return View();
        //    }
        //    public ActionResult Registration(string Pid, string lg)
        //    {
        //        Home obj = new Home();
        //        #region ForQueryString
        //        if (Request.QueryString["Pid"] != null)
        //        {
        //            obj.SponsorId = Request.QueryString["Pid"].ToString();
        //        }
        //        if (Request.QueryString["lg"] != null)
        //        {
        //            obj.Leg = Request.QueryString["lg"].ToString();
        //            if (obj.Leg == "Right")
        //            {
        //                ViewBag.RightChecked = "checked";
        //                ViewBag.LeftChecked = "";
        //                ViewBag.Disabled = "Disabled";
        //            }
        //            else
        //            {
        //                ViewBag.RightChecked = "";
        //                ViewBag.LeftChecked = "checked";
        //                ViewBag.Disabled = "Disabled";
        //            }
        //        }
        //        if (Request.QueryString["Pid"] != null)
        //        {
        //            Common objcomm = new Common();
        //            objcomm.ReferBy = obj.SponsorId;
        //            DataSet ds = objcomm.GetMemberDetails();
        //            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {

        //                obj.SponsorName = ds.Tables[0].Rows[0]["FullName"].ToString();



        //            }
        //        }
        //        else
        //        {
        //            ViewBag.RightChecked = "";
        //            ViewBag.LeftChecked = "checked";
        //        }
        //        #endregion ForQueryString
        //        #region ddlgender
        //        List<SelectListItem> ddlgender = Common.BindGender();
        //        ViewBag.ddlgender = ddlgender;
        //        #endregion ddlgender

        //        #region ddlRate
        //        List<SelectListItem> ddlRate = Common.BindDdlRate();
        //        ViewBag.ddlRate = ddlRate;
        //        #endregion ddlRate



        //        #region Product Bind
        //        Common objcomm1 = new Common();
        //        List<SelectListItem> ddlProduct = new List<SelectListItem>();
        //        DataSet ds1 = objcomm1.BindProduct();
        //        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
        //        {
        //            int count = 0;
        //            foreach (DataRow r in ds1.Tables[1].Rows)
        //            {
        //                if (count == 0)
        //                {
        //                    ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
        //                }
        //                ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
        //                count++;
        //            }
        //        }

        //        ViewBag.ddlProduct = ddlProduct;

        //        Wallet model = new Wallet();
        //        model.Package = "4";
        //        model.Amount = "1000";
        //        #endregion

        //        List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
        //        ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
        //        DataSet ds2 = objcomm1.GetPaymentMode();
        //        if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow r in ds2.Tables[0].Rows)
        //            {



        //                ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PaymentMode"].ToString() });

        //            }
        //        }

        //        ViewBag.ddlpaymentmode = ddlpaymentmode;

        //        List<SelectListItem> ddlBookingType = Common.BookingType();
        //        ViewBag.ddlBookingType = ddlBookingType;


        //        List<SelectListItem> ddlPLC = Common.PLC();
        //        ViewBag.ddlPLC = ddlPLC;


        //        List<SelectListItem> ddlAssociateBenefit = Common.AssociateBenefit();
        //        ViewBag.ddlAssociateBenefit = ddlAssociateBenefit;

        //        List<SelectListItem> ddlCustomerBenefit = Common.CustomerBenefit();
        //        ViewBag.ddlCustomerBenefit = ddlCustomerBenefit;

        //        List<SelectListItem> ddlamount = Common.BindAmount();
        //        ViewBag.ddlamount = ddlamount;







        //        return View(obj);
        //    }

        //    public ActionResult BindData()
        //    {
        //        Common obj = new Common();
        //        #region ddlRate
        //        List<SelectListItem> ddlRate = Common.BindDdlRate();
        //        ViewBag.ddlRate = ddlRate;
        //        obj.ddlRate = ddlRate;
        //        #endregion ddlRate

        //        List<SelectListItem> ddlBookingType = Common.BookingType();
        //        ViewBag.ddlBookingType = ddlBookingType;
        //        obj.ddlBookingType = ddlBookingType;

        //        List<SelectListItem> ddlPLC = Common.PLC();
        //        ViewBag.ddlPLC = ddlPLC;
        //        obj.ddlPLC = ddlPLC;
        //        return Json(obj, JsonRequestBehavior.AllowGet);
        //    }
        public ActionResult EmployeeDashboard()
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
        //    public ActionResult RegistrationAction(string SponsorId, string FirstName, string LastName, string Email, string MobileNo, string PanCard, string Address, string Gender, string OTP, string PinCode, string Leg,
        //        string BookingType, string TopUpDate, string FinalAmount, string BankName, string BankBranch, string TransactionDate, string PlotNumber, string Description, string TransactionNo, string PaymentMode, string CustomerBenefit, string AssociateBenefit, string State, string City, string Rate, string PLC, string PLCAmount, string NetAmount, string GrossAmount, string Discount)

        //    {
        //        Home obj = new Home();

        //        try
        //        {
        //            obj.AddedBy = Session["Pk_AdminId"].ToString();
        //            obj.SponsorId = SponsorId;
        //            obj.FirstName = FirstName;
        //            obj.LastName = LastName;
        //            obj.Email = Email;
        //            obj.MobileNo = MobileNo;
        //            obj.PanCard = PanCard;
        //            obj.Address = Address;
        //            obj.RegistrationBy = "Web";
        //            obj.Gender = Gender;
        //            obj.PinCode = PinCode;
        //            obj.Leg = Leg;
        //            obj.State = State;
        //            obj.City = City;
        //            obj.BookingType = BookingType;
        //            if (Rate == "")
        //            {

        //                obj.Rate = "0";
        //            }
        //            else
        //            {
        //                obj.Rate = Rate;
        //            }

        //            obj.PLC = PLC;
        //            obj.PLCAmount = PLCAmount;
        //            obj.GrossAmount = GrossAmount;
        //            obj.NetAmount = NetAmount;
        //            obj.Discount = Discount;

        //            obj.TopUpDate = string.IsNullOrEmpty(TopUpDate) ? null : Common.ConvertToSystemDate(TopUpDate, "dd/MM/yyyy");

        //            obj.PaymentMode = PaymentMode;
        //            obj.FinalAmount = FinalAmount;
        //            obj.BankName = BankName;



        //            obj.TransactionDate = string.IsNullOrEmpty(TransactionDate) ? null : Common.ConvertToSystemDate(TransactionDate, "dd/MM/yyyy");

        //            obj.PlotNumber = PlotNumber;
        //            obj.Description = Description;
        //            obj.TransactionNo = TransactionNo;

        //            obj.AssociateBenefit = AssociateBenefit == "0" ? null : AssociateBenefit;

        //            obj.CustomerBenefit = CustomerBenefit == "0" ? null : CustomerBenefit;
        //            string password = Common.GenerateRandom();
        //            obj.Password = Crypto.Encrypt(password);
        //            DataSet ds = obj.Registration();
        //            if (ds != null && ds.Tables[0].Rows.Count > 0)
        //            {
        //                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
        //                {
        //                    Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
        //                    Session["DisplayName"] = ds.Tables[0].Rows[0]["Name"].ToString();
        //                    Session["PassWord"] = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
        //                    Session["Transpassword"] = ds.Tables[0].Rows[0]["Password"].ToString();
        //                    Session["MobileNo"] = ds.Tables[0].Rows[0]["MobileNo"].ToString();

        //                    obj.Response = "1";
        //                    if (obj.PlotNumber != null)
        //                    {
        //                        try
        //                        {
        //                            string str2 = BLSMS.RegistrationNew(ds.Tables[0].Rows[0]["Name"].ToString(), obj.PlotNumber);
        //                            BLSMS.SendSMSNew(MobileNo, str2);
        //                        }
        //                        catch { }
        //                    }

        //                    try
        //                    {
        //                        string str2 = BLSMS.Registration(ds.Tables[0].Rows[0]["Name"].ToString(), ds.Tables[0].Rows[0]["LoginId"].ToString(), Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()));
        //                        BLSMS.SendSMSNew(MobileNo, str2);
        //                    }
        //                    catch { }


        //                }
        //                else
        //                {
        //                    obj.Response = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            obj.Response = ex.Message;
        //        }
        //        return Json(obj, JsonRequestBehavior.AllowGet);
        //    }

        //    public ActionResult ConfirmRegistration()
        //    {
        //        return View();
        //    }
        public ActionResult GetStateCity(string Pincode)
        {
            Common obj = new Common();
            obj.Pincode = Pincode;
            DataSet ds = obj.GetStateCity();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                obj.State = ds.Tables[0].Rows[0]["State"].ToString();
                obj.City = ds.Tables[0].Rows[0]["City"].ToString();
                obj.Result = "1";
            }
            else
            {
                obj.Result = "Invalid PinCode";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        //    public ActionResult OTP(string FirstName, string MobileNo)
        //    {
        //        Home obj = new Home();
        //        Common objcom = new Common();
        //        int OTP = objcom.GenerateRandomNo();
        //        Session["OTP"] = OTP.ToString();
        //        string str2 = BLSMS.OTP(FirstName, OTP.ToString());

        //        string str = BLSMS.SendSMS2(SMSCredential.UserName, SMSCredential.Password, SMSCredential.SenderId, "8299051766", str2);
        //        return Json(obj, JsonRequestBehavior.AllowGet);
        //    }

        //    #region PinManagement
        //    public ActionResult CreatePin()
        //    {
        //        #region Product Bind
        //        Common objcomm = new Common();
        //        List<SelectListItem> ddlProduct = new List<SelectListItem>();
        //        DataSet ds1 = objcomm.BindProduct();
        //        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
        //        {
        //            int count = 0;
        //            foreach (DataRow r in ds1.Tables[1].Rows)
        //            {
        //                if (count == 0)
        //                {
        //                    ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
        //                }
        //                ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
        //                count++;
        //            }
        //        }

        //        ViewBag.ddlProduct = ddlProduct;
        //        #endregion

        //        Wallet model = new Wallet();
        //        model.Package = "4";
        //        return View(model);

        //    }

        //    public ActionResult CreatePinAction(Wallet obj)
        //    {
        //        try
        //        {
        //            obj.AddedBy = Session["Pk_AdminId"].ToString();

        //            DataSet ds = obj.CreatePin();
        //            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
        //            {
        //                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
        //                {
        //                    TempData["createpin"] = "Pin Created Successfully";
        //                }
        //                else
        //                {
        //                    TempData["createpin"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //                }

        //            }
        //            else { }

        //        }
        //        catch (Exception ex)
        //        {
        //            TempData["createpin"] = ex.Message;
        //        }
        //        return RedirectToAction("CreatePin", "Admin");
        //    }

        //    public ActionResult UnusedPins()
        //    {
        //        Wallet objewallet = new Wallet();


        //        objewallet.Status = "Unused";
        //        List<Wallet> lst = new List<Wallet>();
        //        DataSet ds = objewallet.GetUsedUnUsedPins();
        //        if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
        //        {

        //            foreach (DataRow dr in ds.Tables[0].Rows)
        //            {
        //                Wallet Objload = new Wallet();
        //                Objload.ePinNo = dr["ePinNo"].ToString();

        //                Objload.Package = dr["PinType"].ToString();

        //                Objload.DisplayName = dr["tOwner"].ToString();
        //                Objload.AddedOn = dr["CreatedDate"].ToString();
        //                Objload.RegisteredTo = dr["tRegTo"].ToString();
        //                Objload.Status = dr["PinStatus"].ToString();
        //                lst.Add(Objload);
        //            }
        //            objewallet.lstunusedpins = lst;
        //        }
        //        #region Product Bind
        //        Common objcomm = new Common();
        //        List<SelectListItem> ddlProduct = new List<SelectListItem>();
        //        DataSet ds1 = objcomm.BindProduct();
        //        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
        //        {
        //            int count = 0;
        //            foreach (DataRow r in ds1.Tables[0].Rows)
        //            {
        //                if (count == 0)
        //                {
        //                    ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
        //                }
        //                ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
        //                count++;
        //            }
        //        }

        //        ViewBag.ddlProduct = ddlProduct;

        //        #endregion
        //        return View(objewallet);
        //    }

        //    [HttpPost]
        //    [ActionName("UnusedPins")]
        //    [OnAction(ButtonName = "Search")]
        //    public ActionResult UnusedPinsBy(Wallet objewallet)
        //    {

        //        objewallet.Status = "Unused";
        //        List<Wallet> lst = new List<Wallet>();
        //        objewallet.Package = objewallet.Package == "0" ? null : objewallet.Package;
        //        DataSet ds = objewallet.GetUsedUnUsedPins();
        //        if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
        //        {

        //            foreach (DataRow dr in ds.Tables[0].Rows)
        //            {
        //                Wallet Objload = new Wallet();
        //                Objload.ePinNo = dr["ePinNo"].ToString();

        //                Objload.Package = dr["PinType"].ToString();

        //                Objload.DisplayName = dr["tOwner"].ToString();
        //                Objload.AddedOn = dr["CreatedDate"].ToString();
        //                Objload.RegisteredTo = dr["tRegTo"].ToString();
        //                Objload.Status = dr["PinStatus"].ToString();
        //                lst.Add(Objload);
        //            }
        //            objewallet.lstunusedpins = lst;
        //        }
        //        #region Product Bind
        //        Common objcomm = new Common();
        //        List<SelectListItem> ddlProduct = new List<SelectListItem>();
        //        DataSet ds1 = objcomm.BindProduct();
        //        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
        //        {
        //            int count = 0;
        //            foreach (DataRow r in ds1.Tables[0].Rows)
        //            {
        //                if (count == 0)
        //                {
        //                    ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
        //                }
        //                ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
        //                count++;
        //            }
        //        }

        //        ViewBag.ddlProduct = ddlProduct;

        //        #endregion
        //        return View(objewallet);
        //    }

        //    public ActionResult TopUpByPin(string Id)
        //    {
        //        Wallet obj = new Wallet();
        //        obj.ePinNo = Id;
        //        return View(obj);
        //    }
        //    public ActionResult TopUpByPinAction(Wallet obj)
        //    {
        //        try
        //        {
        //            obj.AddedBy = Session["Pk_AdminId"].ToString();

        //            DataSet ds = obj.TopupByEpinByAdmin();
        //            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
        //            {
        //                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
        //                {
        //                    TempData["EpinTopup"] = "Id Toup Successfully";
        //                }
        //                else
        //                {
        //                    TempData["EpinTopup"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //                }

        //            }
        //            else { }

        //        }
        //        catch (Exception ex)
        //        {
        //            TempData["EpinTopup"] = ex.Message;
        //        }
        //        return RedirectToAction("TopUpByPin", "Admin");
        //    }
        //    public ActionResult GetMemberName(string LoginId)
        //    {
        //        Common obj = new Common();
        //        obj.ReferBy = LoginId;
        //        DataSet ds = obj.GetMemberDetails();
        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {


        //            obj.DisplayName = ds.Tables[0].Rows[0]["FullName"].ToString();

        //            obj.Result = "Yes";

        //        }
        //        else { obj.Result = "No"; }
        //        return Json(obj, JsonRequestBehavior.AllowGet);
        //    }
        //    public ActionResult FillAmount(string ProductId)
        //    {
        //        Wallet obj = new Wallet();
        //        obj.Package = ProductId;
        //        DataSet ds = obj.BindPriceByProduct();
        //        if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            obj.Amount = ds.Tables[0].Rows[0]["ProductPrice"].ToString();
        //        }
        //        else { }
        //        return Json(obj, JsonRequestBehavior.AllowGet);

        //    }
        //    public ActionResult UsedPins()
        //    {
        //        Wallet objewallet = new Wallet();


        //        objewallet.Status = "Used";
        //        List<Wallet> lst = new List<Wallet>();
        //        DataSet ds = objewallet.GetUsedUnUsedPins();
        //        if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
        //        {

        //            foreach (DataRow dr in ds.Tables[0].Rows)
        //            {
        //                Wallet Objload = new Wallet();
        //                Objload.ePinNo = dr["ePinNo"].ToString();

        //                Objload.Package = dr["PinType"].ToString();

        //                Objload.DisplayName = dr["tOwner"].ToString();
        //                Objload.AddedOn = dr["CreatedDate"].ToString();
        //                Objload.RegisteredTo = dr["tRegTo"].ToString();
        //                Objload.Status = dr["PinStatus"].ToString();
        //                lst.Add(Objload);
        //            }
        //            objewallet.lstunusedpins = lst;
        //        }
        //        #region Product Bind
        //        Common objcomm = new Common();
        //        List<SelectListItem> ddlProduct = new List<SelectListItem>();
        //        DataSet ds1 = objcomm.BindProduct();
        //        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
        //        {
        //            int count = 0;
        //            foreach (DataRow r in ds1.Tables[0].Rows)
        //            {
        //                if (count == 0)
        //                {
        //                    ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
        //                }
        //                ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
        //                count++;
        //            }
        //        }

        //        ViewBag.ddlProduct = ddlProduct;

        //        #endregion
        //        return View(objewallet);
        //    }
        //    [HttpPost]
        //    [ActionName("UsedPins")]
        //    [OnAction(ButtonName = "Search")]
        //    public ActionResult UsedPinsBy(Wallet objewallet)
        //    {



        //        objewallet.Status = "Used";
        //        objewallet.Package = objewallet.Package == "0" ? null : objewallet.Package;
        //        List<Wallet> lst = new List<Wallet>();
        //        DataSet ds = objewallet.GetUsedUnUsedPins();
        //        if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
        //        {

        //            foreach (DataRow dr in ds.Tables[0].Rows)
        //            {
        //                Wallet Objload = new Wallet();
        //                Objload.ePinNo = dr["ePinNo"].ToString();

        //                Objload.Package = dr["PinType"].ToString();

        //                Objload.DisplayName = dr["tOwner"].ToString();
        //                Objload.AddedOn = dr["CreatedDate"].ToString();
        //                Objload.RegisteredTo = dr["tRegTo"].ToString();
        //                Objload.Status = dr["PinStatus"].ToString();
        //                lst.Add(Objload);
        //            }
        //            objewallet.lstunusedpins = lst;
        //        }
        //        #region Product Bind
        //        Common objcomm = new Common();
        //        List<SelectListItem> ddlProduct = new List<SelectListItem>();
        //        DataSet ds1 = objcomm.BindProduct();
        //        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
        //        {
        //            int count = 0;
        //            foreach (DataRow r in ds1.Tables[0].Rows)
        //            {
        //                if (count == 0)
        //                {
        //                    ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
        //                }
        //                ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
        //                count++;
        //            }
        //        }

        //        ViewBag.ddlProduct = ddlProduct;

        //        #endregion
        //        return View(objewallet);
        //    }

        //    public ActionResult TransferPin(string Epin)
        //    {
        //        Wallet obj = new Wallet();
        //        obj.ePinNo = Crypto.Decrypt(Epin);
        //        return View(obj);
        //    }

        //    public ActionResult TransferPinAction(Wallet obj)
        //    {
        //        try
        //        {
        //            // obj.Fk_UserId = Session["Pk_UserId"].ToString();
        //            DataSet ds = obj.TransferPin();
        //            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
        //            {
        //                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
        //                {
        //                    TempData["TransferPin"] = "Pin Transfer Successfull.";
        //                }
        //                else
        //                {
        //                    TempData["TransferPin"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //                }

        //            }
        //            else { }

        //        }
        //        catch (Exception ex)
        //        {
        //            TempData["TransferPin"] = ex.Message;
        //        }

        //        return RedirectToAction("TransferPin");
        //    }
        //    #endregion PinManagement

        //    #region AdvancePayment

        //    [HttpPost]
        //    [ActionName("AdvancePayment")]
        //    [OnAction(ButtonName = "btnSavePayment")]
        //    public ActionResult SaveAdvancePayment(Wallet model)
        //    {
        //        try
        //        {
        //            model.AddedBy = Session["Pk_AdminId"].ToString();
        //            model.TransactionDate = string.IsNullOrEmpty(model.TransactionDate) ? null : Common.ConvertToSystemDate(model.TransactionDate, "dd/MM/yyyy");

        //            DataSet ds = model.SaveAdvancePayment();
        //            if (ds != null && ds.Tables[0].Rows.Count > 0)
        //            {
        //                if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
        //                {
        //                    TempData["Class"] = "alert alert-success";
        //                    TempData["Advance"] = "Payment saved successfully.";
        //                }
        //                else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
        //                {
        //                    TempData["Class"] = "alert alert-danger";
        //                    TempData["Advance"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            TempData["Class"] = "alert alert-danger";
        //            TempData["Advance"] = ex.Message;
        //        }
        //        return RedirectToAction("AdvancePayment");
        //    }

        //    public ActionResult AdvancePaymentReport()
        //    {
        //        Wallet model = new Wallet();
        //        try
        //        {
        //            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
        //            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
        //            DataSet ds = model.AdvancePaymentReport();
        //            if (ds != null && ds.Tables[0].Rows.Count > 0)
        //            {
        //                List<Wallet> lstReport = new List<Wallet>();
        //                foreach (DataRow r in ds.Tables[0].Rows)
        //                {
        //                    Wallet obj = new Wallet();
        //                    obj.LoginId = r["LoginID"].ToString();
        //                    obj.DisplayName = r["FirstName"].ToString();
        //                    obj.Amount = r["Amount"].ToString();
        //                    obj.PaymentDate = r["PaymentDate"].ToString();
        //                    obj.PaymentMode = r["PaymentMode"].ToString();
        //                    obj.TransactionNo = r["PayMode"].ToString();
        //                    obj.Description = r["Description"].ToString();
        //                    lstReport.Add(obj);
        //                }
        //                model.lstewalletledger = lstReport;
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        return View(model);
        //    }
        //    [HttpPost]
        //    [ActionName("AdvancePaymentReport")]
        //    [OnAction(ButtonName = "btnDetails")]
        //    public ActionResult AdvancePaymentReportSearch(Wallet model)
        //    {

        //        try
        //        {
        //            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
        //            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
        //            DataSet ds = model.AdvancePaymentReport();
        //            if (ds != null && ds.Tables[0].Rows.Count > 0)
        //            {
        //                List<Wallet> lstReport = new List<Wallet>();
        //                foreach (DataRow r in ds.Tables[0].Rows)
        //                {
        //                    Wallet obj = new Wallet();
        //                    obj.LoginId = r["LoginID"].ToString();
        //                    obj.DisplayName = r["FirstName"].ToString();
        //                    obj.Amount = r["Amount"].ToString();
        //                    obj.PaymentDate = r["PaymentDate"].ToString();
        //                    obj.PaymentMode = r["PaymentMode"].ToString();
        //                    obj.Description = r["PayMode"].ToString();
        //                    lstReport.Add(obj);
        //                }
        //                model.lstewalletledger = lstReport;
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        return View(model);
        //    }
        //    #endregion

        //    public virtual PartialViewResult Tree()
        //    {
        //        ViewBag.Fk_UserId = "1";
        //        return PartialView("_Tree");
        //    }

        #region PayPayout
        public ActionResult PayPayout()
        {
            Reports model = new Reports();

            List<Reports> lst = new List<Reports>();
            DataSet ds = model.GetPayPayout();
            ViewBag.Total = "0";
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Name = r["Name"].ToString();
                    obj.BankHolderName = r["BankHolderName"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.MemberAccNo = r["MemberAccNo"].ToString();
                    obj.IFSCCode = (r["IFSCCode"].ToString());
                    obj.BankName = (r["MemberBankName"].ToString());
                    obj.Fk_UserId = (r["Pk_UserId"].ToString());
                    obj.Amount = (r["Amount"].ToString());
                    ViewBag.Total = Convert.ToDecimal(ViewBag.Total) + Convert.ToDecimal(r["Amount"].ToString());
                    lst.Add(obj);
                }
                model.lstassociate = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("PayPayout")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult GetPayPayout(Reports model)
        {
           
            model.LoginId = string.IsNullOrEmpty(model.LoginId) ? null : model.LoginId;
            model.Downline = model.IsDownline == true ? "1" : "0";
            model.Direct = model.IsDirect == true ? "1" : "0";
            List<Reports> lst = new List<Reports>();
            DataSet ds = model.GetPayPayout();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Name = r["Name"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.MemberAccNo = r["MemberAccNo"].ToString();
                    obj.IFSCCode = (r["IFSCCode"].ToString());
                    obj.BankName = (r["MemberBankName"].ToString());
                    obj.Fk_UserId = (r["Pk_UserId"].ToString());
                    obj.Amount = (r["Amount"].ToString());
                    ViewBag.Total = Convert.ToDecimal(ViewBag.Total) + Convert.ToDecimal(r["Amount"].ToString());
                    lst.Add(obj);
                }
                model.lstassociate = lst;
            }


            return View(model);
        }

        //Export to Excel for Pay Payout
        [HttpPost]
        [ActionName("PayPayout")]
        [OnAction(ButtonName = "Export")]
        public ActionResult ExportToExcelPayout(Reports model)
        {
            model.LoginId = string.IsNullOrEmpty(model.LoginId) ? null : model.LoginId;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            List<Reports> lst = new List<Reports>();
            DataSet ds = model.GetPayPayout();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string filename = "PayPayout" + ".xls";
                GridView GridView1 = new GridView();
                ds.Tables[0].Columns.Remove("Pk_UserID");
                ds.Tables[0].Columns.Remove("MemberBranch");
                ds.Tables[0].Columns.Remove("BankHolderName");
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                string style = @" .text { mso-number-format:\@; }  ";
                //string style = @"<style> td { mso-number-format:\@; } </style> ";

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=MemberDetailsReport.xls");
                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + "");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                System.IO.StringWriter s_Write = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter h_write = new HtmlTextWriter(s_Write);
                GridView1.ShowHeader = true;
                GridView1.RenderControl(h_write);
                Response.Write(style);
                Response.Write(s_Write.ToString());
                Response.End();

            }

            return null;
        }

        [HttpPost]
        [ActionName("PayPayout")]
        [OnAction(ButtonName = "Save")]
        public ActionResult PayPayoutAction(Reports model)
        {
            string hdrows2 = Request["hdRows2"].ToString();
            string amount = "";
            string description = "";
            string transactiono = "";
            string transactiondate = "";
            string Pk_PaidBoosterId_ = "";
            for (int i = 1; i < int.Parse(hdrows2); i++)
            {
                Pk_PaidBoosterId_ = Request["Fk_UserId_ " + i].ToString();
                amount = "";

                transactiono = Request["txttranno_ " + i].ToString();
                transactiondate = Request["txttransdate_ " + i].ToString();
                model.Amount = Request["txtamount_ " + i].ToString();
                model.Fk_UserId = Pk_PaidBoosterId_;
                
                model.TransactionNo = transactiono;
                DataSet ds = null;
                if (!string.IsNullOrEmpty(transactiondate))
                {
                    model.TransactionDate = transactiondate;
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    ds = model.SavePayPayout();
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["PayPayout"] = "Payment Done";
                    }
                    else
                    {
                        TempData["BoosterPay"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
          
            return RedirectToAction("PayPayout");
        }
        #endregion

        //#region PaidPayout
        //public ActionResult PaidPayout()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ActionName("PaidPayout")]
        //[OnAction(ButtonName = "GetDetails")]
        //public ActionResult GetPaidPayout(Wallet objewallet)
        //{
        //    List<Wallet> lst = new List<Wallet>();
        //    objewallet.FromDate = string.IsNullOrEmpty(objewallet.FromDate) ? null : Common.ConvertToSystemDate(objewallet.FromDate, "dd/MM/yyyy");
        //    objewallet.ToDate = string.IsNullOrEmpty(objewallet.ToDate) ? null : Common.ConvertToSystemDate(objewallet.ToDate, "dd/MM/yyyy");
        //    DataSet ds = objewallet.GetPaidPayout();
        //    ViewBag.Total = "0";
        //    if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            Wallet Objload = new Wallet();
        //            Objload.LoginId = dr["Loginid"].ToString();
        //            Objload.DisplayName = dr["Name"].ToString();
        //            Objload.PaymentDate = dr["Paymentdate"].ToString();

        //            Objload.Amount = dr["Amount"].ToString();
        //            Objload.TransactionDate = dr["TransactionDate"].ToString();
        //            Objload.TransactionNo = dr["TransactionNo"].ToString();
        //            ViewBag.Total = Convert.ToDecimal(ViewBag.Total) + Convert.ToDecimal(dr["Amount"].ToString());
        //            lst.Add(Objload);
        //        }
        //        objewallet.lstpayoutledger = lst;
        //    }
        //    return View(objewallet);
        //}
        //#endregion



        //    #region  Benefit
        //    public ActionResult Benefit(Reports model)
        //    {
        //        Common obj = new Common();
        //        List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
        //        ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
        //        DataSet ds2 = obj.GetPaymentMode();
        //        if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow r in ds2.Tables[0].Rows)
        //            {



        //                ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PaymentMode"].ToString() });

        //            }
        //        }

        //        ViewBag.ddlpaymentmode = ddlpaymentmode;
        //        return View();
        //    }
        //    [HttpPost]
        //    [ActionName("Benefit")]
        //    [OnAction(ButtonName = "GetDetails")]
        //    public ActionResult GetSalaryInstallment(Reports model)
        //    {
        //        Common obj = new Common();
        //        List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
        //        ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
        //        DataSet ds2 = obj.GetPaymentMode();
        //        if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow r in ds2.Tables[0].Rows)
        //            {
        //                ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PaymentMode"].ToString() });

        //            }
        //        }

        //        ViewBag.ddlpaymentmode = ddlpaymentmode;
        //        List<Reports> lst1 = new List<Reports>();

        //        DataSet ds11 = model.GetSalaryInstallment();

        //        if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[1].Rows.Count > 0)
        //        {
        //            Session["LastInst"] = null;
        //            Session["LastInstName"] = ds11.Tables[1].Rows[0]["InstallmentNo"].ToString();
        //        }
        //        else
        //        {
        //            Session["LastInst"] = null;
        //            Session["LastInstName"] = "0";

        //        }

        //        if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow r in ds11.Tables[0].Rows)
        //            {

        //                Reports Obj = new Reports();

        //                Obj.InstallmentNo = r["InstallmentNo"].ToString();
        //                Obj.InstallmentDate = r["InstallmentDate"].ToString();
        //                Obj.InstAmt = r["InstAmt"].ToString();
        //                Obj.PaymentDate = r["PaymentDate"].ToString();
        //                Obj.PaidAmount = r["PaidAmount"].ToString();
        //                Obj.Ispaid = r["Ispaid"].ToString();


        //                lst1.Add(Obj);
        //            }
        //            model.lstassociate = lst1;
        //        }
        //        return View(model);
        //    }
        //    [HttpPost]
        //    [ActionName("Benefit")]
        //    [OnAction(ButtonName = "Save")]
        //    public ActionResult SaveBenefit(Reports obj)
        //    {
        //        string FormName = "";
        //        string Controller = "";

        //        try
        //        {

        //            obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
        //            obj.AddedBy = Session["Pk_AdminId"].ToString();
        //            obj.PaymentDate = Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
        //            DataSet ds = obj.SaveBenefit();
        //            if (ds != null && ds.Tables.Count > 0)
        //            {
        //                if (ds.Tables[0].Rows[0][0].ToString() == "1")
        //                {
        //                    TempData["Benefit"] = "   Salary Saved successfully !";

        //                }
        //                else
        //                {
        //                    TempData["Benefit"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //                }
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            TempData["Benefit"] = ex.Message;
        //        }
        //        FormName = "Benefit";
        //        Controller = "Admin";

        //        return RedirectToAction(FormName, Controller);
        //    }

        //    public ActionResult ConcatenateInst(string instno, string CheckboxStatus, string LoginId)
        //    {
        //        Reports obj = new Reports();
        //        if (CheckboxStatus == "checked")
        //        {
        //            int lastmonth = int.Parse(Session["LastInstName"].ToString()) + 1;
        //            if (lastmonth != int.Parse(instno))
        //            {
        //                obj.Result = "3";
        //                obj.InstallmentNo = instno;
        //                obj.ErrorMessage = "Please select Installment in Sequence Order";
        //                return Json(obj, JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                Session["LastInstName"] = int.Parse(Session["LastInstName"].ToString()) + 1;

        //            }
        //        }
        //        else
        //        {
        //            if (int.Parse(Session["LastInstName"].ToString()) == int.Parse(instno))
        //            {

        //                Session["LastInstName"] = int.Parse(Session["LastInstName"].ToString()) - 1;
        //            }
        //            else
        //            {

        //                obj.Result = "4";
        //                obj.InstallmentNo = instno;
        //                obj.ErrorMessage = "Please Uncheck in Sequence Order By Last.";
        //                return Json(obj, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        if (Session["LastInst"] == null || Session["LastInst"] == "")
        //        {
        //            Session["LastInst"] = null;
        //            Session["LastInst"] = ',' + instno + ',';
        //        }
        //        else
        //        {
        //            if (Session["LastInst"].ToString().Contains(',' + instno + ','))
        //            {
        //                Session["LastInst"] = Session["LastInst"].ToString().Replace(',' + instno + ',', ",");

        //            }
        //            else
        //            {
        //                Session["LastInst"] = ',' + instno + Session["LastInst"].ToString();
        //            }
        //        }

        //        obj.InstallmentNo = Session["LastInst"].ToString();
        //        Reports model = new Reports();
        //        model.LoginId = LoginId;
        //        model.InstallmentNo = obj.InstallmentNo;
        //        DataSet ds11 = model.GetSalaryInstallment();
        //        if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[2].Rows.Count > 0)
        //        {
        //            obj.Amount = ds11.Tables[2].Rows[0]["InstAmt"].ToString();
        //        }
        //        return Json(obj, JsonRequestBehavior.AllowGet);
        //    }
        //    #endregion

        //    #region  Salary
        //    public ActionResult Salary(Reports model)
        //    {
        //        Common obj = new Common();
        //        List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
        //        ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
        //        DataSet ds2 = obj.GetPaymentMode();
        //        if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow r in ds2.Tables[0].Rows)
        //            {



        //                ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PaymentMode"].ToString() });

        //            }
        //        }

        //        ViewBag.ddlpaymentmode = ddlpaymentmode;
        //        return View();
        //    }

        //    [HttpPost]
        //    [ActionName("Salary")]
        //    [OnAction(ButtonName = "GetDetails")]
        //    public ActionResult GetSalaryIns(Reports model)
        //    {
        //        Common obj = new Common();
        //        List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
        //        ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
        //        DataSet ds2 = obj.GetPaymentMode();
        //        if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow r in ds2.Tables[0].Rows)
        //            {
        //                ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PaymentMode"].ToString() });

        //            }
        //        }

        //        ViewBag.ddlpaymentmode = ddlpaymentmode;
        //        List<Reports> lst1 = new List<Reports>();

        //        DataSet ds11 = model.GetSalaryInstDetails();

        //        if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[1].Rows.Count > 0)
        //        {
        //            Session["LastInst"] = null;
        //            Session["LastInstName"] = ds11.Tables[1].Rows[0]["InstallmentNo"].ToString();
        //        }
        //        else
        //        {
        //            Session["LastInst"] = null;
        //            Session["LastInstName"] = "0";

        //        }

        //        if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow r in ds11.Tables[0].Rows)
        //            {

        //                Reports Obj = new Reports();

        //                Obj.InstallmentNo = r["InstallmentNo"].ToString();
        //                Obj.InstallmentDate = r["InstallmentDate"].ToString();
        //                Obj.InstAmt = r["InstAmt"].ToString();
        //                Obj.PaymentDate = r["PaymentDate"].ToString();
        //                Obj.PaidAmount = r["PaidAmount"].ToString();
        //                Obj.Ispaid = r["Ispaid"].ToString();


        //                lst1.Add(Obj);
        //            }
        //            model.lstassociate = lst1;
        //        }
        //        return View(model);
        //    }

        //    [HttpPost]
        //    [ActionName("Salary")]
        //    [OnAction(ButtonName = "Save")]
        //    public ActionResult SaveSalary(Reports obj)
        //    {
        //        string FormName = "";
        //        string Controller = "";

        //        try
        //        {

        //            obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
        //            obj.AddedBy = Session["Pk_AdminId"].ToString();
        //            obj.PaymentDate = Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
        //            DataSet ds = obj.SaveSalary();
        //            if (ds != null && ds.Tables.Count > 0)
        //            {
        //                if (ds.Tables[0].Rows[0][0].ToString() == "1")
        //                {
        //                    TempData["Salary"] = "   Salary Saved successfully !";

        //                }
        //                else
        //                {
        //                    TempData["Salary"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //                }
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            TempData["Salary"] = ex.Message;
        //        }
        //        FormName = "Salary";
        //        Controller = "Admin";

        //        return RedirectToAction(FormName, Controller);
        //    }

        //    public ActionResult ConcatenateSalInst(string instno, string CheckboxStatus, string LoginId)
        //    {
        //        Reports obj = new Reports();
        //        if (CheckboxStatus == "checked")
        //        {
        //            int lastmonth = int.Parse(Session["LastInstName"].ToString()) + 1;
        //            if (lastmonth != int.Parse(instno))
        //            {
        //                obj.Result = "3";
        //                obj.InstallmentNo = instno;
        //                obj.ErrorMessage = "Please select Installment in Sequence Order";
        //                return Json(obj, JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                Session["LastInstName"] = int.Parse(Session["LastInstName"].ToString()) + 1;

        //            }
        //        }
        //        else
        //        {
        //            if (int.Parse(Session["LastInstName"].ToString()) == int.Parse(instno))
        //            {

        //                Session["LastInstName"] = int.Parse(Session["LastInstName"].ToString()) - 1;
        //            }
        //            else
        //            {

        //                obj.Result = "4";
        //                obj.InstallmentNo = instno;
        //                obj.ErrorMessage = "Please Uncheck in Sequence Order By Last.";
        //                return Json(obj, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        if (Session["LastInst"] == null || Session["LastInst"] == "")
        //        {
        //            Session["LastInst"] = null;
        //            Session["LastInst"] = ',' + instno + ',';
        //        }
        //        else
        //        {
        //            if (Session["LastInst"].ToString().Contains(',' + instno + ','))
        //            {
        //                Session["LastInst"] = Session["LastInst"].ToString().Replace(',' + instno + ',', ",");

        //            }
        //            else
        //            {
        //                Session["LastInst"] = ',' + instno + Session["LastInst"].ToString();
        //            }
        //        }

        //        obj.InstallmentNo = Session["LastInst"].ToString();
        //        Reports model = new Reports();
        //        model.LoginId = LoginId;
        //        model.InstallmentNo = obj.InstallmentNo;
        //        DataSet ds11 = model.GetSalaryInstDetails();
        //        if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[2].Rows.Count > 0)
        //        {
        //            obj.Amount = ds11.Tables[2].Rows[0]["InstAmt"].ToString();
        //        }
        //        return Json(obj, JsonRequestBehavior.AllowGet);
        //    }
        //    #endregion

        //    public ActionResult ValidatePancard(string PanCard)
        //    {
        //        Home obj = new Home();
        //        obj.PanCard = PanCard;
        //        DataSet ds = obj.ValidatePan();
        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
        //            {
        //                obj.Response = "1";
        //                obj.PanCard = PanCard;
        //            }
        //            else
        //            {
        //                obj.Response = "Pancard already Registered";
        //            }
        //        }
        //        return Json(obj, JsonRequestBehavior.AllowGet);
        //    }


        //    public ActionResult ValidateMobileNo(string MobileNo)
        //    {
        //        Home obj = new Home();
        //        obj.MobileNo = MobileNo;
        //        DataSet ds = obj.ValidateMobile();
        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
        //            {
        //                obj.Response = "1";
        //                obj.MobileNo = MobileNo;
        //            }
        //            else
        //            {
        //                obj.Response = "Mobile No already Registered";
        //            }
        //        }
        //        return Json(obj, JsonRequestBehavior.AllowGet);
        //    }
        public ActionResult GetGraphDetails()
        {
            List<DashBoard> dataList = new List<DashBoard>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            DashBoard newdata = new DashBoard();

            Ds = newdata.BindGraphDetails();
            if (Ds.Tables.Count > 0)
            {

                int count = 0;
                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    DashBoard details = new DashBoard();


                    details.Total = (dr["Total"].ToString());
                    details.Status = (dr["Status"].ToString());


                    dataList.Add(details);

                    count++;
                }
            }
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGraphDetailsAssociate()
        {
            List<DashBoard> dataList2 = new List<DashBoard>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            DashBoard newdata = new DashBoard();

            Ds = newdata.BindGraphDetailsAssociate();
            if (Ds.Tables.Count > 0)
            {

                int count = 0;
                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    DashBoard details = new DashBoard();


                    details.Total = (dr["Total"].ToString());
                    details.Status = (dr["Status"].ToString());


                    dataList2.Add(details);

                    count++;
                }
            }
            return Json(dataList2, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAssociateJoiningDetails()
        {
            List<DashBoard> dataList3 = new List<DashBoard>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            DashBoard newdata = new DashBoard();

            Ds = newdata.GetAssociateJoining();
            if (Ds.Tables.Count > 0)
            {
                ViewBag.TotalUsers = Ds.Tables[0].Rows.Count;
                int count = 0;
                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    DashBoard details = new DashBoard();


                    details.TotalUser = (dr["TotalUser"].ToString());
                    details.Month = (dr["Month"].ToString());


                    dataList3.Add(details);

                    count++;
                }
            }
            return Json(dataList3, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult BookingList()
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
            return View(obj);
        }
        public ActionResult RegistrationAction(string SponsorId, string FirstName, string LastName, string Email, string MobileNo, string PanCard, string Address, string Gender, string OTP, string Pincode, string Leg)
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
                obj.Address = Address;
                obj.RegistrationBy = "Web";
                obj.Gender = Gender;
                obj.Pincode = Pincode;
                obj.Leg = Leg;
                string password = Common.GenerateRandom();
                obj.Password = Crypto.Encrypt(password);

                DataSet ds = obj.Registration();
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
        public ActionResult BlockUser(Home obj, string LoginID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.BlockAssociate();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "Associate Blocked";
                        FormName = "AssociateList";
                        Controller = "TraditionalAssociate";
                    }
                    else
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AssociateList";
                        Controller = "TraditionalAssociate";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
                FormName = "AssociateList";
                Controller = "TraditionalAssociate";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult UnblockAssociate(Home obj, string LoginID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UnblockAssociate();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "Associate UnBlocked";
                        FormName = "AssociateList";
                        Controller = "TraditionalAssociate";
                    }
                    else
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AssociateList";
                        Controller = "TraditionalAssociate";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
                FormName = "AssociateList";
                Controller = "TraditionalAssociate";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult ActivateUser(string FK_UserID)
        {
            Home model = new Home();
            try
            {
                model.Fk_UserId = FK_UserID;
                model.ProductID = "1";
                model.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.ActivateUserByAdmin();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "User activated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
            }
            return RedirectToAction("AssociateList", "TraditionalAssociate");
        }
        public ActionResult DeactivateUser(string lid)
        {
            Home model = new Home();
            try
            {
                model.LoginId = lid;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.DeactivateUserByAdmin();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "User deactivated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
            }
            return RedirectToAction("AssociateList", "TraditionalAssociate");
        }
        public ActionResult BinaryTree()
        {
            ViewBag.Fk_UserId = "1";
            return View();
        }
        #region PaidPayout
        public ActionResult PaidPayout()
        {
            return View();
        }
        [HttpPost]
        [ActionName("PaidPayout")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult GetPaidPayout(Wallet objewallet)
        {
            List<Wallet> lst = new List<Wallet>();
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
        #region distributePayment

        public ActionResult DistributePayment()
        {
            Wallet model = new Wallet();
            List<AssociateBooking> lst = new List<AssociateBooking>();

            //ViewBag.Binary = ViewBag.Direct = ViewBag.Gross = ViewBag.TDS = ViewBag.Processing = ViewBag.NetIncome = 0;
            DataSet ds = model.GetDitributePaymentList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.ToID = r["LoginId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.Income = r["Income"].ToString();
                    obj.GrossAmount = r["GrossIncome"].ToString();
                    //obj.DirectIncome = r["DirectIncome"].ToString();
                    //obj.DifferentialIncome = r["DifferentialIncome"].ToString();
                    //obj.DirectLeadershipIncome = r["DirectLeadershipIncome"].ToString();
                    obj.Processing = r["Processing"].ToString();
                    obj.TDS = r["TDS"].ToString();
                    lst.Add(obj);
                }
                model.lstdistribute = lst;
                ViewBag.Total = double.Parse(ds.Tables[0].Compute("sum(Income)", "").ToString()).ToString("n2");
            }
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in ds.Tables[0].Rows)
            //    {
            //        Wallet obj = new Wallet();
            //        obj.LoginId = r["LoginId"].ToString();
            //        obj.FirstName = r["FirstName"].ToString();
            //        obj.BinaryIncome = r["BinaryIncome"].ToString();
            //        obj.DirectIncome = r["DirectIncome"].ToString();
            //        obj.GrossIncome = (r["GrossIncome"].ToString());
            //        obj.TDS = (r["TDS"].ToString());
            //        obj.Processing = (r["Processing"].ToString());
            //        obj.NetIncome = (r["NetIncome"].ToString());

            //        obj.LeadershipBonus = r["DirectLeaderShipBonus"].ToString();
            //        ViewBag.Binary = Convert.ToDecimal(ViewBag.Binary) + Convert.ToDecimal(r["BinaryIncome"].ToString());
            //        ViewBag.Direct = Convert.ToDecimal(ViewBag.Direct) + Convert.ToDecimal(r["DirectIncome"].ToString());
            //        ViewBag.Gross = Convert.ToDecimal(ViewBag.Gross) + Convert.ToDecimal(r["GrossIncome"].ToString());
            //        ViewBag.TDS = Convert.ToDecimal(ViewBag.TDS) + Convert.ToDecimal(r["TDS"].ToString());
            //        ViewBag.Processing = Convert.ToDecimal(ViewBag.Processing) + Convert.ToDecimal(r["Processing"].ToString());
            //        ViewBag.NetIncome = Convert.ToDecimal(ViewBag.NetIncome) + Convert.ToDecimal(r["NetIncome"].ToString());

            //        lst.Add(obj);
            //    }
            //    model.lstassociate = lst;

            //}
            model.LastClosingDate = ds.Tables[1].Rows[0]["ClosingDate"].ToString();
            model.PayoutNo = ds.Tables[1].Rows[0]["PayoutNo"].ToString();
            return View(model);
        }

        public ActionResult DistiributePayemntToMembers(Wallet obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.ClosingDate = Common.ConvertToSystemDate(obj.ClosingDate, "dd/MM/yyyy");
                obj.UpdatedBy = Session["PK_AdminId"].ToString();
                DataSet ds = obj.AutoDistributePayment();

                TempData["DistributePayment"] = "Payment distributed successfully";
                FormName = "DistributePayment";
                Controller = "Admin";
            }
            catch (Exception ex)
            {
                TempData["DistributePayment"] = ex.Message;
                FormName = "DistributePayment";
                Controller = "Admin";
            }

            return RedirectToAction(FormName, Controller);
        }

        [HttpPost]
        [ActionName("DistiributePayemntToMembers")]
        [OnAction(ButtonName = "Export")]
        public ActionResult ExportToExcel()
        {
            Wallet model = new Wallet();
            List<Wallet> lst = new List<Wallet>();

            ViewBag.Binary = ViewBag.Direct = ViewBag.Gross = ViewBag.TDS = ViewBag.Processing = ViewBag.NetIncome = 0;
            DataSet ds = model.GetDitributePaymentList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string filename = "DistributePayment.xls";
                GridView GridView1 = new GridView();
                //ds.Tables[0].Columns.Remove("Pk_PaidBoosterId");
                //ds.Tables[0].Columns.Remove("Description");
                //ds.Tables[0].Columns.Remove("TransactionNo");
                //ds.Tables[0].Columns.Remove("TransactionDate");
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                //string style = @" .text { mso-number-format:\@; }  ";
                string style = @"<style> td { mso-number-format:\@; } </style> ";

                Response.Clear();
                // Response.AddHeader("content-disposition", "attachment;filename=MemberDetailsReport.xls");
                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + "");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                System.IO.StringWriter s_Write = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter h_write = new HtmlTextWriter(s_Write);
                GridView1.ShowHeader = true;
                GridView1.RenderControl(h_write);
                Response.Write(style);
                Response.Write(s_Write.ToString());
                Response.End();

            }

            return null;
        }

        #endregion
        public ActionResult PayoutLedger()
        {

            return View();
        }
        [HttpPost]
        [ActionName("PayoutLedger")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult PayoutLedgerBy(Wallet objewallet)
        {


            objewallet.FromDate = string.IsNullOrEmpty(objewallet.FromDate) ? null : Common.ConvertToSystemDate(objewallet.FromDate, "dd/MM/yyyy");
            objewallet.ToDate = string.IsNullOrEmpty(objewallet.ToDate) ? null : Common.ConvertToSystemDate(objewallet.ToDate, "dd/MM/yyyy");
            List<Wallet> lst = new List<Wallet>();
            DataSet ds = objewallet.PayoutLedger();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.Narration = dr["Narration"].ToString();
                    Objload.DrAmount = dr["Debit"].ToString();
                    Objload.CrAmount = dr["Credit"].ToString();
                    Objload.AddedOn = dr["TransactionDate"].ToString();
                    Objload.PayoutBalance = dr["Balance"].ToString();

                    lst.Add(Objload);
                }
                objewallet.lstpayoutledger = lst;
            }
            return View(objewallet);
        }
        public ActionResult ConfirmRegistration()
        {
            return View();
        }
        #region  Benefit
        public ActionResult Benefit(Reports model)
        {
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
            return View();
        }
        [HttpPost]
        [ActionName("Benefit")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult GetSalaryInstallment(Reports model)


        {
            Common obj = new Common();
            List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
            ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
            DataSet ds2 = obj.GetPaymentMode();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });

                }
            }

            ViewBag.ddlpaymentmode = ddlpaymentmode;
            List<Reports> lst1 = new List<Reports>();

            DataSet ds11 = model.GetDataForBenefitDetails();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[1].Rows.Count > 0)
            {
                Session["LastInst"] = null;
                Session["LastInstName"] = ds11.Tables[1].Rows[0]["InstallmentNo"].ToString();
            }
            else
            {
                Session["LastInst"] = null;
                Session["LastInstName"] = "0";

            }

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {

                    Reports Obj = new Reports();

                    Obj.InstallmentNo = r["InstallmentNo"].ToString();
                    Obj.InstallmentDate = r["InstallmentDate"].ToString();
                    Obj.InstAmt = r["InstAmt"].ToString();
                    Obj.PaymentDate = r["PaymentDate"].ToString();
                    Obj.PaidAmount = r["PaidAmount"].ToString();
                    Obj.Ispaid = r["Ispaid"].ToString();


                    lst1.Add(Obj);
                }
                model.lstassociate = lst1;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("Benefit")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveBenefit(Reports obj)
        {
            string FormName = "";
            string Controller = "";

            try
            {

                obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.PaymentDate = Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
                DataSet ds = obj.SaveBenefit();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Benefit"] = "   Benefit Saved successfully !";

                    }
                    else
                    {
                        TempData["Benefit"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["Benefit"] = ex.Message;
            }
            FormName = "Benefit";
            Controller = "Admin";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult ConcatenateInst(string instno, string CheckboxStatus, string LoginId)
        {
            Reports obj = new Reports();
            if (CheckboxStatus == "checked")
            {
                int lastmonth = int.Parse(Session["LastInstName"].ToString()) + 1;
                if (lastmonth != int.Parse(instno))
                {
                    obj.Result = "3";
                    obj.InstallmentNo = instno;
                    obj.ErrorMessage = "Please select Installment in Sequence Order";
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Session["LastInstName"] = int.Parse(Session["LastInstName"].ToString()) + 1;

                }
            }
            else
            {
                if (int.Parse(Session["LastInstName"].ToString()) == int.Parse(instno))
                {

                    Session["LastInstName"] = int.Parse(Session["LastInstName"].ToString()) - 1;
                }
                else
                {

                    obj.Result = "4";
                    obj.InstallmentNo = instno;
                    obj.ErrorMessage = "Please Uncheck in Sequence Order By Last.";
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            if (Session["LastInst"] == null || Session["LastInst"] == "")
            {
                Session["LastInst"] = null;
                Session["LastInst"] = ',' + instno + ',';
            }
            else
            {
                if (Session["LastInst"].ToString().Contains(',' + instno + ','))
                {
                    Session["LastInst"] = Session["LastInst"].ToString().Replace(',' + instno + ',', ",");

                }
                else
                {
                    Session["LastInst"] = ',' + instno + Session["LastInst"].ToString();
                }
            }

            obj.InstallmentNo = Session["LastInst"].ToString();
            Reports model = new Reports();
            model.LoginId = LoginId;
            model.InstallmentNo = obj.InstallmentNo;
            DataSet ds11 = model.GetDataForBenefitDetails();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[2].Rows.Count > 0)
            {
                obj.Amount = ds11.Tables[2].Rows[0]["InstAmt"].ToString();
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion



        public ActionResult ContactList(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetContactList();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Master Objload = new Master();
                    Objload.Pk_ContactId = dr["Pk_ContactId"].ToString();
                    Objload.Name = dr["Name"].ToString();
                    Objload.Email = dr["Email"].ToString();
                    Objload.Mobile = dr["Mobile"].ToString();
                    Objload.Address = dr["Address"].ToString();
                    Objload.CreatedDate = dr["CreatedDate"].ToString();
                    lst.Add(Objload);
                }
                model.lstContact = lst;
            }
            return View(model);
        }
        
        public ActionResult DeleteContactDetails(string id)
        {
            try
            {
                Master model = new Master();
                model.Pk_ContactId = id;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteContact();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Contactmsg"] = "Contact details deleted successfully !";
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        TempData["Contactmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                    else
                    {
                        TempData["Contactmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Contactmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["Contactmsg"] = ex.Message;
            }
            return RedirectToAction("ContactList", "Admin");
        }
        
        public ActionResult VisitorForm()
        {
            Plot model = new Plot();
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
            return View();
        }

        [HttpPost]
        public JsonResult save(Plot obj, string dataValue, string SiteID, string AssociateID, string AssociateName, string Amount, string VisiteDate)
        {

            obj.VisitDate = string.IsNullOrEmpty(VisiteDate) ? null : Common.ConvertToSystemDate(VisiteDate, "dd/MM/yyyy");
            bool status = false;
            var isValidModel = TryUpdateModel(obj);
            var jss = new JavaScriptSerializer();
            var jdv = jss.Deserialize<dynamic>(dataValue);

            //var serializeData = JsonConvert.DeserializeObject<List<Customer>>(empdata);
            //System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
            DataTable VisitorDetails = new DataTable();

            VisitorDetails = JsonConvert.DeserializeObject<DataTable>(jdv["AddData"]);
            obj.dtVisitorDetails = VisitorDetails;
            obj.AddedBy = Session["Pk_AdminId"].ToString();

            DataSet ds = new DataSet();
            ds = obj.SaveVisitorDetails();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    TempData["Visitor"] = "Visitor Details saved successfully";
                    status = true;
                }
                else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["Visitor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                TempData["DrExpenses"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
            }
            return new JsonResult { Data = new { status = status } };
        }


        public ActionResult VisitorList()
        {
            return View();
        }
        [HttpPost]
        [ActionName("VisitorList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult VisitorList(Plot model)
        {

            List<Plot> Visitorlist = new List<Plot>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.Downline = model.IsDownline == true ? "1" : "0";
            model.Direct = model.IsDirect == true ? "1" : "0";
            DataSet ds = model.VisitorList();
            ViewBag.Total = "0";
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_VisitorID"].ToString());
                    obj.AssociateName = r["AssociateName"].ToString();
                    //obj.Mobile = r["Mobile"].ToString();
                    //obj.CustomerName = r["CustomerName"].ToString();
                    obj.SiteName = r["SiteName"].ToString();
                    //obj.Address = r["Address"].ToString();
                    obj.VisitDate = r["VisitDate"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    Visitorlist.Add(obj);
                    ViewBag.Total = Convert.ToDecimal(ViewBag.Total) + Convert.ToDecimal(r["Amount"].ToString());
                }
                model.lstVistor = Visitorlist;
            }
            return View(model);
        }

        public ActionResult PrintVisitor(string Id)
        {
            Plot newdata = new Plot();
            List<Plot> lstvisitor = new List<Plot>();
            newdata.PK_VisitorId = Crypto.Decrypt(Id);
            DataSet ds = newdata.VisitorListById();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.PK_VisitorID = ds.Tables[0].Rows[0]["PK_VisitorID"].ToString();
                ViewBag.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                ViewBag.AssociateName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                ViewBag.Amount = ds.Tables[0].Rows[0]["Amount"].ToString();
                ViewBag.VisitDate = ds.Tables[0].Rows[0]["VisitDate"].ToString();

                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[1].Rows)
                    {
                        Plot obj = new Plot();
                        obj.CustomerName = r["CustomerName"].ToString();
                        obj.Mobile = r["Mobile"].ToString();
                        obj.Address = r["Address"].ToString();
                        lstvisitor.Add(obj);
                    }
                }
                newdata.lstVistor = lstvisitor;
            }
            return View(newdata);
        }

        public ActionResult DeleteVisitor(string Id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Plot model = new Plot();
                model.PK_VisitorId = Crypto.Decrypt(Id);
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteVisitor();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["VisitMsg"] = "Visitor deleted successfully !";
                    }
                    else
                    {
                        TempData["VisitMsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["VisitMsg"] = ex.Message;
            }
            FormName = "VisitorList";
            Controller = "Admin";
            return RedirectToAction(FormName, Controller);
        }

    }
}
