using PUCIT.AIMRL.SFP.MainApp.Util;
using PUCIT.AIMRL.SFP.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PUCIT.AIMRL.SFP.MainApp.Controllers
{
    public class SecurityController : BaseController
    {
        public ActionResult Index()
        {
            if (PUCIT.AIMRL.SFP.MainApp.Security.PermissionManager.perManageSecurityUsers == false)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Permissions");
            }
        }

        //
        // GET: /Admin/
        public ActionResult Users()
        {
            if (PUCIT.AIMRL.SFP.MainApp.Security.PermissionManager.perManageSecurityUsers== false)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Roles()
        {
            if (PUCIT.AIMRL.SFP.MainApp.Security.PermissionManager.perManageSecurityRoles == false)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Id = 1;
                return View();
            }
        }
        public ActionResult Permissions()
        {
            if (PUCIT.AIMRL.SFP.MainApp.Security.PermissionManager.perManageSecurityPermissions == false)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Id = 2;
                return View();
            }
        }
    }
}
