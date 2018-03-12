using CzSharp.Model.Entities.Forum;

namespace CzSharp.Model.Repositories.Forum
{
    public class DiscussionsRepository: TaggableRepository<Discussion, DiscussionTag>, IDiscussionsRepository
    {
        public DiscussionsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}