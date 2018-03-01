using CzSharp.Model;
using Microsoft.AspNetCore.Mvc;

namespace CzSharp.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    public abstract class BaseController: Controller
    {
        protected AppDbContext DbContext;

        protected BaseController(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}