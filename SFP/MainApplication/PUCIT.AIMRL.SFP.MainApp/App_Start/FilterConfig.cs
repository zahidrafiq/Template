﻿using System.Web;
using System.Web.Mvc;

namespace PUCIT.AIMRL.SFP.MainApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}