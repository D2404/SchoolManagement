using InvoiceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        clsCommon objCommon = new clsCommon();
        public ActionResult Index(DashboardModel cls)
        {
            try
            {
                if (objCommon.getUserIdFromSession() != 0)
                {
                    cls = cls.GetUserAccountDashboardCount(cls);
                    return View(cls);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Index1()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}