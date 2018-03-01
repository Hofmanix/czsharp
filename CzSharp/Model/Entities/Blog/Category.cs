using System;
using System.Collections.Generic;

namespace CzSharp.Model.Entities.Blog
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public User User { get; set; }
        public DateTime Created { get; set; }
        
        public List<Article> Articles { get; set; }

        public Category()
        {
            Articles = new List<Article>();
        }
    }
}