using System;
using System.Collections.Generic;

namespace CzSharp.Model.Entities.Forum
{
    public class TopicGroup
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public User User { get; set; }
        public DateTime Created { get; set; }
        
        public List<Topic> Topics { get; set; }

        public TopicGroup()
        {
            Topics = new List<Topic>();
        }
    }
}