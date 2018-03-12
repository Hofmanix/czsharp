using System.Linq;
using System.Threading.Tasks;
using CzSharp.Model;
using CzSharp.Model.Entities;
using CzSharp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CzSharp.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        private UserManager<User> usersManager;

        public UsersController(AppDbContext dbContext, UserManager<User> usersManager) : base(dbContext)
        {
            this.usersManager = usersManager;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return
                View(DbContext.Users.ToList().GroupJoin(
                    DbContext.UserRoles.Join(DbContext.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => new
                    {
                        ur.UserId,
                        r.Name
                    }), u => u.Id, ur => ur.UserId, 
                    (u, rin) => new UserWithRolesViewModel(u, rin.Select(i => i.Name).ToList())));
        }

        [HttpGet]
        public async Task<IActionResult> Activate(string id)
        {
            var user = await usersManager.FindByIdAsync(id);
            user.EmailConfirmed = true;
            await usersManager.UpdateAsync(user);

            TempData["SuccessMessage"] = "Uživatel byl aktivován.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await usersManager.FindByIdAsync(id);
            await usersManager.DeleteAsync(user);

            TempData["SuccessMessage"] = "Uživatel byl smazán.";
            return RedirectToAction("Index");
        }
    }
}