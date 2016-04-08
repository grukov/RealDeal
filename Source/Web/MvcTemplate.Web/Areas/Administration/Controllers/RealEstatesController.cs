namespace MvcTemplate.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using MvcTemplate.Data.Models;
    using MvcTemplate.Services.Data;

    public class RealEstatesController : Controller
    {
        private readonly IRealEstatesService realEstates;
        private readonly ICommentsService comments;
        private readonly IRealEstateImagesService images;
        private readonly ILikesService likes;

        public RealEstatesController(IRealEstatesService realEstates, ICommentsService comments,
            IRealEstateImagesService images, ILikesService likes)
        {
            this.realEstates = realEstates;
            this.comments = comments;
            this.images = images;
            this.likes = likes;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult RealEstates_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.GetDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RealEstates_Update([DataSourceRequest]DataSourceRequest request, RealEstate realEstate)
        {
            realEstate.ModifiedOn = DateTime.Now;
            this.realEstates.Update(realEstate);

            return this.Json(new[] { realEstate }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RealEstates_Destroy([DataSourceRequest]DataSourceRequest request, RealEstate realEstate)
        {
            this.DeleteRealEstate(realEstate);

            return this.Json(new[] { realEstate }.ToDataSourceResult(request, this.ModelState));
        }

        private void DeleteRealEstate(RealEstate realEstate)
        {
            var currentEstate = this.realEstates.GetById(realEstate.Id);

            foreach (var comment in realEstate.Comments)
            {
                this.comments.Delete(comment);
            }

            foreach (var image in realEstate.Images)
            {
                this.images.Delete(image);
            }

            foreach (var like in realEstate.Likes)
            {
                this.likes.Delete(like);
            }

            this.realEstates.Delete(currentEstate);
        }

        private DataSourceResult GetDataSourceResult(DataSourceRequest request)
        {
            IQueryable<RealEstate> realestates = this.realEstates.GetAll();
            DataSourceResult result = realestates.ToDataSourceResult(request, realEstate => new
            {
                Id = realEstate.Id,
                Title = realEstate.Title,
                Content = realEstate.Content,
                Size = realEstate.Size,
                Price = realEstate.Price,
                City = realEstate.City,
                Hood = realEstate.Hood,
                EstateType = realEstate.EstateType,
                LettingType = realEstate.LettingType,
                FurnishedState = realEstate.FurnishedState,
                CreatedOn = realEstate.CreatedOn,
                ModifiedOn = realEstate.ModifiedOn,
                IsDeleted = realEstate.IsDeleted,
                DeletedOn = realEstate.DeletedOn
            });

            return result;
        }
    }
}