using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace EcommerceEcovilleA.Utils
{
    static public class Seguranca
    {
        private static string key = "31e04615-c1c5-4496-924f-f714f3c600d5";
        public static string CriptografaCookie(string strText)
        {
            var cookieText = Encoding.UTF8.GetBytes(strText);
            var encryptedValue = Convert.ToBase64String(MachineKey.Protect(cookieText, key));
            return encryptedValue;
        }

        public static string DescriptografaCookie(string strText)
        {
            var bytes = Convert.FromBase64String(strText);
            var output = MachineKey.Unprotect(bytes, key);
            string result = Encoding.UTF8.GetString(output);
            return result;
        }

    }
}