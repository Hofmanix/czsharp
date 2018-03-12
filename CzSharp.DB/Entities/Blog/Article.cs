using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CzSharp.DB.Entities.Blog
{
    public class Article: IIdentifiable
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime Created { get; set; }
        [Required]
        [Display(Name = "Titulek")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Obsah")]
        public string Content { get; set; }
        [Display(Name = "Kategorie")]
        public Category Category { get; set; }
        
        public List<Tag> Tags { get; set; }

        public Article()
        {
            Tags = new List<Tag>();
        }
    }
}