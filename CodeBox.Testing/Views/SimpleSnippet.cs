using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBox.Testing.Views
{
    public class SimpleSnippet
    {

        public string _name { get; set; }
        public string _code { get; set; }
        public string _description { get; set; }
        public string _language { get; set; }
        public bool _isPublic { get; set; }

        public SimpleSnippet(string name,
                             string code,
                             string description) : this(name, code, description, "None", false)
        {
        }

        public SimpleSnippet(string name,
                             string code,
                             string description,
                             string language,
                             bool isPublic)
        {
            _name = name;
            _code = code;
            _description = description;
            _language = language;
            _isPublic = isPublic;

        }

    }
}
