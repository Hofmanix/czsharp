using Microsoft.AspNetCore.Mvc;

namespace CzSharp.Controllers
{
    public class JobsController: BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}