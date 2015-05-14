using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using CodeBox.WebUI.Infrastructure.Concrete;

namespace CodeBox.Testing
{
    class DerivedMembershipController : CustomMembershipProvider
    {
        public void ValidatePassword(ValidatePasswordEventArgs args)
        {
            base.OnValidatingPassword(args);
        }
    }
}
