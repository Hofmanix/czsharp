using Microsoft.AspNetCore.Mvc;

namespace CzSharp.Areas.Forum.Controllers
{
    public class DefaultController : BaseController
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}