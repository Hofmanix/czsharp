using CzSharp.Model.Entities.Forum;

namespace CzSharp.Model.Repositories.Forum
{
    public class TopicGroupsRepository: Repository<TopicGroup>, ITopicGroupsRepository
    {
        public TopicGroupsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}