namespace MvcTemplate.Services.Data
{
    using System.Linq;
    using MvcTemplate.Data.Common;
    using MvcTemplate.Data.Models;

    public class CommentsService : ICommentsService
    {
        private IDbRepository<Comment> comments;

        public CommentsService(IDbRepository<Comment> comments)
        {
            this.comments = comments;
        }

        public void Add(Comment comment)
        {
            this.comments.Add(comment);
            this.comments.Save();
        }

        public IQueryable<Comment> GetAll()
        {
            return this.comments.All();
        }

        public void Delete(Comment comment)
        {
            this.comments.Delete(comment);
            this.comments.Save();
        }

        public void Update(Comment comment)
        {
            this.ModifyState(comment);
        }

        private void ModifyState(Comment comment)
        {
            var currentComment = this.comments.GetById(comment.Id);

            currentComment.Content = comment.Content;
            this.comments.Save();
        }
    }
}
