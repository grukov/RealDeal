namespace MvcTemplate.Data.Models
{
    using MvcTemplate.Data.Common.Models;

    public class UserImage : Image
    {
        public bool IsProfilePicture { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
