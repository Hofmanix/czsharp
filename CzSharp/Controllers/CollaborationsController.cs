using Microsoft.AspNetCore.Mvc;

namespace CzSharp.Controllers
{
    public class CollaborationsController: BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}