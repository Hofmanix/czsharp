using CzSharp.Model.Entities.Forum;

namespace CzSharp.ViewModels
{
    /// <summary>
    /// View model for discussion show and contribution creating
    /// </summary>
    public class DiscussionViewModel
    {
        public Discussion Discussion { get; set; }
        public Contribution Contribution { get; set; }
        public int DiscussionId { get; set; }
    }
}