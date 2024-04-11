using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OmPrabha.Models;
using System.Data;
using OmPrabha.Filter;
using Newtonsoft.Json;

namespace OmPrabha.Controllers
{
    public class MasterController : AdminBaseController
    {
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

        #region News Master 

        public ActionResult NewsMaster(string NewsID)
        {
            Master model = new Master();
            if (NewsID != null)
            {
                model.NewsID = NewsID;

                DataSet Branch = model.GetNews();
                if (Branch != null && Branch.Tables.Count > 0)
                {
                    model.NewsID = Branch.Tables[0].Rows[0]["PK_NewsID"].ToString();
                    model.NewsHeading = Branch.Tables[0].Rows[0]["NewsHeading"].ToString();
                    model.NewsBody = Branch.Tables[0].Rows[0]["NewsBody"].ToString();
                    model.NewsFor = Branch.Tables[0].Rows[0]["NewsFor"].ToString();
                }
            }

            List<SelectListItem> NewsFor = Common.NewsForList();

            ViewBag.NewsFor = NewsFor;
            // NewsFor.Add();
            return View(model);


        }

        public ActionResult NewsList(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.GetNews();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.NewsID = r["PK_NewsID"].ToString();
                    obj.NewsHeading = r["NewsHeading"].ToString();
                    obj.NewsBody = r["NewsBody"].ToString();
                    obj.NewsFor = r["NewsFor"].ToString();
                    lst.Add(obj);
                }
                model.lstBlock1 = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("NewsMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveNews(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveNews();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["News"] = "News saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["News"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["News"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["News"] = ex.Message;
            }
            return RedirectToAction("NewsMaster", "Master");
        }

        [HttpPost]
        [ActionName("NewsMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateNews(Master obj)
        {
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateNews();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["News"] = "News updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["News"] = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                else
                {
                    TempData["News"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["News"] = ex.Message;
            }
            return RedirectToAction("NewsMaster", "Master");
        }
        public ActionResult DeleteNews(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.NewsID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteNews();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["News"] = "News deleted successfully";
                        FormName = "NewsList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["News"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "NewsList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["News"] = ex.Message;
                FormName = "NewsList";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion

        #region Branch Master

        public ActionResult BranchMaster(string BranchID)
        {
            Master model = new Master();
            if (BranchID != null)
            {
                model.BranchID = Crypto.Decrypt(BranchID);

                DataSet Branch = model.GetBranchList();
                if (Branch != null && Branch.Tables.Count > 0)
                {
                    model.BranchID = Branch.Tables[0].Rows[0]["PK_BranchID"].ToString();
                    model.BranchName = Branch.Tables[0].Rows[0]["BranchName"].ToString();

                }
            }

            return View(model);

        }
        public ActionResult BranchList(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.GetBranchList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.BranchID = r["PK_BranchID"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_BranchID"].ToString());

                    lst.Add(obj);
                }
                model.lstBlock1 = lst;
            }
            return View(model);
        }


        [HttpPost]
        [ActionName("BranchMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveBranch(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveBranch();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Branch"] = "Branch saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Branch"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Branch"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Branch"] = ex.Message;
            }
            return RedirectToAction("BranchMaster", "Master");
        }

        [HttpPost]
        [ActionName("BranchMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateBranch(Master obj)
        {
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateBranch();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Branch"] = "Branch updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Branch"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Branch"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Branch"] = ex.Message;
            }
            return RedirectToAction("BranchMaster", "Master");
        }
        public ActionResult DeleteBranch(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.BranchID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteBranch();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Branch"] = "Branch deleted successfully";
                        FormName = "BranchList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["Branch"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "BranchList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Branch"] = ex.Message;
                FormName = "BranchList";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion

        #region RankMaster

        public ActionResult RankMaster(string DesignationID)
        {
            Master model = new Master();
            if (DesignationID != null)
            {
                model.DesignationID = Crypto.Decrypt(DesignationID);
                DataSet Branch = model.GetDesignation();
                if (Branch != null && Branch.Tables.Count > 0)
                {
                    model.DesignationID = Branch.Tables[0].Rows[0]["PK_DesignationID"].ToString();
                    model.DesignationName = Branch.Tables[0].Rows[0]["DesignationName"].ToString();
                    model.Percentage = Branch.Tables[0].Rows[0]["Percentage"].ToString();
                }
            }

            return View(model);

        }

        public ActionResult RankList(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.GetDesignation();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.DesignationID = r["PK_DesignationID"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.Percentage = r["Percentage"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_DesignationID"].ToString());

                    lst.Add(obj);
                }
                model.lstBlock1 = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("RankMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveRank(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveDesignation();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Rank"] = "Rank saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Rank"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Rank"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Rank"] = ex.Message;
            }
            return RedirectToAction("RankMaster", "Master");
        }

        [HttpPost]
        [ActionName("RankMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateRank(Master obj)
        {
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateRank();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Rank"] = "Rank updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Rank"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Rank"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Rank"] = ex.Message;
            }
            return RedirectToAction("RankMaster", "Master");
        }

        public ActionResult DeleteRank(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.DesignationID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteRank();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Rank"] = "Rank deleted successfully";
                        FormName = "RankList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["Rank"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "RankList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Rank"] = ex.Message;
                FormName = "RankList";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region PLCMaster
        public ActionResult PLCMaster(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.PLCList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PLCID = r["PK_PLCID"].ToString();
                    obj.PLCName = r["PLCName"].ToString();

                    lst.Add(obj);
                }
                model.lstPLC = lst;


            }
            return View(model);
        }

        [HttpPost]
        [ActionName("PLCMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SavePLC(Master obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.SavePLC();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["PLC"] = "PLC saved successfully";
                        FormName = "PLCMaster";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["PLC"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "PLCMaster";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["PLC"] = ex.Message;
                FormName = "PLCMaster";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult DeletePLC(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.PLCID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeletePLC();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["PLC"] = "PLC deleted successfully";
                        FormName = "PLCMaster";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["PLC"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "PLCMaster";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["PLC"] = ex.Message;
                FormName = "PLCMaster";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult UpdatePLC(string PLCID, string PLCName)
        {
            Master obj = new Master();
            try
            {
                obj.PLCID = PLCID;
                obj.PLCName = PLCName;
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.UpdatePLC();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.Result = "PLC updated successfully";
                    }
                    else
                    {
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region SiteMaster

        public ActionResult SiteMaster(string SiteID)
        {
            Master model = new Master();
            #region ddlUnits
            Master obj = new Master();
            int count = 0;
            List<SelectListItem> ddlUnit = new List<SelectListItem>();
            DataSet ds1 = obj.GetUnitList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlUnit.Add(new SelectListItem { Text = "Select Unit", Value = "0" });
                    }
                    ddlUnit.Add(new SelectListItem { Text = r["UnitName"].ToString(), Value = r["PK_UnitID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlUnit = ddlUnit;

            #endregion

            #region ddlSiteType
            Master objSiteType = new Master();
            int count1 = 0;
            List<SelectListItem> ddlSiteType = new List<SelectListItem>();
            DataSet ds2 = objSiteType.GetSiteTypeList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSiteType.Add(new SelectListItem { Text = "Select Site Type", Value = "0" });
                    }
                    ddlSiteType.Add(new SelectListItem { Text = r["SiteTypeName"].ToString(), Value = r["PK_SiteTypeID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlSiteType = ddlSiteType;

            #endregion

            if (SiteID != null)
            {
                model.SiteID = Crypto.Decrypt(SiteID);
                List<Master> lst = new List<Master>();
                DataSet dsSite = model.GetSiteList();
                if (dsSite != null && dsSite.Tables.Count > 0)
                {
                    model.UnitID = dsSite.Tables[0].Rows[0]["FK_UnitID"].ToString();
                    model.SiteName = dsSite.Tables[0].Rows[0]["SiteName"].ToString();
                    model.Location = dsSite.Tables[0].Rows[0]["Location"].ToString();
                    model.Rate = dsSite.Tables[0].Rows[0]["Rate"].ToString();
                    model.SiteTypeID = dsSite.Tables[0].Rows[0]["FK_SiteTypeID"].ToString();
                    model.DevelopmentCharge = dsSite.Tables[0].Rows[0]["DevelopmentCharge"].ToString();
                }


                DataSet ds = model.GetSitePlcChargeList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Master objPLC = new Master();
                        objPLC.PLCID = r["PK_PLCID"].ToString();
                        objPLC.PLCName = r["PLCName"].ToString();
                        objPLC.PLCCharge = r["PLCCharge"].ToString();
                        lst.Add(objPLC);
                    }
                    model.lstPLC = lst;
                }
            }
            else
            {
                List<Master> lst = new List<Master>();
                DataSet ds = model.PLCList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Master objPLC = new Master();
                        objPLC.PLCID = r["PK_PLCID"].ToString();
                        objPLC.PLCName = r["PLCName"].ToString();

                        lst.Add(objPLC);
                    }
                    model.lstPLC = lst;
                }

            }

            return View(model);
        }

        [HttpPost]
        [ActionName("SiteMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveSite(Master obj)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("PK_PLCID", typeof(string));
                dt.Columns.Add("Charge", typeof(string));
                string hdrows = "";
                if (Request["hdRows"] != null)
                {
                    hdrows = Request["hdRows"].ToString();
                }
                else
                {
                    hdrows = "0";
                }
                for (int i = 1; i < int.Parse(hdrows); i++)
                {
                    string plcid = Request["hdPLCID_ " + i].ToString();
                    string charge = Request["txtCharge_ " + i].ToString();

                    DataRow dr = dt.NewRow();
                    dr = dt.NewRow();

                    dr["PK_PLCID"] = plcid;
                    dr["Charge"] = string.IsNullOrEmpty(charge) ? "0" : charge;
                    dt.Rows.Add(dr);
                }

                obj.dtPLCCharge = dt;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveSite();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SiteMaster"] = "Site saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["SiteMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["SiteMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["SiteMaster"] = ex.Message;
            }
            return RedirectToAction("SiteMaster", "Master");
        }

        [HttpPost]
        [ActionName("SiteMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateSite(Master obj)
        {
            try
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("PK_PLCID", typeof(string));
                dt.Columns.Add("Charge", typeof(string));

                string hdrows = "";
                if (Request["hdRows"] != null)
                {
                    hdrows = Request["hdRows"].ToString();
                }
                else
                {
                    hdrows = "0";
                }
                for (int i = 1; i < int.Parse(hdrows); i++)
                {
                    string plcid = Request["hdPLCID_ " + i].ToString();
                    string charge = Request["txtCharge_ " + i].ToString();

                    DataRow dr = dt.NewRow();
                    dr = dt.NewRow();

                    dr["PK_PLCID"] = plcid;
                    dr["Charge"] = string.IsNullOrEmpty(charge) ? "0" : charge;
                    dt.Rows.Add(dr);
                }

                obj.dtPLCCharge = dt;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateSite();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SiteMaster"] = "Site Updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["SiteMaster"] = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                else
                {
                    TempData["SiteMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["SiteMaster"] = ex.Message;
            }
            return RedirectToAction("SiteMaster", "Master");
        }

        public ActionResult SiteList(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.GetSiteList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.SiteID = r["PK_SiteID"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_SiteID"].ToString());
                    obj.SiteName = r["SiteName"].ToString();
                    obj.Location = r["Location"].ToString();
                    obj.Rate = r["Rate"].ToString();
                    obj.UnitName = (r["UnitName"].ToString());
                    obj.DevelopmentCharge = (r["DevelopmentCharge"].ToString());
                    obj.SiteTypeName = (r["SiteTypeName"].ToString());
                    lst.Add(obj);
                }
                model.lstSite = lst;
            }
            return View(model);
        }

        public ActionResult DeleteSite(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.SiteID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteSite();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Site"] = "Site deleted successfully";
                        FormName = "SiteList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["Site"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "SiteList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Site"] = ex.Message;
                FormName = "SiteList";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region SectorMaster

        public ActionResult SectorMaster(string SectorID)
        {
            Master model = new Master();
            #region ddlSite
            Master obj = new Master();
            int count = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet ds1 = obj.GetSiteList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlSite = ddlSite;

            #endregion

            if (SectorID != null)
            {
                model.SectorID = Crypto.Decrypt(SectorID);
                List<Master> lst = new List<Master>();
                DataSet dsSite = model.GetSector();
                if (dsSite != null && dsSite.Tables.Count > 0)
                {
                    model.SiteName = dsSite.Tables[0].Rows[0]["SiteName"].ToString();
                    model.SiteID = dsSite.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    model.SectorID = dsSite.Tables[0].Rows[0]["PK_SectorID"].ToString();
                    model.SectorName = dsSite.Tables[0].Rows[0]["SectorName"].ToString();
                }
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("SectorMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveSector(Master obj)
        {
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveSector();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SectorMaster"] = "Sector saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["SectorMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["SectorMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["SectorMaster"] = ex.Message;
            }
            return RedirectToAction("SectorMaster", "Master");
        }

        [HttpPost]
        [ActionName("SectorMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateSector(Master obj)
        {
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateSector();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SectorMaster"] = "Sector updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["SectorMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["SectorMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["SectorMaster"] = ex.Message;
            }
            return RedirectToAction("SectorMaster", "Master");
        }

        public ActionResult SectorList(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.GetSector();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.SectorID = r["PK_SectorID"].ToString();
                    obj.SiteID = r["FK_SiteID"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_SectorID"].ToString());
                    obj.SectorName = r["SectorName"].ToString();

                    lst.Add(obj);
                }
                model.lstSector = lst;
            }
            return View(model);
        }

        public ActionResult DeleteSector(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.SectorID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteSector();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SectorMaster"] = "Sector deleted successfully";
                        FormName = "SectorList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["SectorMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "SectorList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SectorMaster"] = ex.Message;
                FormName = "SectorList";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
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

        #region BlockMaster
        public ActionResult BlockMaster(string BlockID)
        {
            Master model = new Master();
            if (BlockID != null)
            {
                model.BlockID = BlockID;

                DataSet dsSite = model.GetBlockList();
                if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
                {
                    model.SiteName = dsSite.Tables[0].Rows[0]["SiteName"].ToString();
                    model.SiteID = dsSite.Tables[0].Rows[0]["PK_SiteID"].ToString();
                    model.SectorID = dsSite.Tables[0].Rows[0]["PK_SectorID"].ToString();
                    model.SectorName = dsSite.Tables[0].Rows[0]["SectorName"].ToString();
                    model.BlockName = dsSite.Tables[0].Rows[0]["BlockName"].ToString();
                    model.BlockID = dsSite.Tables[0].Rows[0]["PK_BlockID"].ToString();



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
                    ViewBag.ddlSector = ddlSector;
                    #endregion
                    model.SectorID = dsSite.Tables[0].Rows[0]["PK_SectorID"].ToString();
                    #region BlockList
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    Master objmodel = new Master();
                    objmodel.SiteID = dsSite.Tables[0].Rows[0]["PK_SiteID"].ToString();


                    ViewBag.ddlBlock = lstBlock;
                    #endregion

                }
            }
            else
            {

                List<SelectListItem> ddlSector = new List<SelectListItem>();
                ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
                ViewBag.ddlSector = ddlSector;


            }

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet Site = model.GetSiteList();
            if (Site != null && Site.Tables.Count > 0 && Site.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in Site.Tables[0].Rows)
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

            return View(model);

        }

        [HttpPost]
        [ActionName("BlockMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveBlock(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveBlock();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockMaster"] = "Block saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["BlockMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["BlockMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["BlockMaster"] = ex.Message;
            }
            return RedirectToAction("BlockMaster", "Master");
        }

        [HttpPost]
        [ActionName("BlockMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateBlock(Master obj)
        {
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateBlock();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockMaster"] = "Block updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["BlockMaster"] = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                else
                {
                    TempData["BlockMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["BlockMaster"] = ex.Message;
            }
            return RedirectToAction("BlockMaster", "Master");
        }

        public ActionResult BlockList(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.GetBlockList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.SectorID = r["PK_SectorID"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_SectorID"].ToString());
                    obj.SiteID = r["PK_SiteID"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    obj.BlockID = r["PK_BlockID"].ToString();
                    obj.BlockName = r["BlockName"].ToString();

                    lst.Add(obj);
                }
                model.lstBlock1 = lst;
            }
            return View(model);
        }

        public ActionResult DeleteBlock(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.BlockID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteBlock();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockMaster"] = "Block deleted successfully";
                        FormName = "BlockList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["BlockMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "BlockList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockMaster"] = ex.Message;
                FormName = "BlockList";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region PlotSizeMaster

        public ActionResult PlotSizeList()
        {
            Master model = new Master();
            List<Master> lst = new List<Master>();

            DataSet ds = model.GetPlotSizeList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PlotSizeID = r["PK_PlotSizeMaster"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_PlotSizeMaster"].ToString());
                    obj.WidthFeet = r["WidthFeet"].ToString();
                    obj.WidthInch = r["WidthInch"].ToString();
                    obj.HeightFeet = r["HeightFeet"].ToString();
                    obj.HeightInch = (r["HeightInch"].ToString());
                    obj.PlotArea = (r["PlotArea"].ToString());

                    lst.Add(obj);
                }
                model.lstSite = lst;
            }

            return View(model);
        }

        public ActionResult PlotSizeMaster(string plotSizeID)
        {
            Master model = new Master();
            #region ddlUnits
            Master obj = new Master();
            int count = 0;
            List<SelectListItem> ddlUnit = new List<SelectListItem>();
            DataSet ds1 = obj.GetUnitList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlUnit.Add(new SelectListItem { Text = "Select Unit", Value = "0" });
                    }
                    ddlUnit.Add(new SelectListItem { Text = r["UnitName"].ToString(), Value = r["PK_UnitID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlUnit = ddlUnit;

            #endregion

            if (plotSizeID != null)
            {
                model.PlotSizeID = Crypto.Decrypt(plotSizeID);
                DataSet ds = new DataSet();
                ds = model.GetPlotSizeList();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.PlotSizeID = ds.Tables[0].Rows[0]["PK_PlotSizeMaster"].ToString();
                    model.WidthFeet = ds.Tables[0].Rows[0]["WidthFeet"].ToString();
                    model.WidthInch = ds.Tables[0].Rows[0]["WidthInch"].ToString();
                    model.HeightFeet = ds.Tables[0].Rows[0]["HeightFeet"].ToString();
                    model.HeightInch = ds.Tables[0].Rows[0]["HeightInch"].ToString();
                    model.PlotArea = ds.Tables[0].Rows[0]["PlotArea"].ToString();
                }
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("PlotSizeMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SavePlotSize(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SavePlotSize();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["PlotSize"] = "Plot Size saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["PlotSize"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["PlotSize"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["PlotSize"] = ex.Message;
            }
            return RedirectToAction("PlotSizeMaster", "Master");
        }

        [HttpPost]
        [ActionName("PlotSizeMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdatePlotSize(Master obj)
        {
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdatePlotSize();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["PlotSize"] = "Plot Size updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["PlotSize"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["PlotSize"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["PlotSize"] = ex.Message;
            }
            return RedirectToAction("PlotSizeMaster", "Master");
        }

        public ActionResult DeletePlotSize(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.PlotSizeID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeletePlotSize();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["PlotSize"] = "Plot Size deleted successfully";
                        FormName = "PlotSizeList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["PlotSize"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "PlotSizeList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["PlotSize"] = ex.Message;
                FormName = "PlotSizeList";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }


        #endregion


        #region PlotMaster
        public ActionResult GetSiteDetailsForPlotBooking(string SiteID)
        {
            try
            {
                Plot model = new Plot();
                model.SiteID = SiteID;

                #region GetSiteRate
                //DataSet dsSiteRate = model.GetSiteList();
                //if (dsSiteRate != null)
                //{
                //    model.Rate = dsSiteRate.Tables[0].Rows[0]["Rate"].ToString();
                //    model.Result = "yes";
                //}
                #endregion
                #region GetSectors
                List<SelectListItem> ddlSector = new List<SelectListItem>();
                model.Result = "yes";
                DataSet dsSector = model.GetSectorList();

                if (dsSector != null && dsSector.Tables.Count > 0)
                {
                    foreach (DataRow r in dsSector.Tables[1].Rows)
                    {
                        ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                    }
                }
                model.ddlSector = ddlSector;
                #endregion
                //#region SitePLCCharge
                //List<Master> lstPlcCharge = new List<Master>();
                //DataSet dsPlcCharge = model.GetPLCChargeList();

                //if (dsPlcCharge != null && dsPlcCharge.Tables.Count > 0)
                //{
                //    foreach (DataRow r in dsPlcCharge.Tables[0].Rows)
                //    {
                //        Master obj = new Master();
                //        obj.SiteName = r["SiteName"].ToString();
                //        obj.PLCName = r["PLCName"].ToString();
                //        obj.PLCCharge = r["PLCCharge"].ToString();
                //        obj.PLCID = r["PK_PLCID"].ToString();

                //        lstPlcCharge.Add(obj);
                //    }
                //    model.lstPLC = lstPlcCharge;
                //}
                //#endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public ActionResult PlotMaster(Master model)
        {
            #region ddlSites
            Master obj = new Master();
            int count = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet ds1 = obj.GetSiteList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlSite = ddlSite;

            #endregion

            #region ddlPlotSize
            int countplot = 0;
            List<SelectListItem> ddlPlotSize = new List<SelectListItem>();
            DataSet dsPlotSize = obj.GetPlotSize();

            if (dsPlotSize != null && dsPlotSize.Tables.Count > 0 && dsPlotSize.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPlotSize.Tables[0].Rows)
                {
                    if (countplot == 0)
                    {
                        ddlPlotSize.Add(new SelectListItem { Text = "Select Plot Size", Value = "0" });
                    }
                    ddlPlotSize.Add(new SelectListItem { Text = r["PlotSize"].ToString(), Value = r["PK_PlotSizeMaster"].ToString() });
                    countplot = countplot + 1;
                }
            }

            ViewBag.ddlPlotSize = ddlPlotSize;

            #endregion

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;

            return View();
        }

        [HttpPost]
        [ActionName("PlotMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SavePlot(Master obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("PK_PLCID", typeof(string));
                dt.Columns.Add("Charge", typeof(string));

                string hdrows = Request["hdRows"].ToString();
                for (int i = 0; i < int.Parse(hdrows) - 1; i++)
                {
                    string plcid = Request["plcid_ " + i].ToString();
                    string charge = Request["txtCharge_ " + i].ToString();

                    DataRow dr = dt.NewRow();
                    dr = dt.NewRow();

                    dr["PK_PLCID"] = plcid;
                    dr["Charge"] = string.IsNullOrEmpty(charge) ? "0" : charge;
                    dt.Rows.Add(dr);
                }

                obj.dtPLCCharge = dt;
                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.SavePlot();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = "Plot saved successfully !";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "PlotMaster";
            Controller = "Master";

            return RedirectToAction(FormName, Controller);
        }

        //Site Rate, Sectors & PLC Charge List of the selected Site

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

        #endregion

        #region PlotMaster-List-Update-Delete

        public ActionResult PlotList(Master model)
        {
            DataSet ds = model.GetPlotList();


            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;


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

            return View(model);
        }

        [HttpPost]
        [ActionName("PlotList")]
        [OnAction(ButtonName = "SearchPlot")]
        public ActionResult PlotListDetails(Master model,string SiteID,string SectorID)
        {
            List<Master> lst = new List<Master>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;

            DataSet ds = model.GetPlotList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PlotID = r["PK_PlotID"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_PlotID"].ToString());
                    obj.SiteName = r["SiteName"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    obj.BlockName = r["BlockName"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.PlotArea = r["TotalArea"].ToString();
                    obj.PlotRate = r["PlotRate"].ToString();
                    obj.PlotAmount = r["PlotAmount"].ToString();
                    obj.PLCCharge = r["PLCCharge"].ToString();
                    obj.BookingPercent = r["BookingPercent"].ToString();
                    obj.AllottmentPercent = (r["AllottmentPercent"].ToString());
                    obj.PlotStatus = (r["PlotStatus"].ToString());
                    obj.ColorCSS = (r["StatusColor"].ToString());
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
            return View(model);
        }

        public ActionResult UpdatePlot(string PlotID)
        {
            Master model = new Master();
            try
            {
                if (PlotID != null)
                {
                    #region ddlSites
                    Master obj = new Master();
                    int count = 0;
                    List<SelectListItem> ddlSite = new List<SelectListItem>();
                    DataSet ds1 = obj.GetSiteList();
                    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds1.Tables[0].Rows)
                        {
                            if (count == 0)
                            {
                                ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                            }
                            ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                            count = count + 1;
                        }
                    }

                    ViewBag.ddlSite = ddlSite;

                    #endregion

                    #region ddlPlotSize
                    int countplot = 0;
                    List<SelectListItem> ddlPlotSize = new List<SelectListItem>();
                    DataSet dsPlotSize = obj.GetPlotSize();

                    if (dsPlotSize != null && dsPlotSize.Tables.Count > 0 && dsPlotSize.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in dsPlotSize.Tables[0].Rows)
                        {
                            if (countplot == 0)
                            {
                                ddlPlotSize.Add(new SelectListItem { Text = "Select Plot Size", Value = "0" });
                            }
                            ddlPlotSize.Add(new SelectListItem { Text = r["PlotSize"].ToString(), Value = r["PK_PlotSizeMaster"].ToString() });
                            countplot = countplot + 1;
                        }
                    }

                    ViewBag.ddlPlotSize = ddlPlotSize;

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
                    ViewBag.ddlSector = ddlSector;
                    #endregion

                    #region ddlBlock
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    DataSet dsblock = model.GetBlockList();
                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsblock.Tables[0].Rows)
                        {
                            lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                        }
                    }

                    ViewBag.ddlBlock = lstBlock;
                    #endregion

                    model.PlotID = Crypto.Decrypt(PlotID);

                    DataSet dsPlotDetails = model.GetPlotList();
                    if (dsPlotDetails != null && dsPlotDetails.Tables.Count > 0)
                    {
                        model.PlotID = PlotID;
                        model.PlotSizeID = dsPlotDetails.Tables[0].Rows[0]["PK_PlotSizeMaster"].ToString();
                        model.SiteID = dsPlotDetails.Tables[0].Rows[0]["PK_SiteID"].ToString();
                        model.SiteName = dsPlotDetails.Tables[0].Rows[0]["SiteName"].ToString();
                        model.SectorID = dsPlotDetails.Tables[0].Rows[0]["PK_SectorID"].ToString();
                        model.SectorName = dsPlotDetails.Tables[0].Rows[0]["SectorName"].ToString();
                        model.BlockID = dsPlotDetails.Tables[0].Rows[0]["PK_BlockID"].ToString();
                        model.BlockName = dsPlotDetails.Tables[0].Rows[0]["BlockName"].ToString();
                        model.PlotNumber = dsPlotDetails.Tables[0].Rows[0]["PlotNumber"].ToString();
                        model.PlotArea = dsPlotDetails.Tables[0].Rows[0]["TotalArea"].ToString();
                        model.PlotRate = dsPlotDetails.Tables[0].Rows[0]["PlotRate"].ToString();
                        model.PlotAmount = dsPlotDetails.Tables[0].Rows[0]["PlotAmount"].ToString();
                        model.PLCCharge = dsPlotDetails.Tables[0].Rows[0]["PLCCharge"].ToString();
                        model.BookingPercent = dsPlotDetails.Tables[0].Rows[0]["BookingPercent"].ToString();
                        model.AllottmentPercent = dsPlotDetails.Tables[0].Rows[0]["AllottmentPercent"].ToString();
                        model.PlotStatus = dsPlotDetails.Tables[0].Rows[0]["PlotStatus"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        [HttpPost]
        [ActionName("UpdatePlot")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdatePlot(Master model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("PK_PLCID", typeof(string));
                dt.Columns.Add("Charge", typeof(string));

                string hdrows = Request["hdRows"].ToString();
                for (int i = 0; i < int.Parse(hdrows) - 1; i++)
                {
                    string plcid = Request["plcid_ " + i].ToString();
                    string charge = Request["txtCharge_ " + i].ToString();

                    DataRow dr = dt.NewRow();
                    dr = dt.NewRow();

                    dr["PK_PLCID"] = plcid;
                    dr["Charge"] = string.IsNullOrEmpty(charge) ? "0" : charge;
                    dt.Rows.Add(dr);
                }

                model.dtPLCCharge = dt;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.PlotID = Crypto.Decrypt(model.PlotID);
                DataSet ds = model.UpdatePlot();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = "Plot updated successfully !";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "PlotMaster";
            Controller = "Master";

            return RedirectToAction(FormName, Controller);
        }


        public ActionResult DeletePlot(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.PlotID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeletePlot();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = "Plot deleted successfully";
                        FormName = "PlotList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "PlotList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
                FormName = "PlotList";
                Controller = "Master";
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion


        public ActionResult GetMenuDetails(string URL)
        {
            try
            {
                Master model = new Master();
                model.Fk_UserId = Session["Pk_AdminId"].ToString();
                model.UserType = Session["UserTypeName"].ToString();
                model.Url = URL;
                DataSet ds = model.GetMenuPermissionList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        var MenuList = JsonConvert.SerializeObject(ds.Tables[0]);
                        return Json(MenuList, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json("0", JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public ActionResult PincodeMaster(string Id)

        {
            Master model = new Master();
            model.PinCodeId = Id;
            if (model.PinCodeId != null)
            {
                DataSet ds = model.GetPinCode();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.Pincode = ds.Tables[0].Rows[0]["pincode"].ToString();
                    model.RegionName = ds.Tables[0].Rows[0]["regionname"].ToString();
                    model.Taluk = ds.Tables[0].Rows[0]["Taluk"].ToString();
                    model.District = ds.Tables[0].Rows[0]["Districtname"].ToString();
                    model.StateName = ds.Tables[0].Rows[0]["statename"].ToString();
                }
            }
            return View(model);
        }


        public ActionResult PincodeList()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PincodeList(Master model)
        {
            List<Master> lst = new List<Master>();

            int count1 = 0;
            List<SelectListItem> ddlstate = new List<SelectListItem>();
            DataSet ds1 = model.GetStateList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlstate.Add(new SelectListItem { Text = "-select-", Value = "0" });
                    }
                    ddlstate.Add(new SelectListItem { Text = dr["StateName"].ToString(), Value = dr["fK_StateID"].ToString() });
                    count1 = count1 + 1;
                }
            }
            ViewBag.ddlstate = ddlstate;


            DataSet ds = model.GetPinCode();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PinCodeId = r["PK_Id"].ToString();
                    obj.Pincode = r["pincode"].ToString();
                    obj.RegionName = r["regionname"].ToString();
                    obj.Taluk = r["Taluk"].ToString();
                    obj.District = r["Districtname"].ToString();
                    obj.StateName = r["statename"].ToString();

                    lst.Add(obj);
                }
                model.lstpincode = lst;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult PincodeMaster(Master model)
        {
            try
            {
                if (model.PinCodeId == null)
                {
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = model.SavePinCode();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["success"] = "PinCode save successfully";
                        }
                        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["success"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                    else
                    {
                        TempData["success"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = model.UpdatePinCode();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["success"] = "PinCode Updated successfully";
                        }
                        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["success"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                    else
                    {
                        TempData["success"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["success"] = ex.Message;
            }
            return RedirectToAction("PincodeMaster", "Master");
        }


        public ActionResult DeletePincode(string Id)
        {
            Master model = new Master();
            try
            {
                model.PinCodeId = Id;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeletePinCode();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["success"] = "PinCode delete successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["success"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["success"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["success"] = ex.Message;
            }
            return RedirectToAction("PincodeMaster", "Master");
        }


        public ActionResult CheckPincode(string PincodeNo)
        {
            try
            {
                Master model = new Master();
                model.Pincode = PincodeNo;
                #region GetPincodeDetails
                DataSet ds = model.GetPincodeNo();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.Result = "yes";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        model.Result = "no";
                    }
                }
                else
                {
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

        public ActionResult PlanMaster(string Id)
        {
            Master model = new Master();
            if (Id != null)
            {
                model.PK_PlanId = Id;
                DataSet ds = model.GetPlanNameList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.PlanName = ds.Tables[0].Rows[0]["PlanName"].ToString();
                    model.PlanMonth = ds.Tables[0].Rows[0]["Months"].ToString();
                    model.AlotmentAmount = ds.Tables[0].Rows[0]["AlotmentAmount"].ToString();
                }
            }
            return View(model);
        }



        [HttpPost]
        [ActionName("PlanMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult PlanMaster(Master obj)
        {
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SavePlanMaster();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["PlanMaster"] = "Plan master saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["PlanMaster"] = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                else
                {
                    TempData["PlanMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["PlanMaster"] = ex.Message;
            }
            return RedirectToAction("PlanMaster", "Master");
        }

        public ActionResult PlanMasterList(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetPlanNameList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_PlanId = r["PK_PlanId"].ToString();
                    obj.PlanName = r["PlanName"].ToString();
                    obj.PlanMonth = r["Months"].ToString();
                    obj.AlotmentAmount = r["AlotmentAmount"].ToString();
                    lst.Add(obj);
                }
                model.lstPlan = lst;
            }
            return View(model);
        }



        [HttpPost]
        [ActionName("PlanMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdatePlanMaster(Master obj)
        {
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdatePlanMaster();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["PlanMaster"] = "Plan master updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["PlanMaster"] = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                else
                {
                    TempData["PlanMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["PlanMaster"] = ex.Message;
            }
            return RedirectToAction("PlanMaster", "Master");
        }

         
        public ActionResult DeletePlanMaster(Master obj, string Id)
        {
            try
            {
                if (Id != null)
                {
                    obj.PK_PlanId = Id;
                    obj.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = new DataSet();
                    ds = obj.DeletePlanMaster();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["PlanMaster"] = "Plan master deleted successfully";
                        }
                        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["PlanMaster"] = ds.Tables[0].Rows[0][0].ToString();
                        }
                    }
                    else
                    {
                        TempData["PlanMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["PlanMaster"] = ex.Message;
            }
            return RedirectToAction("PlanMasterList", "Master");
        }




        public ActionResult UpdatePlotStatus(Master model)
        {
            DataSet ds = model.GetPlotList();


            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;


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

            #region PlotStatus

            List<SelectListItem> ddlPlotStatus = Common.BindPlotStatus();
            ViewBag.ddlPlotStatus = ddlPlotStatus;

            #endregion

            return View(model);
        }


        [HttpPost]
        [ActionName("UpdatePlotStatus")]
        [OnAction(ButtonName = "SearchPlot")]
        public ActionResult UpdatePlotStatus(Master model, string SiteID, string SectorID)
        {
            List<Master> lst = new List<Master>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;

            DataSet ds = model.GetPlotList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PlotID = r["PK_PlotID"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_PlotID"].ToString());
                    obj.SiteName = r["SiteName"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    obj.BlockName = r["BlockName"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.PlotArea = r["TotalArea"].ToString();
                    obj.PlotRate = r["PlotRate"].ToString();
                    obj.PlotAmount = r["PlotAmount"].ToString();
                    obj.PLCCharge = r["PLCCharge"].ToString();
                    obj.BookingPercent = r["BookingPercent"].ToString();
                    obj.AllottmentPercent = (r["AllottmentPercent"].ToString());
                    obj.PlotStatus = (r["PlotStatus"].ToString());
                    obj.ColorCSS = (r["StatusColor"].ToString());
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

            #region PlotStatus

            List<SelectListItem> ddlPlotStatus = Common.BindPlotStatus();
            ViewBag.ddlPlotStatus = ddlPlotStatus;

            #endregion
            return View(model);
        }

        

        public ActionResult UpdatePlotStatusDetails(Master model,string PlotId, string PlotStatus)
        {
            string FormName = "";
            string Controller = "";

            model.PlotID = PlotId;
            model.PlotStatus = PlotStatus;
            model.AddedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.UpdatePlotStatus();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["msg"].ToString() == "1")
                {
                    //model.Result = "yes";
                    TempData["PlotStatus"] = "Plot Details Updated  Successfully";
                    FormName = "UpdatePlotStatus";
                    Controller = "Master";
                }
                else
                {
                    //model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    TempData["PlotStatus"]= ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    FormName = "UpdatePlotStatus";
                    Controller = "Master";
                }
            }
            return RedirectToAction(FormName,Controller);
            //return Json(model, JsonRequestBehavior.AllowGet);
        }






    }
}