using CzSharp.Model.Entities;

namespace CzSharp.Model.Repositories
{
    public class CommentsRepository: Repository<Comment>, ICommentsRepository
    {
        public CommentsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}