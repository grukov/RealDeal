namespace MvcTemplate.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using MvcTemplate.Services.Data;

    public class LikeController : BaseController
    {
        private readonly ILikesService likes;
        private readonly IRealEstatesService realEstates;

        public LikeController(ILikesService likes, IRealEstatesService realEstates)
        {
            this.likes = likes;
            this.realEstates = realEstates;
        }

        // GET: Like
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult LikeRealEstate(int id)
        {
            var userId = this.User.Identity.GetUserId();

            var currentLike = this.likes.All().FirstOrDefault(x => x.RealEstateId == id
            && x.UserId == userId);
            if (currentLike == null)
            {
                var like = new Like();
                like.RealEstateId = id;
                like.UserId = this.User.Identity.GetUserId();

                this.likes.Add(like);
            }

            var model = this.realEstates.GetAll().FirstOrDefault(x => x.Id == id);
            return this.PartialView("LikesCountView", model.Likes.Count);
        }

        public ActionResult DislikeRealEstate(int id)
        {
            var userId = this.User.Identity.GetUserId();

            var currentLike = this.likes.All().FirstOrDefault(x => x.RealEstateId == id
            && x.UserId == userId);
            if (currentLike != null)
            {
                this.likes.Delete(currentLike);
            }

            var model = this.realEstates.GetAll().FirstOrDefault(x => x.Id == id);
            return this.PartialView("LikesCountView", model.Likes.Count);
        }
    }
}