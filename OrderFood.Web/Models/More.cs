using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Models
{
    public static class More
    {
        public static string SafeSubstring(string text, int uzunluk)
        {
            if (text.Length > uzunluk)
            {
                return text.Substring(0, uzunluk);
            }
            return text;
        }
    }
}