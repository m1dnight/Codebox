using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeBox.WebUI.Models.Home
{
    public class IndexViewModel
    {
        public List<Domain.Concrete.ORM.Snippet> PublicSnippets { get; set; }
        public int Usercount { get; set; }
        public int SnippetCount { get; set; }
        public int UsersOnline { get; set; }
    }
}