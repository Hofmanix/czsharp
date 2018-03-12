using System.Linq;
using CzSharp.Model.Entities;

namespace CzSharp.Model.Repositories
{
    public class CodeRepository: TaggableRepository<Code, CodeTag>, ICodeRepository
    {
        public CodeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Code> FindPage(int page = 1)
        {
            return FindAll().OrderByDescending(a => a.Id).Skip((page - 1) * 10).Take(10);
        }
    }
}