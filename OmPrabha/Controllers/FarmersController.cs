using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OmPrabha.Models;
using System.Data;
using OmPrabha.Filter;
using System.Net;
using System.Xml;
using System.Text;
using System.IO;

namespace OmPrabha.Controllers
{
    public class FarmersController : AdminBaseController
    {
        // GET: Farmers
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddFarmers(string id)
        {
            Farmer model = new Farmer();
            try
            {
                if (id != null)
                {


                    model.PK_Farmerid = Crypto.Decrypt(id);
                    DataSet ds = model.GetlistById();
                    if (ds.Tables.Count > 0 && ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                        model.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        model.Amount = ds.Tables[0].Rows[0]["Amount"].ToString();
                        model.DOB = ds.Tables[0].Rows[0]["DOB"].ToString();
                        model.SQFT = ds.Tables[0].Rows[0]["SQFT"].ToString();
                        model.Acre = ds.Tables[0].Rows[0]["Acre"].ToString();
                        model.Hectare = ds.Tables[0].Rows[0]["Hectare"].ToString();
                        model.Pincode = ds.Tables[0].Rows[0]["Pincode"].ToString();
                        model.Photo = ds.Tables[0].Rows[0]["Photo"].ToString();
                        model.IDProof = ds.Tables[0].Rows[0]["IDProof"].ToString();
                        model.City = ds.Tables[0].Rows[0]["City"].ToString();
                        model.State = ds.Tables[0].Rows[0]["State"].ToString();
                        model.DelearName = ds.Tables[0].Rows[0]["DelearName"].ToString();
                        model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                        model.PK_Farmerid = ds.Tables[0].Rows[0]["PK_Farmerid"].ToString();
                        model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                        model.AssociateID = ds.Tables[0].Rows[0]["AssociatId"].ToString();
                        model.AssociateLoginID = ds.Tables[0].Rows[0]["AssociatId"].ToString();
                        model.Addrss = ds.Tables[0].Rows[0]["Address"].ToString();
                        model.GataKhasaraN = ds.Tables[0].Rows[0]["GataKhasaraN"].ToString();
                        model.Village = ds.Tables[0].Rows[0]["Village"].ToString();
                        model.Registry = ds.Tables[0].Rows[0]["IsRegistry"].ToString();
                        model.Agreement = ds.Tables[0].Rows[0]["IsAgreement"].ToString();
                        model.Notary = ds.Tables[0].Rows[0]["IsNotary"].ToString();
                        model.SqMtr = ds.Tables[0].Rows[0]["SqMtr"].ToString();
                        model.Category = ds.Tables[0].Rows[0]["Category"].ToString();
                        model.LandType = ds.Tables[0].Rows[0]["LandType"].ToString();
                        model.KharijDakhil = ds.Tables[0].Rows[0]["KharijDakhil"].ToString();
                        model.LandDhara = ds.Tables[0].Rows[0]["LandDhara"].ToString();


                        //ddltitle.Add(new SelectListItem { Text = ds.Tables[0].Rows[0]["Title"].ToString(), Value = ds.Tables[0].Rows[0]["Title"].ToString(), Selected = true });
                        //ViewBag.FTitle = ddltitle;
                    }

                }
                List<SelectListItem> ddltitle = FTitle();
                ViewBag.FTitle = ddltitle;

                List<SelectListItem> ddlRegistry = Registry();
                ViewBag.ddlRegistry = ddlRegistry;

                List<SelectListItem> ddlAgreement = Agreement();
                ViewBag.ddlAgreement = ddlAgreement;

                List<SelectListItem> ddlNotary = Notary();
                ViewBag.ddlNotary = ddlNotary;

                List<SelectListItem> ddlCategory = Category();
                ViewBag.ddlCategory = ddlCategory;

                List<SelectListItem> ddlLandType = LandType();
                ViewBag.ddlLandType = ddlLandType;

                List<SelectListItem> ddlKharijDakhil = KharijDakhil();
                ViewBag.ddlKharijDakhil = ddlKharijDakhil;

                List<SelectListItem> ddlLandDhara = LandDhara();
                ViewBag.ddlLandDhara = ddlLandDhara;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(model);
        }
        [HttpPost]
        [ActionName("AddFarmers")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveFarmers(IEnumerable<HttpPostedFileBase> IDProof, IEnumerable<HttpPostedFileBase> Photo, Farmer obj)
        {
            try
            {
                foreach (var file in IDProof)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        obj.IDProof = "~/FarmerPhotoandIdproof/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(obj.IDProof)));
                    }
                }
                foreach (var photo in Photo)
                {
                    if (photo != null && photo.ContentLength > 0)
                    {
                        obj.Photo = "~/FarmerPhotoandIdproof/" + Guid.NewGuid() + Path.GetExtension(photo.FileName);
                        photo.SaveAs(Path.Combine(Server.MapPath(obj.Photo)));
                    }
                }
                obj.DOB = string.IsNullOrEmpty(obj.DOB) ? null : Common.ConvertToSystemDate(obj.DOB, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.SaveFarmerDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SucMsgFarmer"] = "Farmer Details Saved successfully";

                        obj = new Farmer();
                        ModelState.Clear();
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrMsgFarmer"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                List<SelectListItem> ddltitle = FTitle();
                ViewBag.FTitle = ddltitle;

                List<SelectListItem> ddlRegistry = Registry();
                ViewBag.ddlRegistry = ddlRegistry;

                List<SelectListItem> ddlAgreement = Agreement();
                ViewBag.ddlAgreement = ddlAgreement;

                List<SelectListItem> ddlNotary = Notary();
                ViewBag.ddlNotary = ddlNotary;

                List<SelectListItem> ddlCategory = Category();
                ViewBag.ddlCategory = ddlCategory;

                List<SelectListItem> ddlLandType = LandType();
                ViewBag.ddlLandType = ddlLandType;

                List<SelectListItem> ddlKharijDakhil = KharijDakhil();
                ViewBag.ddlKharijDakhil = ddlKharijDakhil;

                List<SelectListItem> ddlLandDhara = LandDhara();
                ViewBag.ddlLandDhara = ddlLandDhara;
            }
            catch (Exception ex)
            {
                TempData["MsgFarmer"] = ex.Message;
            }
            return RedirectToAction("AddFarmers", "Farmers");
        }

        [HttpPost]
        [ActionName("AddFarmers")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateFarmers(IEnumerable<HttpPostedFileBase> IDProof, IEnumerable<HttpPostedFileBase> Photo, Farmer obj)
        {

            try
            {
                foreach (var file in IDProof)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        obj.IDProof = "/FarmerPhotoandIdproof/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(obj.IDProof)));
                    }

                }
                foreach (var photo in Photo)
                {
                    if (photo != null && photo.ContentLength > 0)
                    {
                        obj.Photo = "/FarmerPhotoandIdproof/" + Guid.NewGuid() + Path.GetExtension(photo.FileName);
                        photo.SaveAs(Path.Combine(Server.MapPath(obj.Photo)));
                    }

                }
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.DOB = string.IsNullOrEmpty(obj.DOB) ? null : Common.ConvertToSystemDate(obj.DOB, "dd/MM/yyyy");

                DataSet ds = obj.UpdateFarmerDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SucMsgFarmer"] = "Farmer Details Updated successfully";
                        obj.PK_Farmerid = null;
                        obj = new Farmer();
                        ModelState.Clear();
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["ErrMsgFarmer"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }

            catch (Exception ex)
            {
                TempData["ErrMsgFarmer"] = ex.Message;
            }
            List<SelectListItem> ddltitle = FTitle();
            ViewBag.FTitle = ddltitle;

            List<SelectListItem> ddlRegistry = Registry();
            ViewBag.ddlRegistry = ddlRegistry;

            List<SelectListItem> ddlAgreement = Agreement();
            ViewBag.ddlAgreement = ddlAgreement;

            List<SelectListItem> ddlNotary = Notary();
            ViewBag.ddlNotary = ddlNotary;

            List<SelectListItem> ddlCategory = Category();
            ViewBag.ddlCategory = ddlCategory;

            List<SelectListItem> ddlLandType = LandType();
            ViewBag.ddlLandType = ddlLandType;

            List<SelectListItem> ddlKharijDakhil = KharijDakhil();
            ViewBag.ddlKharijDakhil = ddlKharijDakhil;

            List<SelectListItem> ddlLandDhara = LandDhara();
            ViewBag.ddlLandDhara = ddlLandDhara;
            //ModelState.Clear();
            return RedirectToAction("AddFarmers", "Farmers");
        }

        public ActionResult Farmerlist()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Farmerlist")]
        [OnAction(ButtonName = "Search")]
        public ActionResult FarmerList(Farmer model)
        {

            List<Farmer> lst = new List<Farmer>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.Getlist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Farmer obj = new Farmer();
                    obj.Name = r["Name"].ToString();
                    obj.Hectare = r["Hectare"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.City = r["City"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.FStatus = r["Status"].ToString();
                    //obj.Amount= (Convert.ToDecimal(Prc).ToString("C", model));
                    obj.IsActive = r["IsActive"].ToString();
                    obj.JoiningDate = r["AddedDate"].ToString();
                    obj.GataKhasaraN = r["GataKhasaraN"].ToString();
                    obj.Hectare = r["Hectare"].ToString();
                    obj.SqMtr = r["SqMtr"].ToString();
                    obj.Village = r["Village"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["Pk_FarmerID"].ToString());
                    lst.Add(obj);
                }
                model.FarmerList = lst;
            }

            return View(model);
        }

        public ActionResult GetSponsorName(string SponsorID)
        {
            try
            {
                Farmer model = new Farmer();
                model.AssociateID = SponsorID;

                #region GetSiteRate
                DataSet dsSponsorName = model.GetSponsorName();
                if (dsSponsorName != null && dsSponsorName.Tables[0].Rows.Count > 0)
                {
                    model.DelearName = dsSponsorName.Tables[0].Rows[0]["Name"].ToString();
                    model.AssociateID = dsSponsorName.Tables[0].Rows[0]["LoginID"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.DelearName = "";
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

        public static List<SelectListItem> FTitle()
        {
            List<SelectListItem> FTitle = new List<SelectListItem>();

            FTitle.Add(new SelectListItem { Text = "Select", Value = "0" });
            FTitle.Add(new SelectListItem { Text = "Mr.", Value = "Mr." });
            FTitle.Add(new SelectListItem { Text = "Ms.", Value = "Ms." });
            FTitle.Add(new SelectListItem { Text = "Mrs.", Value = "Mrs." });
            return FTitle;
        }

        public static List<SelectListItem> Registry()
        {
            List<SelectListItem> Registry = new List<SelectListItem>();
            //Registry.Add(new SelectListItem { Text = "Select", Value = "0" });
            Registry.Add(new SelectListItem { Text = "Pending", Value = "Pending" });
            Registry.Add(new SelectListItem { Text = "Done", Value = "Done" });
            return Registry;
        }

        public static List<SelectListItem> Agreement()
        {
            List<SelectListItem> Agreement = new List<SelectListItem>();
            //Agreement.Add(new SelectListItem { Text = "Select", Value = "0" });
            Agreement.Add(new SelectListItem { Text = "Pending", Value = "Pending" });
            Agreement.Add(new SelectListItem { Text = "Done", Value = "Done" });
            return Agreement;
        }

        public static List<SelectListItem> Category()
        {
            List<SelectListItem> Category = new List<SelectListItem>();
            Category.Add(new SelectListItem { Text = "Select Category", Value = "0"});
            Category.Add(new SelectListItem { Text = "General", Value = "General" });
            Category.Add(new SelectListItem { Text = "OBC", Value = "OBC" });
            Category.Add(new SelectListItem { Text = "SC", Value = "SC" });
            Category.Add(new SelectListItem { Text = "ST", Value = "ST" });
            return Category;
        }

        public static List<SelectListItem> LandType()
        {
            List<SelectListItem> LandType = new List<SelectListItem>();
            LandType.Add(new SelectListItem { Text = "Select LandType", Value = "0" });
            LandType.Add(new SelectListItem { Text = "SANKRAMANIY", Value = "SANKRAMANIY" });
            LandType.Add(new SelectListItem { Text = "AASANKRAMANIY", Value = "AASANKRAMANIY" });
            return LandType;
        }

        public static List<SelectListItem> KharijDakhil()
        {
            List<SelectListItem> KharijDakhil = new List<SelectListItem>();
            KharijDakhil.Add(new SelectListItem { Text = "Select KharijDakhil", Value = "0" });
            KharijDakhil.Add(new SelectListItem { Text = "COMPLETE", Value = "COMPLETE" });
            KharijDakhil.Add(new SelectListItem { Text = "INCOMPLETE", Value = "INCOMPLETE" });
            return KharijDakhil;
        }

        public static List<SelectListItem> LandDhara()
        {
            List<SelectListItem> LandDhara = new List<SelectListItem>();
            LandDhara.Add(new SelectListItem { Text = "Select", Value = "0" });
            LandDhara.Add(new SelectListItem { Text = "D80/1", Value = "D80/1" });
            LandDhara.Add(new SelectListItem { Text = "D80/2", Value = "D80/2" });
            LandDhara.Add(new SelectListItem { Text = "COMPLETE", Value = "COMPLETE" });
            LandDhara.Add(new SelectListItem { Text = "INCOMPLETE", Value = "INCOMPLETE" });
            return LandDhara;
        }

        public static List<SelectListItem> Notary()
        {
            List<SelectListItem> Notary = new List<SelectListItem>();
            //Notary.Add(new SelectListItem { Text = "Select", Value = "0" });
            Notary.Add(new SelectListItem { Text = "Pending", Value = "Pending" });
            Notary.Add(new SelectListItem { Text = "Done", Value = "Done" });
            return Notary;
        }



        public ActionResult active(string id, string IsActive)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Farmer obj = new Farmer();
                if (IsActive == "False")
                {
                    obj.IsActive = "1";
                }
                else
                {
                    obj.IsActive = "0";
                }
                obj.PK_Farmerid = Crypto.Decrypt(id);
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.FarmerStatus();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SucFarmerList"] = "Status Updated successfully";
                        FormName = "Farmerlist";
                        Controller = "Farmers";
                    }
                    else
                    {
                        TempData["ErrFarmerList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "Farmerlist";
                        Controller = "Farmers";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrFarmerList"] = ex.Message;
                FormName = "Farmerlist";
                Controller = "Farmers";
            }
            return RedirectToAction(FormName, Controller);
        }


        public ActionResult PaymetFarmers(string fid)
        {
            Farmer model = new Farmer();
            List<SelectListItem> CheckStatus = Common.CheckStatus();
            ViewBag.CheckStatus = CheckStatus;
            try
            {

                if (fid != null)
                {
                    List<Farmer> Clearedlst = new List<Farmer>();
                    List<Farmer> Pendinglst = new List<Farmer>();
                    List<Farmer> Bouncelst = new List<Farmer>();
                    model.PK_Farmerid = Crypto.Decrypt(fid);
                    //model.EncryptKey = Crypto.Encrypt(model.PK_Farmerid);
                    DataSet ds = model.GetPaymentDataByPkId();
                    if (ds.Tables.Count > 0 && ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            Farmer obj = new Farmer();
                            obj.Reciept = r["ReceiptNo"].ToString();
                            obj.ID = r["fk_Farmerid"].ToString();
                            obj.Fk_PaymentId = r["PID"].ToString();
                            obj.PaidAmount = r["PaidAmount"].ToString();
                            obj.TotalBalance = r["GeneratedAmount"].ToString();
                            obj.RemainingAmount = (r["RemainingAmount"].ToString());
                            obj.CashDate = r["PaidDate"].ToString();
                            obj.PayType = r["PayType"].ToString();
                            obj.ChequeNo = r["ChequeNo"].ToString();
                            obj.BankName = r["BankName"].ToString();
                            obj.Remarks = r["Remark"].ToString();
                            obj.CheqStatus = r["PaidStatus"].ToString();
                            obj.EncryptKey = Crypto.Encrypt(r["PId"].ToString());
                            Clearedlst.Add(obj);
                        }
                        model.ClearedListItem = Clearedlst;
                        ViewBag.TotalCleredPaid = double.Parse(ds.Tables[0].Compute("sum(PaidAmount)", "").ToString()).ToString("n2");
                        ViewBag.RemainingAmount = double.Parse(ds.Tables[0].Compute("sum(RemainingAmount)", "").ToString()).ToString("n2");
                    }
                    if (ds.Tables.Count > 0 && ds != null && ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow pr in ds.Tables[1].Rows)
                        {
                            Farmer obj = new Farmer();
                            obj.Reciept = pr["ReceiptNo"].ToString();
                            obj.ID = pr["fk_Farmerid"].ToString();
                            obj.PaidAmount = pr["PaidAmount"].ToString();
                            obj.Fk_PaymentId = pr["PID"].ToString();
                            obj.CashDate = pr["PaidDate"].ToString();
                            obj.PayType = pr["PayType"].ToString();
                            obj.ChequeNo = pr["ChequeNo"].ToString();
                            obj.BankName = pr["BankName"].ToString();
                            obj.Remarks = pr["Remark"].ToString();
                            obj.CheqStatus = pr["PaidStatus"].ToString();
                            obj.EncryptKey = Crypto.Encrypt(pr["PId"].ToString());
                            Pendinglst.Add(obj);
                        }
                        model.PendingListItem = Pendinglst;
                        ViewBag.TotalPendingPaid = double.Parse(ds.Tables[1].Compute("sum(PaidAmount)", "").ToString()).ToString("n2");
                    }
                    if (ds.Tables.Count > 0 && ds != null && ds.Tables[2].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[2].Rows)
                        {
                            Farmer obj = new Farmer();
                            obj.Reciept = r["ReceiptNo"].ToString();
                            obj.ID = r["fk_Farmerid"].ToString();
                            obj.PaidAmount = r["PaidAmount"].ToString();
                            obj.Fk_PaymentId = r["PID"].ToString();
                            obj.CashDate = r["PaidDate"].ToString();
                            obj.PayType = r["PayType"].ToString();
                            obj.ChequeNo = r["ChequeNo"].ToString();
                            obj.BankName = r["BankName"].ToString();
                            obj.Remarks = r["Remark"].ToString();
                            obj.CheqStatus = r["PaidStatus"].ToString();
                            obj.EncryptKey = Crypto.Encrypt(r["PId"].ToString());
                            Bouncelst.Add(obj);
                        }
                        model.BounceListItem = Bouncelst;
                        ViewBag.TotalBouncePaid = double.Parse(ds.Tables[2].Compute("sum(PaidAmount)", "").ToString()).ToString("n2");
                    }
                    if (ds.Tables.Count > 0 && ds != null && ds.Tables[3].Rows.Count > 0)
                    {
                        model.EncryptKey = Crypto.Encrypt(ds.Tables[3].Rows[0]["PK_FarmerId"].ToString());
                        model.Fk_UserId = ds.Tables[3].Rows[0]["PK_FarmerId"].ToString();
                        model.Name = ds.Tables[3].Rows[0]["Name"].ToString();
                        model.Mobile = ds.Tables[3].Rows[0]["Mobile"].ToString();
                        model.SQFT = ds.Tables[3].Rows[0]["SQFT"].ToString();
                        model.GataKhasaraN = ds.Tables[3].Rows[0]["GataKhasaraN"].ToString();
                        model.Registry = ds.Tables[3].Rows[0]["IsRegistry"].ToString();
                        model.Agreement = ds.Tables[3].Rows[0]["IsAgreement"].ToString();
                        model.Notary = ds.Tables[3].Rows[0]["IsNotary"].ToString();
                    }
                }
            }

            catch (Exception ex)
            {

                throw ex;
            }

            return View(model);
        }
        public ActionResult UpdatePayment(string PId, string FId)
        {
            Farmer model = new Farmer();

            try
            {
                if (PId != "")
                {
                    int count3 = 0;
                    List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
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
                    #region bank
                    int countbank = 0;
                    List<SelectListItem> ddlTransactionType = new List<SelectListItem>();
                    DataSet ddlTransaction = model.GetTransactionList();
                    if (ddlTransaction != null && ddlTransaction.Tables.Count > 0 && ddlTransaction.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow r in ddlTransaction.Tables[0].Rows)
                        {
                            if (countbank == 0)
                            {
                                ddlTransactionType.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
                                //ddlTransactionType.Add(new SelectListItem { Text = "Cash", Value = "1" });
                            }

                            ddlTransactionType.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });
                            countbank = countbank + 1;

                        }
                    }
                    ViewBag.ddlTransactionType = ddlTransactionType;
                    #endregion
                    model.Fk_PaymentId = Crypto.Decrypt(PId);
                    DataSet ds = model.GetFarmerPaymentByPaymentId();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                        model.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        // model.TotalBalance = ds.Tables[0].Rows[0]["TotalBalance"].ToString();
                        model.EncryptKey = Crypto.Encrypt(ds.Tables[0].Rows[0]["PaymentId"].ToString());
                        model.AssociateID = ds.Tables[0].Rows[0]["AssociatId"].ToString();
                        model.DelearName = ds.Tables[0].Rows[0]["DelearName"].ToString();
                        model.Hectare = ds.Tables[0].Rows[0]["Hectare"].ToString();
                        model.Acre = ds.Tables[0].Rows[0]["Acre"].ToString();
                        model.SQFT = ds.Tables[0].Rows[0]["SQFT"].ToString();
                        model.GataKhasaraN = ds.Tables[0].Rows[0]["GataKhasaraN"].ToString();
                        model.PaidAmount = ds.Tables[0].Rows[0]["PaidAmount"].ToString();
                        model.Paymentdate = ds.Tables[0].Rows[0]["PaidDate"].ToString();
                        model.Remarks = ds.Tables[0].Rows[0]["Remark"].ToString();
                        model.PayType = ds.Tables[0].Rows[0]["PayType"].ToString();
                        model.BankBranch = ds.Tables[0].Rows[0]["BankBranch"].ToString();
                        model.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                        model.TransactionDate = ds.Tables[0].Rows[0]["TransactionDate"].ToString();
                        model.Fk_BankId = ds.Tables[0].Rows[0]["Fk_BankId"].ToString();
                        model.TransactionNo = ds.Tables[0].Rows[0]["ChequeNo"].ToString();
                        model.PK_Farmerid = ds.Tables[0].Rows[0]["Pk_FarmerId"].ToString();
                        model.RemainingAmount = ds.Tables[0].Rows[0]["RemainingAmount"].ToString();
                        model.GeneratedAmount = ds.Tables[0].Rows[0]["GeneratedAmount"].ToString();

                    }
                }
                else
                {

                }
            }

            catch (Exception ex)
            {

                TempData["SucMsgFarmer"] = ex.Message;
            }
            return View(model);
        }
        [ActionName("UpdatePayment")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateFarmersPayment(Farmer model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.Paymentdate = string.IsNullOrEmpty(model.Paymentdate) ? null : Common.ConvertToSystemDate(model.Paymentdate, "dd/MM/yyyy");
                model.TransactionDate = string.IsNullOrEmpty(model.TransactionDate) ? null : Common.ConvertToSystemDate(model.TransactionDate, "dd/MM/yyyy");
                model.Fk_PaymentId = Crypto.Decrypt(model.EncryptKey);
                DataSet ds = model.UpdateFarmerPayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SucFarmerList"] = "Payment Updated successfully";

                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["SucFarmerList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["SucFarmerList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
                int count3 = 0;
                Farmer obj = new Farmer();
                List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
                DataSet dsPayMode = obj.GetPaymentModeList();
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
            }
            catch (Exception ex)
            {

                TempData["SucFarmerList"] = ex.Message;
            }
            var id = Crypto.Encrypt(model.PK_Farmerid);
            return RedirectToAction("PaymetFarmers", new { fid = id });
            //return RedirectToAction("AddPayFarmers", "Farmers");
        }
        public ActionResult AddPayFarmers(string id)
        {
            Farmer model = new Farmer();
            try
            {
                if (id != "")
                {
                    model.PK_Farmerid = Crypto.Decrypt(id);
                    DataSet ds = model.GetPaymentListByPkId();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                        model.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        model.TotalBalance = ds.Tables[0].Rows[0]["TotalBalance"].ToString();
                        model.EncryptKey = Crypto.Encrypt(ds.Tables[0].Rows[0]["PK_FarmerId"].ToString());
                        model.AssociateID = ds.Tables[0].Rows[0]["AssociatId"].ToString();
                        model.DelearName = ds.Tables[0].Rows[0]["DelearName"].ToString();
                        model.Hectare = ds.Tables[0].Rows[0]["Hectare"].ToString();
                        model.Acre = ds.Tables[0].Rows[0]["Acre"].ToString();
                        model.SQFT = ds.Tables[0].Rows[0]["SQFT"].ToString();
                        model.GataKhasaraN = ds.Tables[0].Rows[0]["GataKhasaraN"].ToString();
                        model.SqMtr = ds.Tables[0].Rows[0]["SqMtr"].ToString();
                    }
                }
                else
                {

                }
                int count3 = 0;
                List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
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
                #region bank
                int countbank = 0;
                List<SelectListItem> ddlTransactionType = new List<SelectListItem>();
                DataSet ddlTransaction = model.GetTransactionList();
                if (ddlTransaction != null && ddlTransaction.Tables.Count > 0 && ddlTransaction.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow r in ddlTransaction.Tables[0].Rows)
                    {
                        if (countbank == 0)
                        {
                            ddlTransactionType.Add(new SelectListItem { Text = "Select Bank", Value = "0" });
                            //ddlTransactionType.Add(new SelectListItem { Text = "Cash", Value = "1" });
                        }

                        ddlTransactionType.Add(new SelectListItem { Text = r["BankName"].ToString(), Value = r["Pk_BankId"].ToString() });
                        countbank = countbank + 1;

                    }
                }
                ViewBag.ddlTransactionType = ddlTransactionType;
                #endregion
            }

            catch (Exception ex)
            {

                TempData["SucMsgFarmer"] = ex.Message;
            }
            return View(model);
        }
        [ActionName("AddPayFarmers")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveFarmersPayment(Farmer model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.Paymentdate = string.IsNullOrEmpty(model.Paymentdate) ? null : Common.ConvertToSystemDate(model.Paymentdate, "dd/MM/yyyy");
                model.TransactionDate = string.IsNullOrEmpty(model.TransactionDate) ? null : Common.ConvertToSystemDate(model.TransactionDate, "dd/MM/yyyy");

                DataSet ds = model.SavePayMentFarmerDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SucFarmerList"] = "Payment Saved successfully";

                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["SucFarmerList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["SucMsgFarmer"] = "Please Fill All Values.";
                }
                int count3 = 0;
                Farmer obj = new Farmer();
                List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
                DataSet dsPayMode = obj.GetPaymentModeList();
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
            }
            catch (Exception ex)
            {

                TempData["SucFarmerList"] = ex.Message;
            }
            return RedirectToAction("PaymetFarmers", new { fid = model.EncryptKey });
            //return RedirectToAction("AddPayFarmers", "Farmers");
        }
        public ActionResult Farmerpaymentlist(Farmer model)
        {

            List<SelectListItem> CheckStatus = Common.CheckStatus();
            ViewBag.CheckStatus = CheckStatus;
            try
            {
                List<Farmer> lst = new List<Farmer>();
                //model.EncryptKey = Crypto.Encrypt(model.PK_Farmerid);
                model.CheqStatus = model.CheqStatus == "0" ? "Clearing" : model.CheqStatus;
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                DataSet ds = model.GetFarmerpaymentList();
                if (ds.Tables.Count > 0 && ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Farmer obj = new Farmer();
                        obj.Name = r["Name"].ToString();
                        obj.Reciept = r["ReceiptNo"].ToString();
                        obj.ID = r["PId"].ToString();
                        obj.PaidAmount = r["PaidAmount"].ToString();
                        obj.TotalBalance = r["GeneratedAmount"].ToString();
                        //obj.Amount= (r["TotalBalance"].ToString());
                        obj.CashDate = r["PaidDate"].ToString();
                        obj.PayType = r["PayType"].ToString();
                        obj.ChequeNo = r["ChequeNo"].ToString();
                        obj.BankName = r["BankName"].ToString();
                        obj.Remarks = r["Remark"].ToString();
                        obj.CheqStatus = r["PaidStatus"].ToString();
                        obj.EncryptKey = Crypto.Encrypt(r["PId"].ToString());
                        lst.Add(obj);
                    }
                    model.PaymetListFarm = lst;
                }

            }

            catch (Exception ex)
            {

                throw ex;
            }

            return View(model);
        }
        public ActionResult EditPayFarmers(Farmer model, string id, string uid)
        {
            int count3 = 0;
            Farmer obj = new Farmer();

            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            model.PK_Farmerid = Crypto.Decrypt(uid);
            DataSet dsPayMode = obj.GetPaymentModeList();
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
            if (id != null)
            {
                model.Reciept = id;
                model.PK_Farmerid = Crypto.Decrypt(uid);
                DataSet ds = obj.GetPaymentListByIdDeta(model.PK_Farmerid, model.Reciept);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    model.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    model.TotalBalance = ds.Tables[0].Rows[0]["TotalBalance"].ToString();
                    model.Amount = ds.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.CashDate = ds.Tables[0].Rows[0]["PaidDate"].ToString();
                    model.Remarks = ds.Tables[0].Rows[0]["Remark"].ToString();
                    model.ChequeNo = ds.Tables[0].Rows[0]["ChequeNo"].ToString();
                    model.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            return View(model);
        }

        //[ActionName("EditPayFarmers")]
        //[OnAction(ButtonName = "btnUpdate")]
        //public ActionResult EditPayFarmerS(Farmer model, string id, string uid)
        //{
        //    try
        //    {
        //        model.AddedBy = Session["Pk_AdminId"].ToString();
        //        if (Convert.ToInt32(model.PayType) != 0 && model.Mobile != null && model.TotalBalance != null && model.Amount != null && Convert.ToDecimal(model.Amount) > 0 && model.CashDate != null)
        //        {
        //            DataSet ds = model.UpdatePayMentFarmerDetails();
        //            if (ds != null && ds.Tables.Count > 0)
        //            {
        //                if (ds.Tables[0].Rows[0][0].ToString() == "1")
        //                {

        //                    TempData["SucMsgFarmer"] = "Payment Updated successfully";

        //                }
        //                else if (ds.Tables[0].Rows[0][0].ToString() == "0")
        //                {
        //                    TempData["ErrMsgFarmer"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //                }
        //                model.PK_Farmerid = string.Empty;
        //            }
        //        }
        //        else
        //        {
        //            TempData["ErrMsgFarmer"] = "Please fill all value.";
        //        }

        //        int count3 = 0;
        //        Farmer obj = new Farmer();
        //        List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
        //        DataSet dsPayMode = obj.GetPaymentModeList();
        //        if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow r in dsPayMode.Tables[0].Rows)
        //            {
        //                if (count3 == 0)
        //                {
        //                    ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Type", Value = "0" });
        //                }
        //                ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
        //                count3 = count3 + 1;
        //            }
        //        }
        //        ViewBag.ddlPaymentMode = ddlPaymentMode;
        //    }
        //    catch (Exception ex)
        //    {

        //        TempData["MsgFarmer"] = ex.Message;
        //    }

        //    return View(model);
        //}

        public ActionResult DeletePayment(string id, string UserId)
        {
            string FormName = "";
            string Controller = "";
            Farmer obj = new Farmer();
            try
            {
                obj.ID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeletePayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SucFarmerList"] = "Payment details deleted successfully";
                        FormName = "PaymetFarmers";
                        Controller = "Farmers";
                    }
                    else
                    {
                        TempData["SucFarmerList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "PaymetFarmers";
                        Controller = "Farmers";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SucFarmerList"] = ex.Message;
                FormName = "PaymetFarmers";
                Controller = "Farmers";
            }
            //return RedirectToAction(FormName); 
            return RedirectToAction(FormName, new { fid = UserId });
            // return RedirectToAction((FormName+"?id="+UserId).Trim(),Controller);
        }

        public ActionResult UpdateCheckStaus(string CheqStatus, string ReferencdId, string FarmerId, string CheckDate)
        {
            string FormName = "";
            string Controller = "";
            Farmer obj = new Farmer();
            try
            {
                obj.ID = ReferencdId;
                obj.Fk_UserId = FarmerId;
                obj.CheqStatus = CheqStatus;
                obj.CashDate = string.IsNullOrEmpty(CheckDate) ? null : Common.ConvertToSystemDate(CheckDate, "dd/MM/yyyy"); ;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateCheckStatus();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.Result = "Yes";
                        FormName = "PaymetFarmers";
                        Controller = "Farmers";
                    }
                    else
                    {
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "PaymetFarmers";
                        Controller = "Farmers";
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
                FormName = "PaymetFarmers";
                Controller = "Farmers";
            }
            //return RedirectToAction(FormName); 
            var uid = Crypto.Encrypt(FarmerId);
            return RedirectToAction(FormName, new { fid = uid });
            // return RedirectToAction((FormName+"?id="+UserId).Trim(),Controller);
        }
        public ActionResult FarmerPlotRegistry(Farmer model)
        {
            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    //if (count == 0)
                    //{
                    ddlBranch.Add(new SelectListItem { Text = "Lucknow", Value = "1" });
                    //}
                    //ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion
            #region ddlSite
            int count1 = 0;
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
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            List<SelectListItem> ddlRegistry = new List<SelectListItem>();
            ddlRegistry.Add(new SelectListItem { Text = "Select Registry", Value = "0" });
            ddlRegistry.Add(new SelectListItem { Text = "Company", Value = "1" });
            ddlRegistry.Add(new SelectListItem { Text = "Farmer", Value = "2" });
            ViewBag.ddlRegistry = ddlRegistry;
            #region ddlfarmer
            int countfarmer = 0;
            List<SelectListItem> ddlfarmer = new List<SelectListItem>();
            DataSet dsFamer = model.FarmerListForPlotRegistry();
            if (dsFamer != null && dsFamer.Tables.Count > 0 && dsFamer.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsFamer.Tables[0].Rows)
                {
                    if (countfarmer == 0)
                    {
                        ddlfarmer.Add(new SelectListItem { Text = "Select Farmer", Value = "0" });
                    }
                    ddlfarmer.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_FarmerId"].ToString() });
                    countfarmer = countfarmer + 1;

                }
            }
            ViewBag.ddlfarmer = ddlfarmer;
            #endregion
            return View(model);
        }
        public ActionResult checkplotnumberforfarmerplotregistry(string SiteID, string SectorID, string BlockID, string PlotNumber)
        {
            Farmer obj = new Farmer();
            obj.SiteID = SiteID;
            obj.SectorID = SectorID;
            obj.BlockID = BlockID;
            obj.PlotNumber = PlotNumber;
            DataSet ds = obj.CheckPlotNumberForPlotRegistry();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    obj.PlotSize = ds.Tables[0].Rows[0]["PlotSize"].ToString();
                    obj.PlotArea = ds.Tables[0].Rows[0]["PlotArea"].ToString();
                    obj.PlotID = ds.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    obj.CustomerName = ds.Tables[0].Rows[0]["Name"].ToString();
                    obj.CustomerId = ds.Tables[0].Rows[0]["PK_UserId"].ToString();
                    obj.Result = "1";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
                else
                {
                    obj.Result = "Invalid Plot Number";
                }
            }
            else
            {
                obj.Result = "Invalid Plot Number";

            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FarmerDetailsById(string FarmerId)
        {
            Farmer obj = new Farmer();
            obj.PK_Farmerid = FarmerId;
            DataSet ds = obj.FarmerListById();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    obj.PK_Farmerid = ds.Tables[0].Rows[0]["PK_FarmerId"].ToString();
                    obj.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    obj.GataKhasaraN = ds.Tables[0].Rows[0]["GataKhasaraN"].ToString();
                    obj.SQFT = ds.Tables[0].Rows[0]["SQFT"].ToString();
                    obj.Result = "yes";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
                else
                {
                    obj.Result = "0";
                }
            }
            else
            {
                obj.Result = "0";

            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [ActionName("FarmerPlotRegistry")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveFarmerPlotRegistry(Farmer model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.RegistryDate = string.IsNullOrEmpty(model.RegistryDate) ? null : Common.ConvertToSystemDate(model.RegistryDate, "dd/MM/yyyy");
                if(model.IsRegistryByCompany == null)
                {
                    model.IsRegistryByCompany = "0";
                }
                else
                {
                    model.IsRegistryByCompany = "1";
                }
                DataSet ds = model.SaveFarmerPlotRegistry();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plotregistry"] = "Plot Registry successfully";
                        TempData["Booking"] = "Reciept Number : " + ds.Tables[0].Rows[0]["RecieptNumber"].ToString();
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Plotregistry"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Plotregistry"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {

                TempData["Plotregistry"] = ex;
            }

            return RedirectToAction("FarmerPlotRegistry", "Farmers");
        }
        public ActionResult FarmerPlotRegistrylist(Farmer model)
        {
            List<Farmer> lst = new List<Farmer>();
            DataSet ds = model.GetPlotRegistrylist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Farmer obj = new Farmer();

                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.PlotNumber = r["PlotDetails"].ToString();
                    obj.GataKhasaraN = r["GataKhasraNo"].ToString();
                    obj.FarmerName = r["FarmerName"].ToString();
                    obj.FStatus = r["Status"].ToString();
                    obj.TotalBalance = r["TotalArea"].ToString();
                    obj.PlotSize = r["PlotSize"].ToString();
                    obj.IsActive = r["IsActive"].ToString();
                    obj.RemainingArea = r["RemainingArea"].ToString();
                    obj.RegistryDate = r["RegistryDate"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_PlotRegistryId"].ToString());
                    lst.Add(obj);
                }
                model.FarmerPlotList = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("FarmerPlotRegistrylist")]
        [OnAction(ButtonName = "Search")]
        public ActionResult PlotRegistryList(Farmer model)
        {

            List<Farmer> lst = new List<Farmer>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetPlotRegistrylist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Farmer obj = new Farmer();

                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.PlotNumber = r["PlotDetails"].ToString();
                    obj.GataKhasaraN = r["GataKhasraNo"].ToString();
                    obj.FarmerName = r["FarmerName"].ToString();
                    obj.FStatus = r["Status"].ToString();
                    obj.TotalBalance = r["TotalArea"].ToString();
                    obj.PlotSize = r["PlotSize"].ToString();
                    obj.IsActive = r["IsActive"].ToString();
                    obj.RemainingArea = r["RemainingArea"].ToString();
                    obj.RegistryDate = r["RegistryDate"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_PlotRegistryId"].ToString());
                    lst.Add(obj);
                }
                model.FarmerPlotList = lst;
            }

            return View(model);
        }
        public ActionResult Plotactive(string id, string IsActive)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Farmer obj = new Farmer();
                if (IsActive == "False")
                {
                    obj.IsActive = "1";
                }
                else
                {
                    obj.IsActive = "0";
                }
                obj.PK_Farmerid = Crypto.Decrypt(id);
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.RegistryStatus();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["plotregistrylist"] = "Status Updated successfully";
                        FormName = "FarmerPlotRegistrylist";
                        Controller = "Farmers";
                    }
                    else
                    {
                        TempData["plotregistrylist"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "FarmerPlotRegistrylist";
                        Controller = "Farmers";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrFarmerList"] = ex.Message;
                FormName = "FarmerPlotRegistrylist";
                Controller = "Farmers";
            }
            return RedirectToAction(FormName, Controller);
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
    }
}