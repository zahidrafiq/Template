﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using PUCIT.AIMRL.SFP.Entities.DBEntities;

namespace PUCIT.AIMRL.SFP.UI.Common
{
    public static class GlobalDataManager
    {
        #region Private Data

        private static String FORMCATEGORIESLIST_KEY = "FormCategoriesData";

        #endregion
        public static String PageTitlePrefix = String.Empty;
        public static Boolean IsCSEncrypted = false;
        public static String BuildVersion = "";
        public static String BasePath = VirtualPathUtility.ToAbsolute("~");


        public static Boolean EnableOptimization = false;
        public static Boolean IgnoreHashing = false;
        public static Boolean UseGmailSMTP = false;
        public static String FromAddress = "";
        public static String SMTPServer = "";
        public static String SMTPPort = "";
        public static String SMTPUser = "";
        public static String SMTPPassword = "";

        public static PUCIT.AIMRL.Common.IEmailHandler _emailhandler = null;

        //public static List<Messages> MessagesList
        //{
        //    get
        //    {
        //        if (ApplicationState[NOTIFICATIONMESSAGES] != null)
        //            return ApplicationState[NOTIFICATIONMESSAGES] as List<Messages>;
        //        else
        //            return new List<Messages>();
        //    }
        //    set
        //    {
        //        ApplicationState[NOTIFICATIONMESSAGES] = value;
        //    }
        //}


        private static HttpApplicationState ApplicationState
        {
            get
            {
                return System.Web.HttpContext.Current.Application;
            }
        }
    }
}