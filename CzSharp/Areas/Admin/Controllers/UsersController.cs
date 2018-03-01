using System.Linq;
using CzSharp.Model;
using CzSharp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CzSharp.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController(AppDbContext dbContext): base(dbContext) { }
        
        // GET
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
    }
}