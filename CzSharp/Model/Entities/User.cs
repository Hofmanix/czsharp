using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CzSharp.Model.Entities
{
    public class User: IdentityUser<int>
    {
    }
}