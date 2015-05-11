using System.Collections.Generic;
using System.Web.Mvc;

namespace CodeBox.WebUI.Models.Snippet
{
    public class SnippetCRUDViewModel
    {
        private List<SelectListItem> _languages = new List<SelectListItem>();
        public List<SelectListItem> Languages
        {
            get
            {
                    return _languages;
            }
            set { _languages = value; }
        }
        public int SelectedLanguageId { get; set; }
        public Domain.Concrete.ORM.Snippet Snippet { get; set; }
        public List<SelectListItem> Groups { get; set; }
        public int?[] SelectedGroupId { get; set; }
    }
}