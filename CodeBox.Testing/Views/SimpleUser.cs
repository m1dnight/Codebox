using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBox.Testing.Views
{

    public class SimpleUser
    {
        public string firstName;
        public string lastName;
        public string email;
        public string oldPassword;
        public string newPassword;

        public SimpleUser(string firstName, string lastName, string email) : this(firstName, lastName, email, "", "")
        {}

        public SimpleUser(string firstName, string lastName, string email, string oldPassword, string newPassword) 
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.oldPassword = oldPassword;
            this.newPassword = newPassword;
        }

    }
}
