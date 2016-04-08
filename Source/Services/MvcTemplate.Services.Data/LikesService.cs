namespace MvcTemplate.Services.Data
{
    using System.Linq;
    using MvcTemplate.Data.Common;
    using MvcTemplate.Data.Models;

    public class LikesService : ILikesService
    {
        private IDbRepository<Like> likes;

        public LikesService(IDbRepository<Like> likes)
        {
            this.likes = likes;
        }
        
        public IQueryable<Like> All()
        {
            return this.likes.All();
        }

        public Like GetById(int id)
        {
            return this.likes.GetById(id);
        }

        public void Add(Like like)
        {
            this.likes.Add(like);
            this.likes.Save();
        }

        public void Delete(Like like)
        {
            this.likes.HardDelete(like);
            this.likes.Save();
        }
    }
}
