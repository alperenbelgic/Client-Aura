using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.Helper
{
    public static class RoleNames
    {
        public static string Administrators = "Administrators";
    }

    public static class GeneralKeys
    {
        public static string ActionActiveKey = "CA_ActionActiveKey";

        public static string SaveButtonDefaultCaption = "Save";

        public static string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

    }
}
