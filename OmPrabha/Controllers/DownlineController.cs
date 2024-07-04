using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OmPrabha.Models;
using OmPrabha.Filter;

namespace OmPrabha.Controllers
{
    public class DownlineController : BaseController
    {

        public ActionResult DirectList()
        {
   
            List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            ViewBag.ddlStatus = AssociateStatus;
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.ddlleg = Leg;

            Reports model = new Reports();
            List<Reports> lst = new List<Reports>();
            model.LoginId = Session["LoginId"].ToString();
            model.FromDate = Common.ConvertToSystemDate(DateTime.Today.ToShortDateString(), "MM/dd/yyyy");
            model.ToDate = Common.ConvertToSystemDate(DateTime.Today.ToShortDateString(), "MM/dd/yyyy");
            DataSet ds = model.GetDirectList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.SponsorId = (r["LoginId"].ToString());
                    obj.SponsorName = (r["Name"].ToString());
                    obj.Package = (r["ProductName"].ToString());
                    lst.Add(obj);
                }
                model.lstassociate = lst;

            }
            return View(model);
        }

        [HttpPost]
        [ActionName("DirectList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult DirectListBy(Reports model)
        {

            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.FromActivationDate = string.IsNullOrEmpty(model.FromActivationDate) ? null : Common.ConvertToSystemDate(model.FromActivationDate, "dd/MM/yyyy");
            model.ToActivationDate = string.IsNullOrEmpty(model.ToActivationDate) ? null : Common.ConvertToSystemDate(model.ToActivationDate, "dd/MM/yyyy");
            List<Reports> lst = new List<Reports>();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetDirectList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.SponsorId = (r["LoginId"].ToString());
                    obj.SponsorName = (r["Name"].ToString());
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


        public ActionResult DownLineList()
        {
            List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            ViewBag.ddlStatus = AssociateStatus;
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.ddlleg = Leg;

            Reports model = new Reports();
            //List<Reports> lst = new List<Reports>();
            //model.LoginId = Session["LoginId"].ToString();
            //model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            //model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            //model.FromActivationDate = string.IsNullOrEmpty(model.FromActivationDate) ? null : Common.ConvertToSystemDate(model.FromActivationDate, "dd/MM/yyyy");
            //model.ToActivationDate = string.IsNullOrEmpty(model.ToActivationDate) ? null : Common.ConvertToSystemDate(model.ToActivationDate, "dd/MM/yyyy");
            //DataSet ds = model.GetDownlineList();

            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in ds.Tables[0].Rows)
            //    {
            //        Reports obj = new Reports();
            //        obj.Name = r["Name"].ToString();
            //        obj.LoginId = r["LoginId"].ToString();
            //        obj.PlotNumber = r["PlotNumber"].ToString();
            //        obj.JoiningDate = r["JoiningDate"].ToString();
            //        obj.Leg = r["Leg"].ToString();
            //        obj.PermanentDate = (r["PermanentDate"].ToString());
            //        obj.Status = (r["Status"].ToString());
            //        obj.Mobile = (r["Mobile"].ToString());
            //        obj.Package = (r["ProductName"].ToString());
            //        lst.Add(obj);
            //    }
            //    model.lstassociate = lst;


            //}
            return View(model);
        }
        [HttpPost]
        [ActionName("DownLineList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult DownLineListBy(Reports model)
        {

            List<Reports> lst = new List<Reports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.FromActivationDate = string.IsNullOrEmpty(model.FromActivationDate) ? null : Common.ConvertToSystemDate(model.FromActivationDate, "dd/MM/yyyy");
            model.ToActivationDate = string.IsNullOrEmpty(model.ToActivationDate) ? null : Common.ConvertToSystemDate(model.ToActivationDate, "dd/MM/yyyy");
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetDownlineList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Name = r["Name"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.SponsorId= r["SponsorLoginId"].ToString();
                    obj.SponsorName = r["SponsorName"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.Mobile = (r["Mobile"].ToString());
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
    }
}