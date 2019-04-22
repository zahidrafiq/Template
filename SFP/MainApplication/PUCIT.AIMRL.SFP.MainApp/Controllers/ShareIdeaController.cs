using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PUCIT.AIMRL.SFP.MainApp.Controllers
{
    public class ShareIdeaController : Base1Controller
    {
        public ActionResult ShareIdea()
        {
            return View("ShareIdea");
        }
    }
}