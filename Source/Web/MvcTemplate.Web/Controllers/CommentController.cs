namespace MvcTemplate.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using MvcTemplate.Data.Models;
    using MvcTemplate.Services.Data;
    using MvcTemplate.Web.Infrastructure.Mapping;
    using MvcTemplate.Web.ViewModels.Comment;
    using MvcTemplate.Web.ViewModels.RealEstate;

    public class CommentController : BaseController
    {
        private const int ItemsPerPage = 5;

        private readonly ICommentsService comments;

        public CommentController(ICommentsService comments)
        {
            this.comments = comments;
        }

        public ActionResult Index()
        {
            return this.PartialView();
        }

        [HttpPost]
        public ActionResult AddComment(RealEstateDetails details)
        {
            int id = int.Parse(this.TempData["Id"].ToString());
            var currentComment = this.MapFromModel(details.CommentAddViewModel);
            currentComment.UserId = this.User.Identity.GetUserId();
            currentComment.RealEstateId = id;
            this.comments.Add(currentComment);

            return this.RedirectToAction("RealEstateDetails/" + id, "RealEstate");
        }

        public ActionResult GetRealEstateComments(int id)
        {
            int page = id;
            int realEstateId = int.Parse(this.TempData["Id"].ToString());
            int commentsCount = this.comments.GetAll().Where(c => c.RealEstateId == realEstateId).Count();
            var totalPages = Math.Ceiling(commentsCount / (decimal)ItemsPerPage);
            var itemsToSkip = (page - 1) * ItemsPerPage;

            this.TempData["Id"] = realEstateId;

            var model = this.comments.GetAll()
                .Where(c => c.RealEstateId == realEstateId)
                .OrderByDescending(c => c.CreatedOn)
                .Skip(itemsToSkip).Take(ItemsPerPage).To<CommentPagingViewModel>().ToList();

            return this.PartialView("GetRealEstateComments", model);
        }

        private Comment MapFromModel(CommentAddViewModel model)
        {
            return new Comment() { Content = model.Content };
        }
    }
}