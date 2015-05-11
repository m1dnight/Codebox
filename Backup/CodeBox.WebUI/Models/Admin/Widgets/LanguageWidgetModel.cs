using System.Collections.Generic;
using System.Web.Mvc;

namespace CodeBox.WebUI.Models.Admin.Widgets
{
    public class LanguageWidgetModel
    {
        
        public IEnumerable<SelectListItem> Languages { get; set; }
        public int SelectedItem { get; set; }
    }
}