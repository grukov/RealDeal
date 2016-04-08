namespace MvcTemplate.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using MvcTemplate.Data.Models;
    using MvcTemplate.Services.Data;

    public class CommentsController : Controller
    {
        private readonly ICommentsService comments;

        public CommentsController(ICommentsService comments)
        {
            this.comments = comments;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Comments_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.GetDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Comments_Update([DataSourceRequest]DataSourceRequest request, Comment comment)
        {
            this.comments.Update(comment);

            return this.Json(new[] { comment }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Comments_Destroy([DataSourceRequest]DataSourceRequest request, Comment comment)
        {
            this.comments.Delete(comment);

            return this.Json(new[] { comment }.ToDataSourceResult(request, this.ModelState));
        }

        private DataSourceResult GetDataSourceResult(DataSourceRequest request)
        {
            IQueryable<Comment> comments = this.comments.GetAll();
            DataSourceResult result = comments.ToDataSourceResult(request, comment => new {
                Id = comment.Id,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                ModifiedOn = comment.ModifiedOn,
                IsDeleted = comment.IsDeleted,
                DeletedOn = comment.DeletedOn
            });

            return result;
        }
    }
}
