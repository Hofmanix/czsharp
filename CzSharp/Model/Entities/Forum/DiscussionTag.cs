namespace CzSharp.Model.Entities.Forum
{
    public class DiscussionTag: ITag
    {
        public virtual Tag Tag { get; set; }
        public int TagId { get; set; }
        
        public virtual Discussion Discussion { get; set; }
        public int DiscussionId { get; set; }
    }
}