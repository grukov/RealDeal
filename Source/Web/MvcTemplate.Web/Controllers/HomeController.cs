namespace MvcTemplate.Web.Controllers
{
    using System.Web.Mvc;
    using MvcTemplate.Web.Tools.RssTool;

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            this.ViewBag.FBFeed = RealEstaesRssReader.GetFeed();

            return this.View();
        }
    }
}
