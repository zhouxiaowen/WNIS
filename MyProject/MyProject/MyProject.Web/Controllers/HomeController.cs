using System.Web.Mvc;

namespace MyProject.Web.Controllers
{
    public class HomeController : MyProjectControllerBase
    {
        public ActionResult Index()
        {
            return View();
            //return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}