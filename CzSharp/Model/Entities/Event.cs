using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CzSharp.Model.Entities
{
    public class Event: ITaggable<EventTag>
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Název")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Od")]
        public DateTime From { get; set; }
        [Required]
        [Display(Name = "Do")]
        public DateTime To { get; set; }
        [Required]
        [Display(Name = "Místo")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Informace")]
        public string Info { get; set; }
        public virtual User User { get; set; }
        public DateTime Created { get; set; }
        
        public virtual ICollection<EventTag> EventTags { get; set; }

    }
}