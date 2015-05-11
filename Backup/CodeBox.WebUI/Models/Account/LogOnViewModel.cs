using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CodeBox.WebUI.Models.Account
{
    public class LogOnViewModel
    {
        [Required(ErrorMessage = " ")]
        [DisplayName("User name")]
        public string Username { get; set; }

        [Required(ErrorMessage = " ")]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
}
}