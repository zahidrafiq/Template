using PUCIT.AIMRL.SFP.MainApp.Util;
using PUCIT.AIMRL.SFP.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PUCIT.AIMRL.SFP.MainApp.Controllers
{
    public class PartialController : BaseController
    {
        public ActionResult YesNoModal()
        {
            return PartialView();
        }

    }
}
