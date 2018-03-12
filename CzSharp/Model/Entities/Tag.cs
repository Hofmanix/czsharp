using System.Collections.Generic;
using CzSharp.Model.Entities.Blog;
using CzSharp.Model.Entities.Forum;

namespace CzSharp.Model.Entities
{
    public class Tag: IIdentifiable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public virtual List<ArticleTag> ArticleTags { get; set; }
        public virtual List<CodeTag> CodeTags { get; set; }
        public virtual List<DiscussionTag> DiscussionTags { get; set; }
        public virtual List<EventTag> EventTags { get; set; }
    }
}