using DevExpress.ExpressApp;
using DevExpress.ExpressApp.EF;
using Solution1.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.Helper
{
    public class UserHelper
    {
        public static TheUser GetCurrentUser()
        {
            return SecuritySystem.CurrentUser as TheUser;
        }

    }
}
