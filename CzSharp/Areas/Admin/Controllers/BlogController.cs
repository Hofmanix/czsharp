using System.Linq;
using CzSharp.Model;
using CzSharp.Model.Entities.Blog;
using CzSharp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CzSharp.Areas.Admin.Controllers
{
    public class BlogController : BaseController
    {
        public BlogController(AppDbContext dbContext): base(dbContext) {}
        
        // GET
        public IActionResult Index()
        {
            return
            View();
        }

        public IActionResult Article(int id = default(int))
        {
            return View(new ArticleViewModel
            {
                Article = id == default(int) ? new Article() : DbContext.Articles.Find(id),
                Categories = DbContext.Categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }),
                Tags = DbContext.Tags.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Title
                })
            });
        }
    }
}