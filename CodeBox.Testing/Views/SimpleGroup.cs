using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBox.Testing.Views
{

    public class SimpleGroup
    {
        private string description;
        private string name;

        public SimpleGroup(string description, string name)
        {
            this.description = description;
            this.name = name;
        }

        public string Description()
        {
            return description;
        }

        public string Name()
        {
            return name;
        }
    }
}
