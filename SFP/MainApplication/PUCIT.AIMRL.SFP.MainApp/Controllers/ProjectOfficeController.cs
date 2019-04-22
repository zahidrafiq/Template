using PUCIT.AIMRL.SFP.MainApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PUCIT.AIMRL.SFP.MainApp.Controllers
{
    public class ProjectOfficeController : BaseController
    {
        // GET: ProjectOffice
        public ActionResult Index()
        {
            return View("RegisteredProjects");
        }
    }
}