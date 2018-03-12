namespace CzSharp.Model.Entities.Blog
{
    public class ArticleTag: ITag
    {
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
        
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}