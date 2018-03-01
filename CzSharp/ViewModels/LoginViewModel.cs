﻿using System.ComponentModel.DataAnnotations;

namespace CzSharp.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Uživatelské jméno")]
        public string Username { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; }
        
        [Display(Name = "Zapamatovat")]
        public bool RememberMe { get; set; }
    }
}