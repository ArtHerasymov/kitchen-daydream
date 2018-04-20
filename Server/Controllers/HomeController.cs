using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Server.BL;
using System.Net;

namespace Server.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";


            // Dump data on entry (Using Bridge pattern)
            IBackupPlan backupPlan;
            if (CheckForInternetConnection())
                backupPlan = new OnlineBackup();
            else
                backupPlan = new OfflineBackup();

            backupPlan.DumpData();

            return View();
        }


        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
