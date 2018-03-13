using CzSharp.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace CzSharp.ViewModels
{
    /// <summary>
    /// View model for event creation
    /// </summary>
    public class EventViewModel
    {
        public Event Event { get; set; }
        [Display(Name = "Tagy")]
        public string SelectedTags { get; set; }
    }
}