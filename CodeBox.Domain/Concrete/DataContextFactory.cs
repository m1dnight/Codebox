using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using CodeBox.Domain.Concrete.ORM;

namespace CodeBox.Domain.Concrete
{
    public class DataContextFactory
    {
        public static CodeBoxEntities CodeBoxEntities
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Items["Context"] == null)
                {
                    HttpContext.Current.Items["Context"] = new CodeBoxEntities();
                }
                return HttpContext.Current.Items["Context"] as CodeBoxEntities;
            }
            set
            {
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Items["Context"] = value;
                }
            }
        }
    }
}
