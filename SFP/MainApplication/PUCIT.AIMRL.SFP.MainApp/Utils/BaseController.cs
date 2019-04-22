﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PUCIT.AIMRL.SFP.UI.Common;

namespace PUCIT.AIMRL.SFP.MainApp.Util
{
    public partial class BaseController : Controller
    {

        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (SessionManager.IsUserLoggedIn == false)
            {
                //Abandons the current session and redirect to Login Page
                SessionManager.AbandonSession();

                String queryString = "ReturnURL=" + this.Request.RawUrl.Replace("%", "_");
                context.Result = new RedirectResult(Resources.PAGES_DEFAULT_LOGIN_PAGE + "?" + queryString);
                return;
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            Exception objErr = filterContext.Exception;

        }

        public BaseController()
        {
            var user = SessionManager.CurrentUser;

            if (user != null)
            {
                String username = user.Login;
                ViewBag.UserId = SessionManager.CurrentUser.UserId;
                ViewBag.DispName = username;
                ViewBag.FullName = user.UserFullName;
                ViewBag.Login = user.Login;
                ViewBag.Email = user.Email;
            }
        }

        public ActionResult RedirectToNormalDashboard()
        {
            ViewBag.Title = PUCIT.AIMRL.SFP.UI.Common.GlobalDataManager.PageTitlePrefix + "Home";
            return RedirectToAction("Home", "Scheduling");
        }
        public ActionResult RedirectToAgentDashBoard()
        {
            ViewBag.Title = PUCIT.AIMRL.SFP.UI.Common.GlobalDataManager.PageTitlePrefix + "Agent Dashboard";
            return RedirectToAction("AgentDashboard", "Scheduling");
        }
    }

}
