using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CzSharp.Model.Entities.Blog;
using Microsoft.EntityFrameworkCore;

namespace CzSharp.Model.Repositories.Blog
{
    public class CategoriesRepository: Repository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(AppDbContext dbContext): base(dbContext) { }

        public async Task<Category> FindByTitleAsync(string title)
        {
            return await FindAll().FirstOrDefaultAsync(t => t.Title.ToLower() == title.ToLower());
        }
    }
}