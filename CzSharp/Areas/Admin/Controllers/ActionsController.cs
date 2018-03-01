using CzSharp.Model;
using Microsoft.AspNetCore.Mvc;

namespace CzSharp.Areas.Admin.Controllers
{
    public class ActionsController : BaseController
    {
        public ActionsController(AppDbContext dbContext): base(dbContext) {}
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}