namespace MvcTemplate.Web.ViewModels.Comment
{
    using System;
    using MvcTemplate.Data.Models;
    using MvcTemplate.Web.Infrastructure.Mapping;

    public class CommentPagingViewModel : IMapFrom<MvcTemplate.Data.Models.Comment>
    {
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public MvcTemplate.Data.Models.User User { get; set; }
    }
}