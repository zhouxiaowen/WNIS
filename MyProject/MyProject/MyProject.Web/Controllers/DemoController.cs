using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Web.Controllers
{
    public class DemoController : Controller
    {
        // GET: Demo
        public ActionResult Table_1()
        {
            return View();
        }

        public ActionResult SetDemoValue()
        {
            return View();
        }
    }
}