using System;

namespace CzSharp.DB.Entities.Forum
{
    public class Contribution: IIdentifiable
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
        public DateTime Created { get; set; }
        public Discussion Discussion { get; set; }
    }
}