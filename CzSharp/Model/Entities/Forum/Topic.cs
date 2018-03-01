using System;
using System.Collections.Generic;

namespace CzSharp.Model.Entities.Forum
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public User User { get; set; }
        public DateTime Created { get; set; }
        public TopicGroup TopicGroup { get; set; }
        
        public List<Discussion> Discussions { get; set; }

        public Topic()
        {
            Discussions = new List<Discussion>();
        }
    }
}