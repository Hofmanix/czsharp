using System;
using Microsoft.AspNetCore.Identity;

namespace CzSharp.DB.Entities
{
    public class UserRole: IdentityRole<int>
    {
        public const string
            Administrator = "Administrator",
            Moderator = "Moderator",
            SeniorBlogger = "SeniorBlogger",
            Blogger = "Blogger",
            Coder = "Coder",
            EventCreator = "EventCreator",
            Member = "Member";

        public static string[] Roles => new[]
            {Administrator, Moderator, SeniorBlogger, Blogger, Coder, EventCreator, Member};
    }
}