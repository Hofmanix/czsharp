using CzSharp.Model.Entities.Forum;

namespace CzSharp.Model.Repositories.Forum
{
    public class TaggableRepository: TaggableRepository<Discussion, DiscussionTag>, IDiscussionsRepository
    {
        public TaggableRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}