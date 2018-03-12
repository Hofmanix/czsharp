using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CzSharp.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace CzSharp.Model.Repositories
{
    public class TagsRepository: Repository<Tag>, ITagsRepository
    {
        public TagsRepository(AppDbContext dbContext): base(dbContext) { }

        public async Task<Tag> FindByTitleAsync(string title)
        {
            return await FindAll().FirstOrDefaultAsync(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        public IQueryable<Tag> FindByTitlePart(string titlePart)
        {
            return FindAll().Where(t => t.Title.IndexOf(titlePart, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}