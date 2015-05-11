using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CodeBox.WebUI.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username must have at least 3 characters and can only contain letters, numbers, - and _.")]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password must be at least 6 characters long and contain at least one number and one special character.")]
        [DisplayName("Desired password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The e-mail address provided is invalid. Please check the value and try again.")]
        [DisplayName("Email address")]
        public string Mail { get; set; }
    }
}