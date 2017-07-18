using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Web.Controllers
{
    public class SysController : MyProjectControllerBase
    {
        // GET: Sys
        public ActionResult UserManage()
        {
            return View();
        }
        // 菜单模块
        public ActionResult MenuModule()
        {
            return View();
        }

        public ActionResult Menus()
        {
            return View();
        }
        // 通用字典
        public ActionResult DicManage()
        {
            return View();
        }
    }
}