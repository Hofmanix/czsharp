using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CzSharp.Model.Entities.Blog
{
    public class Article: ITaggable<ArticleTag>
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public DateTime Created { get; set; }
        [Required]
        [Display(Name = "Titulek")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Obsah")]
        public string Content { get; set; }
        [Display(Name = "Kategorie")]
        public virtual Category Category { get; set; }

        public virtual ICollection<ArticleTag> ArticleTags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public Article()
        {
            ArticleTags = new List<ArticleTag>();
        }
    }
}