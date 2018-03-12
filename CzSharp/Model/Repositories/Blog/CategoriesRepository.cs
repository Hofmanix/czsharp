using System.Collections.Generic;
using CzSharp.Model.Entities.Blog;

namespace CzSharp.Model.Repositories.Blog
{
    public class CategoriesRepository: Repository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(AppDbContext dbContext): base(dbContext) { }
    }
}