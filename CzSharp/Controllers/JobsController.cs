using Microsoft.AspNetCore.Mvc;

namespace CzSharp.Controllers
{
    public class JobsController: BaseController
    {
        /// <summary>
        /// TODO: Create jobs
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}