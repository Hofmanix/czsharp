using System.Collections.Generic;
using CzSharp.Model.Entities;

namespace CzSharp.ViewModels
{
    public class UserWithRolesViewModel
    {
        public User User { get; }
        public List<string> UserRoles { get; }
        
        public UserWithRolesViewModel(User user, List<string> userRoles)
        {
            User = user;
            UserRoles = userRoles;
        }
    }
}