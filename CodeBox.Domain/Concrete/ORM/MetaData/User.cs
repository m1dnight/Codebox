using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CodeBox.Domain.Concrete.ORM
{
    [MetadataType(typeof(UserMetaDataSource))]
    public partial class User
    {

    }
    public class UserMetaDataSource
    {
        [HiddenInput(DisplayValue = true)]
        [Display(Order = 1, Name = "User ID")]
        public Int32 UserId { get; set; }

        [Display(Order = 2, Name = "Username")]
        public string Username { get; set; }


        [Display(Order = 3)]
        public string Name { get; set; }

        [Display(Order = 4)]
        public string Surname { get; set; }

        [Display(Order = 5)]
        public String Mail { get; set; }


        [Display(Order = 6)]
        public String Comment { get; set; }

        [Display(Order = 7, Name="Is Approved?")]
        public Boolean Approved { get; set; }


        [Display(Order = 8, Name = "Is Locked?")]
        public Boolean LockedOut { get; set; }

        [HiddenInput(DisplayValue = true)]
        [Display(Order = 9, Name = "Registration Date")]
        public DateTime CreationDate { get; set; }

        [HiddenInput(DisplayValue = true)]
        [Display(Order = 10, Name = "Last Login")]
        public DateTime? lastLoginDate { get; set; }

        [HiddenInput(DisplayValue = true)]
        [Display(Order = 11, Name="Last password change")]
        public DateTime? LastPasswordChangeDate { get; set; }


        [HiddenInput(DisplayValue = true)]
        [Display(Order = 12, Name="Last time locked out")]
        public DateTime? LastLockOutDate { get; set; }

        [HiddenInput(DisplayValue = true)]
        [Display(Order = 13, Name="Last activity")]
        public DateTime? LastActivityDate { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? LastSeen { get; set; }

        [ScaffoldColumn(false)]
        public String ProviderName { get; set; }

        [ScaffoldColumn(false)]
        public Byte[] ImageData { get; set; }

        [ScaffoldColumn(false)]
        public String ImageMimeType { get; set; }

        [ScaffoldColumn(false)]
        public String passwordQuestion { get; set; }

        [ScaffoldColumn(false)]
        public string Password { get; set; }

    }
}
