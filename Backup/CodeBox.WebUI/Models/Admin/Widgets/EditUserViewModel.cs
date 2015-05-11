using System.Collections.Generic;
using System.Web.Mvc;
using CodeBox.Domain.Concrete.ORM;

namespace CodeBox.WebUI.Models.Admin.Widgets
{
    public class EditUserViewModel
    {
        public User User { get; set; }
        public IEnumerable<SelectListItem> PossibleRoles { get; set; }
        public int[] SelectedRoles { get; set; }
}
}