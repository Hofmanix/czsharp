using Microsoft.AspNetCore.Mvc;

namespace CzSharp.Areas.Events.Controllers
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