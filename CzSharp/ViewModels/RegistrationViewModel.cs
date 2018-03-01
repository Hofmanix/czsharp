using System.ComponentModel.DataAnnotations;

namespace CzSharp.ViewModels
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Musíte vyplnit uživatelské jméno")]
        [Display(Name = "Uživatelské jméno")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Musíte vyplnit heslo")]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Musíte vyplnit heslo znovu")]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo znovu")]
        [Compare("Password", ErrorMessage = "Hesla se musí shodovat")]
        public string PasswordAgain { get; set; }
        
        [Required(ErrorMessage = "Musíte vyplnit emailovou adresy")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Emailová adresa není ve správném tvaru")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}