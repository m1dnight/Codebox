using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Scratchpad
{
    class Program
    {
        static void Main(string[] args)
        {
            String hashed = GetSHA256Hash("abc123");
            Console.WriteLine(hashed);
            //c18f3e0599590d1f028ac69563d25c03f83f3a4981afab4a040a0137c4f9fb78
            Console.Read();
        }
        public static string GetSHA256Hash(string message)
        {
            var ue = new UnicodeEncoding();
            var hashstring = new SHA256Managed();
            var hashvalue = hashstring.ComputeHash(ue.GetBytes(message));
            var hex = hashvalue.Aggregate("", (current, x) => current + String.Format("{0:x2}", x));
            return hex;
        }
    }
}
