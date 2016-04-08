using System.ComponentModel.DataAnnotations;
using MvcTemplate.Web.Infrastructure.Mapping;

namespace MvcTemplate.Web.ViewModels.Comment
{
    public class CommentAddViewModel : IMapFrom<MvcTemplate.Data.Models.Comment>
    {
        [Required]
        [MinLength(5)]
        public string Content { get; set; }

        public string UserId { get; set; }

        public int RealEstateId { get; set; }
    }
}