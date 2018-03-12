using System;
using System.Linq;
using CzSharp.Model.Entities.Forum;
using System.ComponentModel.DataAnnotations;
using CzSharp.Model.Entities;

namespace CzSharp.ViewModels
{
    public class ForumIndexViewModel
    {
        public IQueryable<TopicGroup> TopicGroups { get; set; }
        public TopicGroup NewTopicGroup { get; set; }
        public Topic NewTopic { get; set; }
        [Required]
        [Display(Name = "Skupina témat")]
        public string SelectedTopicGroup { get; set; }
    }
}