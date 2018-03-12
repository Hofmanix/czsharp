using System;
using System.ComponentModel.DataAnnotations;

namespace CzSharp.Model.Entities.Forum
{
    public class Contribution: IIdentifiable
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Příspěvek")]
        public string Content { get; set; }
        public virtual User User { get; set; }
        public DateTime Created { get; set; }
        public virtual Discussion Discussion { get; set; }
    }
}