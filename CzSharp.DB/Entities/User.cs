using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CzSharp.DB.Entities
{
    public class User: IdentityUser<int>
    {
    }
}