using System.Linq;
using CzSharp.Model.Entities.Blog;

namespace CzSharp.Model.Repositories.Blog
{
    public class ArticlesRepository: TaggableRepository<Article, ArticleTag>, IArticlesRepository
    {
        public ArticlesRepository(AppDbContext dbContext): base(dbContext) { }
        
        public IQueryable<Article> FindPage(int page = 1)
        {
            return FindAll().OrderByDescending(a => a.Id).Skip((page - 1) * 10).Take(10);
        }
    }
}