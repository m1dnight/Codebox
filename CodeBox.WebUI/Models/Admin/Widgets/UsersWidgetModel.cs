using System.Collections.Generic;
using System.Web.Mvc;

namespace CodeBox.WebUI.Models.Admin.Widgets
{
    public class UsersWidgetModel
    {
        public IEnumerable<SelectListItem> Users { get; set; }
        public int SelectedItem { get; set; }
    }
}