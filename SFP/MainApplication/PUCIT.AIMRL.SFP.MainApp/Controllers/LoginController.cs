﻿using PUCIT.AIMRL.Common;
using PUCIT.AIMRL.SFP.DAL;
using PUCIT.AIMRL.SFP.MainApp.Models;
using PUCIT.AIMRL.SFP.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PUCIT.AIMRL.SFP.MainApp.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title =PUCIT.AIMRL.SFP.UI.Common.GlobalDataManager.PageTitlePrefix + "Login";
            return View("login");
        }

        public ActionResult Index2()
        {
            ViewBag.Title = PUCIT.AIMRL.SFP.UI.Common.GlobalDataManager.PageTitlePrefix + "Login";
            return View("login");
        }
        public ActionResult ResetPassword1(string rt)
        {
            if (SessionManager.IsUserLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                try
                {
                    UserInfoRepository repo = new UserInfoRepository();
                    if (repo.IsValidResetToken(rt))
                    {
                        ViewBag.data = rt;
                        return View();
                    }
                    else
                    {
                        TempData["Msg"] = "Invalid password reset token!";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index");
                }
            }

        }
        public ActionResult ForgotPassword()
        {
            return PartialView("ForgotPassword");
        }
        public ActionResult LoginPanel()
        {
            return PartialView("LoginPanel");
        }
        public ActionResult AdminLogin()
        {
            return View("AdminLogin");
        }
        public ActionResult test()
        {
            return View("test");
        }

        public ActionResult SignOut()
        {

            if (SessionManager.IsUserLoggedIn && SessionManager.LogsInAsOtherUser)
            {
                UserInfoRepository repo = new UserInfoRepository();
                repo.ValidateUser(SessionManager.ActualUserLoginID, "", "", true, false);
                
                SessionManager.ActualUserUserID = 0;
                SessionManager.LogsInAsOtherUser = false;
                SessionManager.ActualUserLoginID = "";

                return RedirectToAction("Index", "Home");
            }
            else
            {
                SessionManager.AbandonSession();
                return RedirectToAction("Index");
            }
        }
    }
}
