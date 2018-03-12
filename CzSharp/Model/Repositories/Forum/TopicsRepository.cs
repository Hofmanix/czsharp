using CzSharp.Model.Entities.Forum;

namespace CzSharp.Model.Repositories.Forum
{
    public class TopicsRepository: Repository<Topic>, ITopicsRepository
    {
        public TopicsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}