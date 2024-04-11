using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OmPrabha.Models;
using OmPrabha.Filter;
using System.Data;

namespace OmPrabha.Controllers
{
    public class WebsiteController : Controller
    {
         //GET: Website
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }

        public ActionResult EventGallery()
        {
            return View();
        }
        

        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult OurTeam()
        {
            return View();
        }
        [HttpPost]
        [ActionName("ContactUs")]
        [OnAction(ButtonName = "btnsave")]
        public ActionResult ContactUs(Website model)
        {
            try
            {
                DataSet ds = model.SaveContact();
                if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
                {
                    if(ds.Tables[0].Rows[0]["MSG"].ToString()=="1")
                    {
                        TempData["Contactmsg"] = "Details saved successfully !";
                    }
                    else if(ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
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
            catch(Exception ex)
            {
                TempData["Contactmsg"] = ex.Message;
            }
            return RedirectToAction("ContactUs", "Website");
        }


        public ActionResult Signup()
        {
            return View();
        }
        public ActionResult Signin()
        {
            return View();
        }   
        public ActionResult Property()
        {
            return View();
        }
    

    }
}