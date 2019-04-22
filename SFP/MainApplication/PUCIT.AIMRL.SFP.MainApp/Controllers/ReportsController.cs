﻿using PUCIT.AIMRL.SFP.MainApp.Util;
using PUCIT.AIMRL.SFP.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PUCIT.AIMRL.SFP.MainApp.Controllers
{
    public class ReportsController : BaseController
    {
        //
        // GET: /Admin/

        public ActionResult UserLoginHistory()
        {
            if (PUCIT.AIMRL.SFP.MainApp.Security.PermissionManager.perViewLoginHistoryReport == false)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult ForgotPasswordLog()
        {
            if (PUCIT.AIMRL.SFP.MainApp.Security.PermissionManager.perViewLoginHistoryReport == false)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}
