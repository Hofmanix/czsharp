using CzSharp.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace CzSharp.ViewModels
{
    public class CodeViewModel
    {
        public Code Code { get; set; }
        [Display(Name = "Tagy")]
        public string SelectedTags { get; set; }
    }
}