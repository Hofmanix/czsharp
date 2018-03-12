using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CzSharp.Model.Entities;

namespace CzSharp.Model.Repositories
{
    public abstract class TaggableRepository<T, TR>: Repository<T>, ITaggableRepository<T, TR> where T: class, ITaggable<TR> where TR: class, ITag, new()
    {
        protected TaggableRepository(AppDbContext dbContext): base(dbContext) {}
        
        public async Task CreateWithTagsAsync(T entity, List<Tag> tags)
        {

            await CreateAsync(entity);
            
            var tagsRefs = tags.Select(t => new TR
            {
                TagId = t.Id
            }).ToList();
            foreach (var tagRef in tagsRefs)
            {
                var property = tagRef.GetType()
                    .GetProperty($"{entity.GetType().Name}Id", BindingFlags.Public | BindingFlags.Instance);
                if (property != null && property.CanWrite)
                {
                    property.SetValue(tagRef, entity.Id, null);
                }
            }
            
            if (tagsRefs.Count > 0)
            {
                await DbContext.Set<TR>().AddRangeAsync(tagsRefs);
                await DbContext.SaveChangesAsync();
            }
        }

        public Task UpdateWithTags(T entity, List<Tag> tags)
        {
            throw new System.NotImplementedException();
        }
    }
}