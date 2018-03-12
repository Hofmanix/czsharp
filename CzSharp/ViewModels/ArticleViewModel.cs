using System.Collections.Generic;
using CzSharp.Model.Entities;
using CzSharp.Model.Entities.Blog;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CzSharp.ViewModels
{
    public class ArticleViewModel
    {
        public Article Article { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Tags { get; set; }
        [Display(Name = "Tagy")]
        public string SelectedTags { get; set; }
    }
}