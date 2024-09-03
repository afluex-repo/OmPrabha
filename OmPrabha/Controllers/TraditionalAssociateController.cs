using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OmPrabha.Models;
using System.Data;
using OmPrabha.Filter;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace OmPrabha.Controllers
{
    public class TraditionalAssociateController : AdminBaseController
    {

        #region AffiliateRegistration
        public ActionResult AssociateRegistration(string UserID)
        {
            TraditionalAssociate model = new TraditionalAssociate();
            try
            {
                if (UserID != null)
                {

                    model.UserID = Crypto.Decrypt(UserID);
                    //model.UserID = UserID;
                    DataSet dsPlotDetails = model.GetList();
                    if (dsPlotDetails != null && dsPlotDetails.Tables.Count > 0)
                    {
                        model.UserID = UserID;
                        model.SponsorID = dsPlotDetails.Tables[0].Rows[0]["SponsorId"].ToString();
                        model.LoginID = dsPlotDetails.Tables[0].Rows[0]["AssociateId"].ToString();
                        model.SponsorName = dsPlotDetails.Tables[0].Rows[0]["SponsorName"].ToString();
                        model.FirstName = dsPlotDetails.Tables[0].Rows[0]["FirstName"].ToString();
                        model.LastName = dsPlotDetails.Tables[0].Rows[0]["LastName"].ToString();
                        model.DesignationID = dsPlotDetails.Tables[0].Rows[0]["FK_DesignationID"].ToString();
                        model.DesignationName = dsPlotDetails.Tables[0].Rows[0]["DesignationName"].ToString();
                        model.BranchID = dsPlotDetails.Tables[0].Rows[0]["Fk_BranchId"].ToString();
                        model.Contact = dsPlotDetails.Tables[0].Rows[0]["Mobile"].ToString();
                        model.Email = dsPlotDetails.Tables[0].Rows[0]["Email"].ToString();
                        model.State = dsPlotDetails.Tables[0].Rows[0]["State"].ToString();
                        model.City = dsPlotDetails.Tables[0].Rows[0]["City"].ToString();
                        model.Address = dsPlotDetails.Tables[0].Rows[0]["Address"].ToString();
                        model.Pincode = dsPlotDetails.Tables[0].Rows[0]["PinCode"].ToString();
                        model.PanNo = dsPlotDetails.Tables[0].Rows[0]["PanNumber"].ToString();
                        model.BranchName = dsPlotDetails.Tables[0].Rows[0]["BranchName"].ToString();
                        //   ViewBag.Disabled = "disabled";
                        model.AdharNumber = dsPlotDetails.Tables[0].Rows[0]["AdharNumber"].ToString();
                        model.BankAccountNo = dsPlotDetails.Tables[0].Rows[0]["MemberAccNo"].ToString();
                        model.BankName = dsPlotDetails.Tables[0].Rows[0]["MemberBankName"].ToString();
                        model.BankBranch = dsPlotDetails.Tables[0].Rows[0]["MemberBranch"].ToString();
                        model.IFSCCode = dsPlotDetails.Tables[0].Rows[0]["IFSCCode"].ToString();
                        model.ProfilePic = dsPlotDetails.Tables[0].Rows[0]["ProfilePic"].ToString();
                        model.Signature = dsPlotDetails.Tables[0].Rows[0]["Signature"].ToString();
                    }
                }
                else
                {

                }

                #region ddlBranch
                TraditionalAssociate obj = new TraditionalAssociate();
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
            return View(model);

        }

        public ActionResult GetSponsorName(string SponsorID)
        {
            try
            {
                TraditionalAssociate model = new TraditionalAssociate();
                model.LoginID = SponsorID;

                #region GetSiteRate
                DataSet dssponsor = model.GetAssociateList();
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

        [HttpPost]
        [ActionName("AssociateRegistration")]
        [OnAction(ButtonName = "btnRegistration")]
        public ActionResult AssociateRegistration(HttpPostedFileBase postedFile, HttpPostedFileBase postedFile1, TraditionalAssociate model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Random rnd = new Random();
                int ctrPasword = rnd.Next(111111, 999999);
                model.Password = Crypto.Encrypt(ctrPasword.ToString());
                model.AddedBy = Session["Pk_AdminId"].ToString();
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
            FormName = "ConfirmRegistration";
            Controller = "TraditionalAssociate";

            return RedirectToAction(FormName, Controller);
        }



        public ActionResult UpdateAssociate(string UserID)
        {
            TraditionalAssociate model = new TraditionalAssociate();
            try
            {
                if (UserID != null)
                {
                  
                    model.UserID = Crypto.Decrypt(UserID);
                    //model.UserID = UserID;
                    DataSet dsPlotDetails = model.GetList();
                    if (dsPlotDetails != null && dsPlotDetails.Tables.Count > 0)
                    {
                        model.UserID = UserID;
                        model.SponsorID = dsPlotDetails.Tables[0].Rows[0]["SponsorId"].ToString();
                        model.LoginID = dsPlotDetails.Tables[0].Rows[0]["AssociateId"].ToString();
                        model.SponsorName = dsPlotDetails.Tables[0].Rows[0]["SponsorName"].ToString();
                        model.FirstName = dsPlotDetails.Tables[0].Rows[0]["FirstName"].ToString();
                        model.LastName = dsPlotDetails.Tables[0].Rows[0]["LastName"].ToString();
                        model.DesignationID = dsPlotDetails.Tables[0].Rows[0]["FK_DesignationID"].ToString();
                        model.DesignationName = dsPlotDetails.Tables[0].Rows[0]["DesignationName"].ToString();
                        model.BranchID = dsPlotDetails.Tables[0].Rows[0]["Fk_BranchId"].ToString();
                        model.Contact = dsPlotDetails.Tables[0].Rows[0]["Mobile"].ToString();
                        model.Email = dsPlotDetails.Tables[0].Rows[0]["Email"].ToString();
                        model.State = dsPlotDetails.Tables[0].Rows[0]["State"].ToString();
                        model.City = dsPlotDetails.Tables[0].Rows[0]["City"].ToString();
                        model.Address = dsPlotDetails.Tables[0].Rows[0]["Address"].ToString();
                        model.Pincode = dsPlotDetails.Tables[0].Rows[0]["PinCode"].ToString();
                        model.PanNo = dsPlotDetails.Tables[0].Rows[0]["PanNumber"].ToString();
                        model.BranchName = dsPlotDetails.Tables[0].Rows[0]["BranchName"].ToString();
                        //   ViewBag.Disabled = "disabled";
                        model.AdharNumber = dsPlotDetails.Tables[0].Rows[0]["AdharNumber"].ToString();
                        model.BankAccountNo = dsPlotDetails.Tables[0].Rows[0]["MemberAccNo"].ToString();
                        model.BankName = dsPlotDetails.Tables[0].Rows[0]["MemberBankName"].ToString();
                        model.BankBranch = dsPlotDetails.Tables[0].Rows[0]["MemberBranch"].ToString();
                        model.IFSCCode = dsPlotDetails.Tables[0].Rows[0]["IFSCCode"].ToString();
                        model.ProfilePic = dsPlotDetails.Tables[0].Rows[0]["ProfilePic"].ToString();
                        model.Signature = dsPlotDetails.Tables[0].Rows[0]["Signature"].ToString();
                        model.Percentage = dsPlotDetails.Tables[0].Rows[0]["Percentage"].ToString();

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
                                    ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                                }
                                ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                                desgnationCount = desgnationCount + 1;
                            }
                        }
                        ViewBag.ddlDesignation = ddlDesignation;

                        #endregion

                    }
                }
                else
                {

                }

                #region ddlBranch
                TraditionalAssociate obj = new TraditionalAssociate();
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

                //#region ddlDesignation

                //int desgnationCount = 0;
                //List<SelectListItem> ddlDesignation = new List<SelectListItem>();
                //DataSet dsdesignation = obj.GetDesignationList();
                //if (dsdesignation != null && dsdesignation.Tables.Count > 0 && dsdesignation.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow r in dsdesignation.Tables[0].Rows)
                //    {
                //        if (desgnationCount == 0)
                //        {
                //            ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                //        }
                //        ddlDesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                //        desgnationCount = desgnationCount + 1;
                //    }
                //}
                //ViewBag.ddlDesignation = ddlDesignation;

                //#endregion
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(model);

        }
        [HttpPost]
        [ActionName("UpdateAssociate")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateAssociateRegistration(HttpPostedFileBase postedFile, HttpPostedFileBase postedFile1, TraditionalAssociate model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                model.UserID = Crypto.Decrypt(model.UserID);
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
                DataSet dsRegistration = model.UpdateAssociate();
                if (dsRegistration.Tables[0].Rows[0][0].ToString() == "1")
                    if (dsRegistration.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Registration"] = "Updated Successfull !";
                    }
                    else
                    {
                        TempData["Registration"] = dsRegistration.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
            }
            catch (Exception ex)
            {
                TempData["Registration"] = ex.Message;
            }
            FormName = "AssociateRegistration";
            Controller = "TraditionalAssociate";

            return RedirectToAction(FormName, Controller);
        }
        public ActionResult ConfirmRegistration()
        {
            return View();
        }

        #endregion

        #region Associate List
        public ActionResult AssociateList(TraditionalAssociate model)
        {
            //model.JoiningFromDate = Common.ConvertToSystemDate(DateTime.Today.ToShortDateString(), "MM/dd/yyyy"); 
            //model.JoiningToDate = Common.ConvertToSystemDate(DateTime.Today.ToShortDateString(), "MM/dd/yyyy");
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
            List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            ViewBag.ddlStatus = AssociateStatus;
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.ddlleg = Leg;
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
                        ddlDesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
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
        [ActionName("AssociateList")]
        [OnAction(ButtonName = "btnSearchCustomer")]
        public ActionResult AssociateListbyId(TraditionalAssociate model)
        {
            List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            ViewBag.ddlStatus = AssociateStatus;
            List<SelectListItem> ddldesignation = new List<SelectListItem>();
            DataSet dsdesignation = model.GetDesignationList();
            int designationcount = 0;
            if (dsdesignation !=null && dsdesignation.Tables[0].Rows.Count>0 && dsdesignation.Tables.Count>0)
            {
                foreach (DataRow r  in dsdesignation.Tables[0].Rows)
                {
                    if (designationcount==0)
                    {
                        ddldesignation.Add(new SelectListItem { Text = "Select Designation", Value = "0" });
                    }
                    ddldesignation.Add(new SelectListItem { Text = r["DesignationName"].ToString(), Value = r["PK_DesignationID"].ToString() });
                    designationcount = designationcount + 1;
                }
            }
            ViewBag.ddlDesignation = ddldesignation;
            List<TraditionalAssociate> lst = new List<TraditionalAssociate>();
            model.JoiningFromDate = string.IsNullOrEmpty(model.JoiningFromDate) ? null : Common.ConvertToSystemDate(model.JoiningFromDate, "dd/MM/yyyy");
            model.JoiningToDate = string.IsNullOrEmpty(model.JoiningToDate) ? null : Common.ConvertToSystemDate(model.JoiningToDate, "dd/MM/yyyy");
            model.FK_DesignationId = model.FK_DesignationId=="0" ?  null :model.FK_DesignationId;
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
                    //obj.LoginID = r["LoginId"].ToString();
                    //obj.DesignationID = r["FK_DesignationID"].ToString();
                    obj.Status = r["Status"].ToString();
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
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    lst.Add(obj);
                }
                model.lstTrad = lst;
            }
            return View(model);
        }
        #endregion
        #region AssociateUplineDetail

        public ActionResult AdvisorUplineDetail(TraditionalAssociate model)
        {

            return View(model);
        }

        [HttpPost]
        [ActionName("AdvisorUplineDetail")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetList(TraditionalAssociate model)
        {
            List<TraditionalAssociate> lst = new List<TraditionalAssociate>();

            DataSet ds = model.GetDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    TraditionalAssociate obj = new TraditionalAssociate();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.Percentage = r["Percentage"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.Contact = r["Mobile"].ToString();

                    lst.Add(obj);
                }
                model.lstTrad = lst;
            }

            return View(model);
        }

        #endregion
        #region AssociateDownlineDetail


        public ActionResult AdvisorDownlineDetail(TraditionalAssociate model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("AdvisorDownlineDetail")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetDownlineList(TraditionalAssociate model)
        {
            List<TraditionalAssociate> lst = new List<TraditionalAssociate>();

            DataSet ds = model.GetDownlineDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    TraditionalAssociate obj = new TraditionalAssociate();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.Percentage = r["Percentage"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    lst.Add(obj);
                }
                model.lstTrad = lst;
            }

            return View(model);
        }

        #endregion
        public ActionResult LevelTree()
        {
            return View();
        }
        public ActionResult UpdateRank(string UserID)
        {
            TraditionalAssociate model = new TraditionalAssociate();

                model.UserID = Crypto.Decrypt(UserID);
                DataSet ds = model.GetRankList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.UserID = UserID;
                    model.FirstName = ds.Tables[0].Rows[0]["Name"].ToString();
                    model.PK_DesignationID = ds.Tables[0].Rows[0]["FK_DesignationID"].ToString();
                    model.OldDesignation = ds.Tables[0].Rows[0]["DesignationName"].ToString();
            }

            #region ddlDesignation

            int desgnationCount = 0;
            List<SelectListItem> ddlDesignation = new List<SelectListItem>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[1].Rows)
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
            return View(model);
        }
        [HttpPost]
        [ActionName("UpdateRank")]
        [OnAction(ButtonName = "update")]
        public ActionResult UpdateRankAction(TraditionalAssociate model, string UserID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                model.UserID = Crypto.Decrypt(model.UserID);
                DataSet ds = model.UpdateAssociateRank();
                if (ds!= null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Registration"] = "Rank Updated Successfull !!";
                    }
                    else
                    {
                        TempData["Registration"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
            }
            catch (Exception ex)
            {
                TempData["Registration"] = ex.Message;
            }
            FormName = "AssociateList";
            Controller = "TraditionalAssociate";

            return RedirectToAction(FormName, Controller);
        }
    }
}