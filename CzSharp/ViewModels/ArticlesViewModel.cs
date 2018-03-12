using System.Linq;
using CzSharp.Model.Entities.Blog;

namespace CzSharp.ViewModels
{
    public class ArticlesViewModel
    {
        public IQueryable<Article> Articles { get; set; }
        public int ArticlesCount { get; set; }
        public int ActivePage { get; set; }
    }
}