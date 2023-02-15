using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceManagementSystem.Models
{
    public class SessionModel
    {
        public static int RoleId
        {
            get { return HttpContext.Current.Session["RoleId"] == null ? 0 : (int)HttpContext.Current.Session["RoleId"]; }
            set { HttpContext.Current.Session["RoleId"] = value; }
        }
        public static int TeacherId
        {
            get { return HttpContext.Current.Session["TeacherId"] == null ? 0 : (int)HttpContext.Current.Session["TeacherId"]; }
            set { HttpContext.Current.Session["TeacherId"] = value; }
        }
        public static int UserId
        {
            get { return HttpContext.Current.Session["UserId"] == null ? 0 : (int)HttpContext.Current.Session["UserId"]; }
            set { HttpContext.Current.Session["UserId"] = value; }
        }
        public static string Username
        {
            get { return HttpContext.Current.Session["Username"] == null ? "" : (Convert.ToString(HttpContext.Current.Session["Username"])); }
            set { HttpContext.Current.Session["Username"] = value; }
        }


        public static void ClearSession()
        {
            System.Web.HttpContext.Current.Session.Remove("ROLE_ID");
            System.Web.HttpContext.Current.Session.Clear();
        }
    }
}