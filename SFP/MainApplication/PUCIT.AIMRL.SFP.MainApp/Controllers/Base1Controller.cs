using PUCIT.AIMRL.SFP.MainApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PUCIT.AIMRL.SFP.MainApp.Controllers
{
    public class Base1Controller : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Session["AccessToken"] == null)
            {
                var details = TokenHelper.GetTokenDetails();
                if (details != null && details.ContainsKey("access_token"))
                {
                    Session["AccessToken"] = details["access_token"];
                }
                else
                {
                    Session["AccessToken"] = null;
                }
            }
        }
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
            if (Session["AccessToken"] != null)
            {
                ViewBag.Token = Session["AccessToken"];
            }
        }
    }
}