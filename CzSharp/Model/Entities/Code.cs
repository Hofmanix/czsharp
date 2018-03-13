using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CzSharp.Model.Entities
{
    public class Code: ITaggable<CodeTag>
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Titulek")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Popis")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Ukázka kódu")]
        public string CodeSample { get; set; }
        public virtual User User { get; set; }
        public DateTime Created { get; set; }
        
        public virtual ICollection<CodeTag> CodeTags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}