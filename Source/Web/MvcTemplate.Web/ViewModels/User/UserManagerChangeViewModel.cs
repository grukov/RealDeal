namespace MvcTemplate.Web.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;
    using MvcTemplate.Data.Models;
    using MvcTemplate.Web.Infrastructure.Mapping;
    using System.Collections.Generic;
    public class UserManagerChangeViewModel : IMapFrom<MvcTemplate.Data.Models.User>
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public ICollection<UserImage> Photos { get; set; }

        public bool HasProfilePicture()
        {
            foreach (var photo in this.Photos)
            {
                if (photo.IsProfilePicture)
                {
                    return true;
                }
            }

            return false;
        }
    }
}