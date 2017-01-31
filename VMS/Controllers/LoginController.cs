using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMS.Models;
using VMS.ClassUtility;

namespace VMS.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View("login");
        }

        public JsonResult login(string un, string pw)
        {
            string ret = "FALSE";

            Model_Login ml = new Model_Login();
            ml.Un = un;
            ml.Pw = pw;

            DataTable userData = ml.ValidateUser(ml);
            if (userData != null)
            {
                if ((userData.Rows[0].ItemArray[0].ToString() == "Y")
                    && (userData.Rows[0].ItemArray[1].ToString() == "Y"))
                {
                    Authentication.SetSessionLogin(ml.Un, ml.Pw, "VENDOR");
                    ret = "members/enlistment";
                }
                else if ((userData.Rows[0].ItemArray[0].ToString() == "Y")
                    && (userData.Rows[0].ItemArray[1].ToString() == "N"))
                {
                    Authentication.SetSessionLogin(ml.Un, ml.Pw, "ADMIN");
                    ret = "admin";
                }
            }
            return Json(ret);
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            
            return Redirect("~");
        }

        public ActionResult RedirectUser()
        {
            return View();
        }
    }
}