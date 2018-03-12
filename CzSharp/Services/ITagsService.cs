using System.Collections.Generic;
using System.Threading.Tasks;
using CzSharp.Model.Entities;

namespace CzSharp.Services
{
    public interface ITagsService
    {
        Task<IEnumerable<Tag>> ParseTags(string tagsStr);
        Task<Tag> FindOrCreate(string title);
    }
}