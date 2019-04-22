using PUCIT.AIMRL.SFP.Entities;
using PUCIT.AIMRL.SFP.MainApp.Models;
using PUCIT.AIMRL.SFP.MainApp.Util;
using PUCIT.AIMRL.SFP.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PUCIT.AIMRL.SFP.MainApp.Controllers
{
    public class HomeController : BaseController
    {

        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View("Dashboard");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
        
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}
