using System.Collections.Generic;
using CzSharp.Model.Entities;

namespace CzSharp.ViewModels
{
    /// <summary>
    /// View model for user with its roles
    /// </summary>
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