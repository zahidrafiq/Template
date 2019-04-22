using PUCIT.AIMRL.SFP.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace PUCIT.AIMRL.SFP.MainApp.Controllers
{
    public class UserWallController : Controller
    {
        // GET: UserWall
        [System.Web.Http.HttpPost]
        public ActionResult UserWall([FromUri]int UserId)
        {
            ViewBag.UserId = UserId;
            try
            {
                ViewBag.CurrentUserId = SessionManager.CurrentUser.UserId;
                return View();
            }
            catch
            {
                return View("login");
            }
        }

    }
}