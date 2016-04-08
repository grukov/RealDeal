namespace MvcTemplate.Web.ViewModels.User
{
    using System.Collections.Generic;
    using MvcTemplate.Data.Models;
    using MvcTemplate.Web.Infrastructure.Mapping;

    public class UserGalleryViewModel : IMapFrom<MvcTemplate.Data.Models.User>
    {
        public string Id { get; set; }

        public ICollection<UserImage> Photos { get; set; }
    }
}