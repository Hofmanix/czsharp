using CzSharp.Model.Entities.Forum;

namespace CzSharp.Model.Repositories.Forum
{
    public class ContributionsRepository: Repository<Contribution>, IContributionsRepository
    {
        public ContributionsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}