using System.Threading.Tasks;
using CzSharp.Model.Entities.Blog;

namespace CzSharp.Model.Repositories.Blog
{
    public interface ICategoriesRepository: IRepository<Category>
    {
        Task<Category> FindByTitleAsync(string title);
    }
}