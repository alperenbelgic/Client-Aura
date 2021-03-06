﻿using DevExpress.ExpressApp;
using DevExpress.ExpressApp.EF;
using Solution1.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.Helper
{
    public static class SystemHelper
    {
        public static DateTime GetSystemTime()
        {
            return DateTime.Now;
        }

        public static Solution1DbContext GetDbContext()
        {
            var dbContext = new Solution1DbContext(GeneralKeys.GetConnectionString());

            return dbContext;
        }

        public static IObjectSpace GetObjectSpace()
        {
            EFObjectSpaceProvider osProvider = new EFObjectSpaceProvider(typeof(Solution1DbContext), GeneralKeys.GetConnectionString());
            return osProvider.CreateObjectSpace();
        }
    }
}
