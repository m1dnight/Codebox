using System.Collections.Generic;
using System.Web.Mvc;

namespace CodeBox.WebUI.Models.Admin.Widgets
{
    public class RoleWidgetModel
    {
        public IEnumerable<SelectListItem> Roles { get; set; }
        public int SelectedItem { get; set; }
    }
}