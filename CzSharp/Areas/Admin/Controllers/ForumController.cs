using CzSharp.Model;
using Microsoft.AspNetCore.Mvc;

namespace CzSharp.Areas.Admin.Controllers
{
    public class ForumController : BaseController
    {
        public ForumController(AppDbContext dbContext): base(dbContext) {}
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}