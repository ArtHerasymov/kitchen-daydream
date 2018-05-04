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


            // Dump data on entry (Using Strategy pattern)
            AbstractBackupManager backupManager;
            if (CheckForInternetConnection())
                backupManager = new AbstractBackupManager(new OnlineBackup());
            else
                backupManager = new AbstractBackupManager(new OfflineBackup());

            backupManager.DumpData();

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
