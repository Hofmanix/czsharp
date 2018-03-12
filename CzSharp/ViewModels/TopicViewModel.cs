using CzSharp.Model.Entities.Forum;
using System.ComponentModel.DataAnnotations;

namespace CzSharp.ViewModels
{
    public class TopicViewModel
    {
        public Topic Topic { get; set; }
        public Discussion Discussion { get; set; }
        public Contribution Contribution { get; set; }
        public int TopicId { get; set; }
        [Display(Name = "Tagy")]
        public string SelectedTags { get; set; }
    }
}