using System.Collections.Generic;
using System.Threading.Tasks;
using CzSharp.Model.Entities;

namespace CzSharp.Model.Repositories
{
    public interface ITaggableRepository<T, TR>: IRepository<T> where T: ITaggable<TR>
    {
        Task CreateWithTagsAsync(T entity, List<Tag> tags);
        Task UpdateWithTags(T entity, List<Tag> tags);
    }
}