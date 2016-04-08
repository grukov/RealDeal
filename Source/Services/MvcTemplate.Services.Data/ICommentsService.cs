namespace MvcTemplate.Services.Data
{
    using System.Linq;
    using MvcTemplate.Data.Models;

    public interface ICommentsService
    {
        void Add(Comment comment);

        IQueryable<Comment> GetAll();

        void Delete(Comment comment);

        void Update(Comment comment);
    }
}
