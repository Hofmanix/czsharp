using CzSharp.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace CzSharp.ViewModels
{
    public class EventViewModel
    {
        public Event Event { get; set; }
        [Display(Name = "Tagy")]
        public string SelectedTags { get; set; }
    }
}