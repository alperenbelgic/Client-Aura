using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solution1.Web.Helper
{
    public class SessionHelper
    {
        public static void AssignSessionValue(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public static T GetSessionValue<T>(string key) where T : class
        {
            return (T)HttpContext.Current.Session[key];
        }


    }
}