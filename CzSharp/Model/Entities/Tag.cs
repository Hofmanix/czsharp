using System.Collections.Generic;
using CzSharp.Model.Entities.Blog;

namespace CzSharp.Model.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public List<Category> Categories { get; set; }
        public List<Event> Events { get; set; }

        public Tag()
        {
            Categories = new List<Category>();
            Events = new List<Event>();
        }
    }
}