using System.Web.Mvc;
using MvcTemplate.Data.Common;
using MvcTemplate.Data.Models;

namespace MvcTemplate.Web.Controllers
{
    public class RealEstateImageController : BaseController
    {
        private readonly IDbRepository<RealEstateImage> realEstateImages;

        public RealEstateImageController(IDbRepository<RealEstateImage> realEstateImages)
        {
            this.realEstateImages = realEstateImages;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult GetImage(int id)
        {
            var image = this.realEstateImages.GetById(id);

            return this.File(image.Content, "image/jpeg");
        }
    }
}