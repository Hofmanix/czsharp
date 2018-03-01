using CzSharp.Model;
using Microsoft.AspNetCore.Mvc;

namespace CzSharp.Areas.Admin.Controllers
{
    public class CollaborationsController : BaseController
    {
        public CollaborationsController(AppDbContext dbContext): base(dbContext) {}
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}