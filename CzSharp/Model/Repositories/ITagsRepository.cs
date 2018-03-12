using System.Linq;
using System.Threading.Tasks;
using CzSharp.Model.Entities;

namespace CzSharp.Model.Repositories
{
    public interface ITagsRepository: IRepository<Tag>
    {
        Task<Tag> FindByTitleAsync(string title);
        IQueryable<Tag> FindByTitlePart(string titlePart);
    }
}