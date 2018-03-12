using System.Linq;
using CzSharp.Model.Entities;

namespace CzSharp.Model.Repositories
{
    public interface ICodeRepository: ITaggableRepository<Code, CodeTag>
    {
        IQueryable<Code> FindPage(int page = 1);
    }
}