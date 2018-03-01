using CzSharp.Model;
using Microsoft.AspNetCore.Mvc;

namespace CzSharp.Areas.Admin.Controllers
{
    public class OverviewController : BaseController
    {
        public OverviewController(AppDbContext dbContext): base(dbContext) {}
        
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}