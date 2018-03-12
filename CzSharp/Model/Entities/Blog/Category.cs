using System;
using System.Collections.Generic;

namespace CzSharp.Model.Entities.Blog
{
    public class Category: IIdentifiable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual User User { get; set; }
        public DateTime Created { get; set; }
        
        public virtual List<Article> Articles { get; set; }

        public Category()
        {
            Articles = new List<Article>();
        }
    }
}