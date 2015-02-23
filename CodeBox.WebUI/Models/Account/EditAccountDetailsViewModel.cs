using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CodeBox.WebUI.Models.Account
{
    public class EditAccountDetailsViewModel
    {

        [HiddenInput(DisplayValue=false)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required!")]
        public string Surname { get; set; }

        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Email is required!")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|org|net|edu|gov|mil|biz|info|mobi|name|aero|asia|jobs|museum|be)\b", ErrorMessage = "Email address is not valid!")]
        public string Mail { get; set; }


        public byte[] ImageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimetype { get; set; }
    }

    public class RegularExpressionAttributeNoGroups : RegularExpressionAttribute
    {
        public RegularExpressionAttributeNoGroups(string pattern) : base(pattern)
        {

        }
    }
}