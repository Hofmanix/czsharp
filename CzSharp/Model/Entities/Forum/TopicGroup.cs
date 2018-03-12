using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CzSharp.Model.Entities.Forum
{
    public class TopicGroup: IIdentifiable
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Název")]
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public virtual User User { get; set; }
        
        public virtual List<Topic> Topics { get; set; }
    }
}