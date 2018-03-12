using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CzSharp.Model.Entities.Forum
{
    public class Topic: IIdentifiable
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Název")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Informace")]
        public string Info { get; set; }
        public virtual User User { get; set; }
        public DateTime Created { get; set; }
        public virtual TopicGroup TopicGroup { get; set; }
        
        public virtual List<Discussion> Discussions { get; set; }
    }
}