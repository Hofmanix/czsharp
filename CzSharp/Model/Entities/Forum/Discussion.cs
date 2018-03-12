using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CzSharp.Model.Entities.Forum
{
    public class Discussion: ITaggable<DiscussionTag>
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Název")]
        public string Title { get; set; }
        public virtual User User { get; set; }
        public DateTime Created { get; set; }
        public virtual Topic Topic { get; set; }
        
        public virtual List<Contribution> Contributions { get; set; }
        public virtual List<DiscussionTag> DiscussionTags { get; set; }
    }
}