using System.Linq;
using CzSharp.Model.Entities.Blog;

namespace CzSharp.Model.Repositories.Blog
{
    public interface IArticlesRepository: ITaggableRepository<Article, ArticleTag>
    {
        IQueryable<Article> FindPage(int page = 1);
    }
}