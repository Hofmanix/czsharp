using CzSharp.Model.Entities.Forum;

namespace CzSharp.Model.Repositories.Forum
{
    public interface IDiscussionsRepository: ITaggableRepository<Discussion, DiscussionTag>
    {
        
    }
}