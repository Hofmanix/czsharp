namespace CzSharp.Model.Entities
{
    public class CodeTag: ITag
    {
        public virtual Tag Tag { get; set; }
        public int TagId { get; set; }
        
        public virtual Code Code { get; set; }
        public int CodeId { get; set; }
    }
}