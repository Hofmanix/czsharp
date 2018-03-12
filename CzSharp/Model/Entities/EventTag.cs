namespace CzSharp.Model.Entities
{
    public class EventTag: ITag
    {
        public int EventId { get; set; }
        public virtual Event Event { get; set; }
        
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}