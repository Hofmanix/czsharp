using System;
using System.Collections.Generic;

namespace CzSharp.DB.Entities.Forum
{
    public class Discussion: IIdentifiable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public User User { get; set; }
        public DateTime Created { get; set; }
        public Topic Topic { get; set; }
        
        public List<Contribution> Contributions { get; set; }
        public List<Tag> Tags { get; set; }

        public Discussion()
        {
            Contributions = new List<Contribution>();
            Tags = new List<Tag>();
        }
    }
}