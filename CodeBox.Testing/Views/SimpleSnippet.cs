using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBox.Testing.Views
{
    public class SimpleSnippet
    {

        private string name;
        private string code;
        private string description;
        private string language;
        private bool isPublic;
        private string group;

        public SimpleSnippet(string name,
                             string code,
                             string description) : this(name, code, description, "None", false)
        {}

        public SimpleSnippet(string name,
                             string code,
                             string description,
                             string language,
                             bool isPublic) : this(name, code, description, language, isPublic, "None")
        { }

        public SimpleSnippet(string name,
                             string code,
                             string description,
                             string language,
                             bool isPublic,
                             string group)
        {
            this.name = name;
            this.code = code;
            this.description = description;
            this.language = language;
            this.isPublic = isPublic;
            this.group = group;
        }

        public string Name()
        {
            return name;
        }

        public string Description()
        {
            return description;
        }

        public string Code()
        {
            return code;
        }

        public string Language()
        {
            return language;
        }

        public bool IsPublic()
        {
            return isPublic;
        }

        public string Group()
        {
            return group;
        }

    }
}
