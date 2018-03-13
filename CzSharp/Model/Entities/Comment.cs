using System;
using CzSharp.Model.Entities.Blog;
using System.ComponentModel.DataAnnotations;

namespace CzSharp.Model.Entities
{
    public class Comment: IIdentifiable
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public virtual User User { get; set; }
        [Required]
        [Display(Name = "Nový komentář")]
        public string Content { get; set; }
        
        public virtual Article Article { get; set; }
        public int? ArticleId { get; set; }
        
        public virtual Code Code { get; set; }
        public int? CodeId { get; set; }
        
        public virtual Event Event { get; set; }
        public int? EventId { get; set; }
    }
}