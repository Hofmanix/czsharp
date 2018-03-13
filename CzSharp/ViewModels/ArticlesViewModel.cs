using System.Linq;
using CzSharp.Model.Entities.Blog;

namespace CzSharp.ViewModels
{
    /// <summary>
    /// View model for articles page with pagination
    /// </summary>
    public class ArticlesViewModel
    {
        public IQueryable<Article> Articles { get; set; }
        public int ArticlesCount { get; set; }
        public int ActivePage { get; set; }
    }
}