using System.ComponentModel.DataAnnotations;

namespace CodeBox.WebUI.Models.Group
{
    public class AddUserViewModel
    {
        private const string MailRegex = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|org|net|edu|gov|mil|biz|info|mobi|name|aero|asia|jobs|museum|be)\b";

        [RegularExpression(MailRegex, ErrorMessage = "Not a valid address!")]
        [Required]
        public string Mail { get; set; }

        public string ReturnUrl { get; set; }

        public int GroupId { get; set; }
    }
}