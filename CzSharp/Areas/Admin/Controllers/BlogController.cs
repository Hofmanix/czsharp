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
    }
}