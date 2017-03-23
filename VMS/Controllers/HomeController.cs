using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //App_Code.MailHelper mh = new App_Code.MailHelper();
            //mh.TestEmailTemplate("roger.shubho@fdl.com.bd", "temp id", "roger", "pwd");

            return View();
        }

        public ActionResult SIF()
        {
            ViewBag.Message = "SIF Page Here.";
            return View("../SIF/SIF");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}